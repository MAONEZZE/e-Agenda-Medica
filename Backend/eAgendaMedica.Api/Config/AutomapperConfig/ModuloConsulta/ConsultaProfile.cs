﻿using eAgendaMedica.Api.ViewModels.ModuloConsulta;
using eAgendaMedica.Dominio.ModuloConsulta;

namespace eAgendaMedica.Api.Config.AutomapperConfig.ModuloConsulta
{
    public class ConsultaProfile : Profile
    {
        public ConsultaProfile()
        {
            //CreateMap<O que é, O que vai virar>();
            CreateMap<Consulta, ListarConsultaViewModel>()
                .ForMember(consultaVM => consultaVM.Data, opt => opt.MapFrom(consulta => consulta.Data.ToShortDateString()))
                .ForMember(consultaVM => consultaVM.HoraInicio, opt => opt.MapFrom(consulta => consulta.HoraInicio.ToString(@"hh\:mm\:ss")));

            CreateMap<Consulta, VisualizarConsultaViewModel>()
                .ForMember(consultaVM => consultaVM.Data, opt => opt.MapFrom(consulta => consulta.Data.ToShortDateString()))
                .ForMember(consultaVM => consultaVM.HoraInicio, opt => opt.MapFrom(consulta => consulta.HoraInicio.ToString(@"hh\:mm\:ss")))
                .ForMember(consultaVM => consultaVM.HoraTermino, opt => opt.MapFrom(consulta => consulta.HoraTermino.ToString(@"hh\:mm\:ss")));

            CreateMap<FormConsultaViewModel, Consulta>()
                .AfterMap<InserirMedicoConsultaMappingAction>()
                .AfterMap<InserirPacienteConsultaMappingAction>();

            CreateMap<Consulta, FormConsultaViewModel>();
        }
    }
}
