﻿using eAgendaMedica.Api.ViewModels.ModuloPaciente;
using eAgendaMedica.Dominio.ModuloPaciente;

namespace eAgendaMedica.Api.Config.AutomapperConfig.ModuloPaciente
{
    public class PacienteProfile : Profile
    {
        public PacienteProfile()
        {
            //CreateMap<O que é, O que eu quero que vire>();
            CreateMap<Paciente, ListarPacienteViewModel>();

            CreateMap<Paciente, VisualizarPacienteViewModel>();

            CreateMap<FormPacienteViewModel, Paciente>();

            CreateMap<Paciente, FormPacienteViewModel>();
        }
    }
}
