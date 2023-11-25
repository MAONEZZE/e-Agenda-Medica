﻿using eAgendaMedica.Dominio.ModuloCirurgia;
using eAgendaMedica.Dominio.ModuloConsulta;
using eAgendaMedica.Dominio.ModuloMedico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAgendaMedica.Dominio.Compartilhado
{
    public static class VerificadorDisponibilidadeMedicoExtension
    {
        public static bool VerificadorDisponibilidadeMedico<T>(this Medico medico, Atividade<T> atividadeNova)
        {
            DateTime dataHoraFinal = DateTime.MinValue;
            DateTime dataHoraInicio = DateTime.MaxValue;

            bool disponivel = true;

            foreach (var item in medico.SelecionarAtividades())
            {
                TimeSpan tempoRecuperacao;
                DateTime dataHoraFinalExistente;
                DateTime dataHoraInicioExistente;

                Guid idAtividadeExistente;


                if (item is Consulta)
                {
                    Consulta consulta = (Consulta)item;

                    idAtividadeExistente = consulta.Id;

                    tempoRecuperacao = TimeSpan.FromMinutes(20);

                    dataHoraFinalExistente = consulta.Data.Add(consulta.HoraTermino);
                    dataHoraInicioExistente = consulta.Data.Add(consulta.HoraInicio);
                }
                else
                {
                    Cirurgia cirurgia = (Cirurgia)item;

                    idAtividadeExistente = cirurgia.Id;

                    tempoRecuperacao = TimeSpan.FromHours(4);

                    dataHoraFinalExistente = cirurgia.Data.Add(cirurgia.HoraTermino);
                    dataHoraInicioExistente = cirurgia.Data.Add(cirurgia.HoraInicio);
                }

                //TODO - refazer a logica a partir daqui | tem que saber o tipo do qeu esta inserindo

                var atividadeNovaDataHoraHoraTermino = atividadeNova.Data.Add(atividadeNova.HoraTermino);
                var atividadeNovaDataHoraHoraInicio = atividadeNova.Data.Add(atividadeNova.HoraInicio);

                bool idDiferente = idAtividadeExistente != atividadeNova.Id;
                bool horariosIguais = dataHoraFinalExistente == atividadeNovaDataHoraHoraTermino || dataHoraInicioExistente == atividadeNovaDataHoraHoraInicio;


                if (horariosIguais && idDiferente == false)
                {
                    break;
                }
                else if (horariosIguais && idDiferente)
                {
                    disponivel = false;
                    break;
                }
                else
                {
                    if (dataHoraFinalExistente > dataHoraFinal && dataHoraFinalExistente <= atividadeNovaDataHoraHoraInicio)
                    //verifica o maior horario final mais proximo do inicio dessa nova atividade
                    {
                        dataHoraFinal = dataHoraFinalExistente;
                    }

                    if (dataHoraInicioExistente < dataHoraInicio && dataHoraInicioExistente >= atividadeNovaDataHoraHoraTermino)
                    //verifica o menor horario inicial mais proximo do final dessa nova atividade
                    {
                        dataHoraInicio = dataHoraInicioExistente;
                    }

                    //verifica se tem tempo de recuperação suficiente

                    if (Math.Abs((atividadeNovaDataHoraHoraInicio - dataHoraFinal).Ticks) < tempoRecuperacao.Ticks)
                    {
                        disponivel = false;
                        break;
                    }
                    else if (Math.Abs((atividadeNovaDataHoraHoraTermino - dataHoraInicio).Ticks) < tempoRecuperacao.Ticks)
                    {
                        disponivel = false;
                        break;
                    }
                }
            }

            return disponivel;
        }
    }
}
