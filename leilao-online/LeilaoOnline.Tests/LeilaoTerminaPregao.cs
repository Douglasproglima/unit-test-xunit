using LeilaoOnline.Core;
using System.Collections.Generic;
using Xunit;
namespace LeilaoOnline.Tests
{
    public class LeilaoTerminaPregao
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

        /* Nomes dos Testes: Give, When e Then
         * 1 Parte -  Qual o método que está sendo testado;
         * 2 Parte -  Qual as condições de entrada ou qual é o cenário
         * 3 Parte - Qual é a espectativa do teste
         * EXEMPLOS:
         *  CatalogoControllerGetImage.CallsImageServiceWithId
         *  CatalogoControllerGetImage.LogsWarningGivenImageMissingException
         *  CatalogoControllerGetImage.ReturnsFileResultWithByteGivenSuccess
         *  CatalogoControllerGetImage.ReturnsNotFoundResultGivenImageMissingException
         */

        [Theory]
        [ClassData(typeof(LeilaoFinalizarPregao))]
        public void RetornarMaiorLanceDoLeilaoComPeloMenosUmLance(Lance ganhandor, Leilao leilao, List<Lance> lances)
        {
            // Arrange/Give - Cenário
            leilao.IniciarPregao();
            foreach (var lance in lances)
                leilao.ReceberLance(lance.Cliente, lance.Valor);

            //Act/When - Método sob teste
            leilao.TerminarPregao();

            //Assert/Then
            Assert.Equal(ganhandor.Cliente, leilao.Ganhador.Cliente);
            Assert.Equal(ganhandor.Valor, leilao.Ganhador.Valor);
        }

        /* Give: Dado leilão com pelo ao menos um lance
         * When: Quando o pregão/leilão terminar
         * Then: Então o valor esperado é maior que o valor dado
         *       e o cliente ganhador é o que deu o maior lance.
         */
        [Theory]
        [InlineData(1000, new double[] { 100, 990, 998, 1000})]
        [InlineData(1000, new double[] { 100, 1000, 990, 998})]
        [InlineData(550, new double[] { 550 })]
        public void RetornarMaiorValorDadoLeilaoComPeloMenosUmLance(double valorEsperado, double[] ofertas)
        {
            //Arrange/Given - Cenário
            var leilao = new Leilao("Van Gogh");
            var douglas = new Interessada("Douglas", leilao);
            var fulano = new Interessada("Fulano", leilao);
            
            leilao.IniciarPregao();

            for (int index = 0; index < ofertas.Length; index++)
            {
                var valor = ofertas[index];
                if ((index % 2) == 0)
                    leilao.ReceberLance(douglas, valor);
                else
                    leilao.ReceberLance(fulano, valor);
            }

            //Act/When - Método sob teste
            leilao.TerminarPregao();
            
            //Assert/Then
            var valorObtido = leilao.Ganhador.Valor;
            Assert.Equal(valorEsperado, valorObtido);
        }

        /* Give: Dado leilão sem qualquer lance
         * When: Quando o pregão/leilão terminar
         * Then: Então o valor do lance ganhador é zero
         */
        [Fact]
        public void RetornarZeroDadoLeilaoSemLance()
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
