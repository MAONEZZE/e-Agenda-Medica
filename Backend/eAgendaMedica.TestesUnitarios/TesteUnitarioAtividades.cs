

using eAgendaMedica.Dominio.Compartilhado;
using FluentAssertions;
using Moq;

namespace eAgendaMedica.TestesUnitarios
{
    [TestClass]
    public class TesteUnitarioAtividades
    {
        private readonly Medico medico = new Medico("Antonio", "3242342456-56", "12345-SP");

        private readonly DateTime agora = DateTime.Now;
        private readonly TimeSpan duasHoras = TimeSpan.FromHours(2);
        private readonly TimeSpan quatroHoras = TimeSpan.FromHours(4);
        private readonly TimeSpan seisHoras = TimeSpan.FromHours(6);
        private readonly TimeSpan oitoHoras = TimeSpan.FromHours(8);
        private readonly TimeSpan dezHoras = TimeSpan.FromHours(10);

        private readonly TimeSpan vinteMinutos = TimeSpan.FromMinutes(20);
        private readonly TimeSpan quarentaMinutos = TimeSpan.FromMinutes(40);

        [TestMethod]
        public void Deve_retornar_true_se_anterior_for_cirurgia_intervalo_maiorOUigual_4horas() 
        {
            //Arrange
            var cirurgia1 = new Cirurgia(new DateTime(2020, 07, 02), duasHoras, seisHoras, It.IsAny<Paciente>());
            medico.AdicionarCirurgia(cirurgia1);

            
            //Action
            var cirurgiaMarcada = new Cirurgia(new DateTime(2020, 07, 02), dezHoras, new TimeSpan(11, 14, 18), It.IsAny<Paciente>());

            var disponivel = medico.VerificadorDisponibilidadeMedico<Cirurgia>(cirurgiaMarcada);

            //Assert
            disponivel.Should().Be(true);
        }

        [TestMethod]
        public void Deve_retornar_false_se_tentar_inserir_novaCirurgia_mesmo_horarioInicio_cirurgiaExistente()
        {
            //Arrange
            var cirurgia1 = new Cirurgia(new DateTime(2020, 07, 02), duasHoras, seisHoras, It.IsAny<Paciente>());
            medico.AdicionarCirurgia(cirurgia1);


            //Action
            var cirurgiaMarcada = new Cirurgia(new DateTime(2020, 07, 02), duasHoras, oitoHoras, It.IsAny<Paciente>());

            var disponivel = medico.VerificadorDisponibilidadeMedico<Cirurgia>(cirurgiaMarcada);

            //Assert
            disponivel.Should().Be(false);
        }

        [TestMethod]
        public void Deve_retornar_false_se_anterior_for_cirurgia_intervalo_menor_4horas()
        {
            //Arrange
            var cirurgia1 = new Cirurgia(new DateTime(2020, 07, 02), duasHoras, seisHoras, It.IsAny<Paciente>());
            medico.AdicionarCirurgia(cirurgia1);

            //Action
            var cirurgiaMarcada = new Cirurgia(new DateTime(2020, 07, 02), oitoHoras, new TimeSpan(11, 14, 18), It.IsAny<Paciente>());

            var disponivel = medico.VerificadorDisponibilidadeMedico<Cirurgia>(cirurgiaMarcada);

            //Assert
            disponivel.Should().Be(false);
        }

        [TestMethod]
        public void Deve_retornar_false_caso_dataMarcada_seja_igualOUmaior_que_DataInicio_de_Cirurgia_existente()
        {
            //TODO - Terminar os testes
            //Arrange
            var cirurgia1 = new Cirurgia(new DateTime(2020, 07, 02), duasHoras, seisHoras, It.IsAny<Paciente>());
            medico.AdicionarCirurgia(cirurgia1);

            //Action
            var cirurgiaMarcada = new Cirurgia(new DateTime(2020, 07, 02), seisHoras, oitoHoras, It.IsAny<Paciente>());

            var disponivel = medico.VerificadorDisponibilidadeMedico<Cirurgia>(cirurgiaMarcada);

            //Assert
            disponivel.Should().Be(false);
        }



        [TestMethod]
        public void Deve_inserir_novaCirurgia_entre_duas_cirurgiasExistente()
        {
            //Arrange
            var cirurgia1 = new Cirurgia(new DateTime(2020, 07, 02), duasHoras, seisHoras, It.IsAny<Paciente>());
            medico.AdicionarCirurgia(cirurgia1);

            var cirurgia2 = new Cirurgia(new DateTime(2020, 07, 02), new TimeSpan(17, 14, 18), new TimeSpan(18, 14, 18), It.IsAny<Paciente>());
            medico.AdicionarCirurgia(cirurgia2);

            //Action
            var cirurgiaMarcada = new Cirurgia(new DateTime(2020, 07, 02), dezHoras, new TimeSpan(11, 14, 18), It.IsAny<Paciente>());

            var disponivel = medico.VerificadorDisponibilidadeMedico<Cirurgia>(cirurgiaMarcada);

            //Assert
            disponivel.Should().Be(true);
        }

