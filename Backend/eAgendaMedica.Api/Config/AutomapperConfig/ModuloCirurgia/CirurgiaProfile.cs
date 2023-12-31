﻿using eAgendaMedica.Api.ViewModels.ModuloCirurgia;
using eAgendaMedica.Dominio.ModuloCirurgia;

namespace eAgendaMedica.Api.Config.AutomapperConfig.ModuloCirurgia
{
    public class CirurgiaProfile : Profile
    {
        public CirurgiaProfile()
        {
            //CreateMap<O que é, O que vai virar>();
            CreateMap<Cirurgia, ListarCirurgiaViewModel>()
                .ForMember(cirurgiaVM => cirurgiaVM.Data, opt => opt.MapFrom(cirurgia => cirurgia.Data.ToShortDateString()))
                .ForMember(cirurgiaVM => cirurgiaVM.HoraInicio, opt => opt.MapFrom(cirurgia => cirurgia.HoraInicio.ToString(@"hh\:mm")));

            CreateMap<Cirurgia, VisualizarCirurgiaViewModel>()
                .ForMember(cirurgiaVM => cirurgiaVM.Data, opt => opt.MapFrom(cirurgia => cirurgia.Data.ToShortDateString()))
                .ForMember(cirurgiaVM => cirurgiaVM.HoraInicio, opt => opt.MapFrom(cirurgia => cirurgia.HoraInicio.ToString(@"hh\:mm")))
                .ForMember(cirurgiaVM => cirurgiaVM.HoraTermino, opt => opt.MapFrom(cirurgia => cirurgia.HoraTermino.ToString(@"hh\:mm")));

            CreateMap<FormCirurgiaViewModel, Cirurgia>()
                .AfterMap<InserirMedicoCirurgiaMappingAction>()
                .AfterMap<InserirPacienteCirurgiaMappingAction>();

            CreateMap<Cirurgia, FormCirurgiaViewModel>();
        }
    }
}
