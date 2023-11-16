﻿using eAgendaMedica.Aplicacao.ModuloCirurgia;
using eAgendaMedica.Aplicacao.ModuloConsulta;
using eAgendaMedica.Aplicacao.ModuloMedico;
using eAgendaMedica.Aplicacao.ModuloPaciente;
using eAgendaMedica.Dominio.Compartilhado;
using eAgendaMedica.Dominio.ModuloCirurgia;
using eAgendaMedica.Dominio.ModuloConsulta;
using eAgendaMedica.Dominio.ModuloMedico;
using eAgendaMedica.Dominio.ModuloPaciente;
using eAgendaMedica.Infra.Compartilhado;
using eAgendaMedica.Infra.ModuloCirurgia;
using eAgendaMedica.Infra.ModuloConsulta;
using eAgendaMedica.Infra.ModuloMedico;
using eAgendaMedica.Infra.ModuloPaciente;
using Microsoft.EntityFrameworkCore;

namespace eAgendaMedica.Api.Config
{
    public static class InjecaoDependenciaConfigExtension
    {
        public static void ConfigurarInjecaoDependencia(this IServiceCollection services, IConfiguration config)
        {
            string connectionString = config.GetConnectionString("SqlServer");

            services.AddDbContext<IContextoPersistencia, eAgendaMedicaDbContext>(optionsBuilder => 
            {
                optionsBuilder.UseSqlServer(connectionString);
            });

            //Registrando esse contexto com a interface IContextoPersistencia
            //Em outros lugares do código, pode injetar IContextoPersistencia e obter uma instância de eAgendaMedicaDbContext
            services.AddTransient<ManipuladorExcecoes>();

            services.AddTransient<IRepositorioCirurgia, RepositorioCirurgia>();
            services.AddTransient<ServicoCirurgia>();

            services.AddTransient<IRepositorioConsulta, RepositorioConsulta>();
            services.AddTransient<ServicoConsulta>();

            services.AddTransient<IRepositorioMedico, RepositorioMedico>();
            services.AddTransient<ServicoMedico>();

            services.AddTransient<IRepositorioPaciente, RepositorioPaciente>();
            services.AddTransient<ServicoPaciente>();
        }
    }
}
