using LeilaoOnline.Core;
using System.Collections.Generic;
using Xunit;
namespace LeilaoOnline.Tests
{
    public class LeilaoTestes
    {
        /*Padrões:
         * Arrange | Act | Assert
         * Given | When | Then
         */

        /* Classes de Equivalência
         * BVA -> Boundary value analysis
         * Equivalence partitioning 
         */

        /*Tipos de Testes
         * [Fact]......: São teste que são verdades para um tipo particular de dado.
         * [Theories]..: Or Data/Driven Tests: Teste orientado a dados.
         * [MemberData]: 
         * [ClassData].: Teste orientados a classes, em classes estáticas, usado para uma coleção de testes
         */

        [Theory]
        [ClassData(typeof(LeilaoFinalizarPregao))]
        public void RetornarMaiorLanceDoLeilaoComPeloMenosUmLance(Lance ganhandor, Leilao leilao, List<Lance> lances)
        {
            // Arrange/Give - Cenário
            foreach (var lance in lances)
                leilao.RecebeLance(lance.Cliente, lance.Valor);

            //Act/When - Método sob teste
            leilao.TerminarPregao();

            //Assert/Then
            Assert.Equal(ganhandor.Cliente, leilao.Ganhador.Cliente);
            Assert.Equal(ganhandor.Valor, leilao.Ganhador.Valor);
        }

        [Theory]
        [InlineData(1000, new double[] { 100, 990, 998, 1000})]
        [InlineData(1000, new double[] { 100, 1000, 990, 998})]
        [InlineData(550, new double[] { 550 })]
        public void TesteLeilaoComLancesOrdenadosPorValor(double valorEsperado, double[] ofertas)
        {
            //Arrange/Given - Cenário
            var leilao = new Leilao("Van Gogh");
            var douglas = new Interessada("Douglas", leilao);

            foreach (var oferta in ofertas)
                leilao.RecebeLance(douglas, oferta);

            //Act/When - Método sob teste
            leilao.TerminarPregao();
            
            //Assert/Then
            var valorObtido = leilao.Ganhador.Valor;
            Assert.Equal(valorEsperado, valorObtido);
        }

        [Fact]
        public void LeilaoSemLance()
        {
            //Arrange/Given - Cenário
            var leilao = new Leilao("Picasso");

            //Act/When - Método sob teste
            leilao.TerminarPregao();

            //Assert/Then
            var valorEsperado = 0;
            var valorObtido = leilao.Ganhador.Valor;
            Assert.Equal(valorEsperado, valorObtido);
        }
    }
}
