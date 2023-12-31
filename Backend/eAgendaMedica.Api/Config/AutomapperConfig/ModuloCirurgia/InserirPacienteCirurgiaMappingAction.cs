﻿using eAgendaMedica.Api.ViewModels.ModuloCirurgia;
using eAgendaMedica.Dominio.ModuloCirurgia;
using eAgendaMedica.Dominio.ModuloPaciente;

namespace eAgendaMedica.Api.Config.AutomapperConfig.ModuloCirurgia
{
    public class InserirPacienteCirurgiaMappingAction : IMappingAction<FormCirurgiaViewModel, Cirurgia>
    {
        private readonly IRepositorioPaciente repPaciente;
        public InserirPacienteCirurgiaMappingAction(IRepositorioPaciente repPaciente)
        {
            this.repPaciente = repPaciente;
        }

        public void Process(FormCirurgiaViewModel cirurgiaVM, Cirurgia cirurgia, ResolutionContext ctxt)
        {
            cirurgia.PacienteAtributo = repPaciente.SelecionarPorIdAsync(cirurgiaVM.Paciente_id).Result;
        }
    }
}