        [TestMethod]
        public void Nao_Deve_inserir_novaCirurgia_entre_duas_cirurgiasExistente_comHoraFinal_maiorOUigual_CirurgiaExistenteHoraInicio()
        {
            //Arrange
            var cirurgia1 = new Cirurgia(new DateTime(2020, 07, 02), duasHoras, seisHoras, It.IsAny<Paciente>());
            medico.AdicionarCirurgia(cirurgia1);

            var cirurgia2 = new Cirurgia(new DateTime(2020, 07, 02), new TimeSpan(17, 14, 18), new TimeSpan(18, 14, 18), It.IsAny<Paciente>());
            medico.AdicionarCirurgia(cirurgia2);

            //Action
            var cirurgiaMarcada = new Cirurgia(new DateTime(2020, 07, 02), dezHoras, new TimeSpan(17, 14, 18), It.IsAny<Paciente>());

            var disponivel = medico.VerificadorDisponibilidadeMedico<Cirurgia>(cirurgiaMarcada);

            //Assert
            disponivel.Should().Be(false);
        }

        //=======================================

        [TestMethod]
        public void Deve_retornar_true_se_anterior_for_consulta_intervalo_maiorOUigual_20min()
        {
            //Arrange
            var consulta1 = new Consulta(new DateTime(2020, 07, 02), duasHoras, seisHoras, It.IsAny<Paciente>(), It.IsAny<Medico>());
            medico.AdicionarConsulta(consulta1);


            //Action
            var consultaMarcada = new Consulta(new DateTime(2020, 07, 02), dezHoras, new TimeSpan(11, 14, 18), It.IsAny<Paciente>(), It.IsAny<Medico>());

            var disponivel = medico.VerificadorDisponibilidadeMedico<Consulta>(consultaMarcada);

            //Assert
            disponivel.Should().Be(true);
        }

        [TestMethod]
        public void Deve_retornar_false_se_anterior_for_consulta_intervalo_menor_20min()
        {
            //Arrange
            var consulta1 = new Consulta(new DateTime(2020, 07, 02), duasHoras, seisHoras, It.IsAny<Paciente>(), It.IsAny<Medico>());
            medico.AdicionarConsulta(consulta1);

            //Action
            var consultaMarcada = new Consulta(new DateTime(2020, 07, 02), new TimeSpan(6, 14, 18), new TimeSpan(11, 14, 18), It.IsAny<Paciente>(), It.IsAny<Medico>());

            var disponivel = medico.VerificadorDisponibilidadeMedico<Consulta>(consultaMarcada);

            //Assert
            disponivel.Should().Be(false);
        }


        [TestMethod]
        public void Deve_inserir_novaConsulta_entre_duas_cirurgiasExistente()
        {
            //Arrange
            var consulta1 = new Consulta(new DateTime(2020, 07, 02), duasHoras, seisHoras, It.IsAny<Paciente>(), It.IsAny<Medico>());
            medico.AdicionarConsulta(consulta1);

            var consulta2 = new Consulta(new DateTime(2020, 07, 02), new TimeSpan(17, 14, 18), new TimeSpan(18, 14, 18), It.IsAny<Paciente>(), It.IsAny<Medico>());
            medico.AdicionarConsulta(consulta2);

            //Action
            var consultaMarcada = new Consulta(new DateTime(2020, 07, 02), dezHoras, new TimeSpan(11, 14, 18), It.IsAny<Paciente>(), It.IsAny<Medico>());

            var disponivel = medico.VerificadorDisponibilidadeMedico<Consulta>(consultaMarcada);

            //Assert
            disponivel.Should().Be(true);
        }

        [TestMethod]
        public void Nao_Deve_inserir_novaConsulta_entre_duas_consultasExistente_comHoraFinal_maiorOUigual_ConsultaExistenteHoraInicio()
        {
            //Arrange
            var consulta1 = new Consulta(new DateTime(2020, 07, 02), duasHoras, seisHoras, It.IsAny<Paciente>(), It.IsAny<Medico>());
            medico.AdicionarConsulta(consulta1);

            var consulta2 = new Consulta(new DateTime(2020, 07, 02), new TimeSpan(17, 14, 18), new TimeSpan(18, 14, 18), It.IsAny<Paciente>(), It.IsAny<Medico>());
            medico.AdicionarConsulta(consulta2);

            //Action
            var consultaMarcada = new Consulta(new DateTime(2020, 07, 02), dezHoras, new TimeSpan(17, 14, 18), It.IsAny<Paciente>(), It.IsAny<Medico>());

            var disponivel = medico.VerificadorDisponibilidadeMedico<Consulta>(consultaMarcada);

            //Assert
            disponivel.Should().Be(false);
        }

    }
}