﻿using AutoMapper;
using eAgendaMedica.Aplicacao.Compartilhado;
using eAgendaMedica.Dominio.Compartilhado;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace eAgendaMedica.Api.Controllers.Compartilhado
{
    public abstract class ControladorApiBase<TList, TForm, TVisu, TEntity> : ControllerBase
        where TList : ViewModelBase<TList>
        where TForm : ViewModelBase<TForm>
        where TVisu : ViewModelBase<TVisu>
        where TEntity : EntidadeBase<TEntity>
    {
        private IMapper _map;
        private IServicoBase<TEntity> _service;

        public ControladorApiBase(IServicoBase<TEntity> service, IMapper map)
        {
            this._map = map;
            this._service = service;
        }

        private IActionResult ProcessarResposta(Result<TEntity> resultado, TForm registroVM = null)
        {
            if (resultado.IsFailed)
            {
                return BadRequest(new
                {
                    Sucesso = false,
                    Errors = resultado.Errors.Select(result => result.Message)
                });
            }

            return Ok(new
            {
                Sucesso = true,
                Dados = registroVM
            });
        }

        [HttpGet]
        [ProducesResponseType(typeof(string[]), 500)]
        public virtual async Task<IActionResult> SelecionarTodos()
        {
            var resultado = await _service.SelecionarTodosAsync();

            if (resultado.IsFailed)
            {
                return BadRequest(new
                {
                    Sucesso = false,
                    Errors = resultado.Errors.Select(result => result.Message)
                });
            }

            List<TList> registrosVM = this._map.Map<List<TList>>(resultado.Value);
            //O que está em parenteses será convertido no que está entre <>

            return Ok(new
            {
                Sucesso = true,
                Dados = registrosVM
            });
        }

        [HttpGet("visualizacao-completa/{id}")] // O {} é para colocar o nome do parametro do metodo. É tipo o :id do angular
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public virtual IActionResult SelecionarPorId(Guid id)
        {
            var resultado = _service.SelecionarPorId(id);

            if (resultado.IsFailed)
            {
                return NotFound(new
                {
                    Sucesso = false,
                    Errors = resultado.Errors.Select(result => result.Message)
                });
            }

            TVisu registroVM = this._map.Map<TVisu>(resultado.Value);

            return Ok(new
            {
                Sucesso = true,
                Dados = registroVM
            });
        }

        [HttpPost("inserir")]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 500)]
        public virtual IActionResult Inserir(TForm registroVM)
        {
            TEntity contato = this._map.Map<TEntity>(registroVM);

            return ProcessarResposta(_service.Inserir(contato), registroVM);
        }

        [HttpPut("editar/{id}")]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public virtual IActionResult Editar(Guid id, TForm registroVM)
        {
            var resultado = _service.SelecionarPorId(id);

            if (resultado.IsFailed)
            {
                return NotFound(new
                {
                    Sucesso = false,
                    Errors = resultado.Errors.Select(result => result.Message)
                });
            }

            TEntity contato = this._map.Map(registroVM, resultado.Value);
            #region Porque usar esse outro Map?
            /* Ele pega a referencia do primeiro objeto passado pro ele
                * this._map.Map(valor, referência)
                * 
                * Existem variações de Maps e caso fizesse assim para editar:
                * 
                *    Contato contato = this.map.Map<Contato>(contatoVM);
                * 
                * O entityframework perderia a referencia do objeto de destino,
                * já que se fizar isso é a mesma coisa de instancia um novo objeto
                */
            #endregion

            return ProcessarResposta(_service.Editar(contato), registroVM);
        }

        [HttpDelete("excluir/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]//Isso daqui mostra os erros qeu podem retornar do endpoint
        public virtual IActionResult Excluir(Guid id)
        {
            var resultado = _service.SelecionarPorId(id);

            if (resultado.IsFailed)
            {
                return NotFound(new
                {
                    Sucesso = false,
                    Errors = resultado.Errors.Select(result => result.Message)
                });
            }

            var registro = resultado.Value;

            return ProcessarResposta(_service.Excluir(registro));
        }
    }
}
