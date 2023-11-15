﻿namespace eAgendaMedica.Aplicacao.Compartilhado
{
    public interface IServicoBase<T>
        where T : EntidadeBase<T>
    {
        public Task<Result<T>> InserirAsync(T registro);
        public Task<Result<T>> EditarAsync(T registro);
        public Task<Result> ExcluirAsync(Guid id);
        public Task<Result<List<T>>> SelecionarTodosAsync();
        public Task<Result<T>> SelecionarPorIdAsync(Guid id);
    }
}
