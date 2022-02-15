using LeilaoOnline.Core;
using System.Linq;
using Xunit;

namespace LeilaoOnline.Tests
{
    public class LeilaoRecebeOferta
    {
        /* Give: Dado leilão finalizado X lances
         * When: Quando leilão recebe nova oferta de lance
         * Then: Então a qtde de lances continua sendo X 
         */
        [Theory]
        [InlineData(4, new double[] { 1000, 1200, 1400, 1500})]
        [InlineData(2, new double[] { 800, 950})]
        public void NaoPermiteNovosLancesDadoLeilaoFinalizado(int qtdeEsperada, double[] ofertas)
        {
            //Arrange/Given - Cenário
            var leilao = new Leilao("Picasso");
            var siglano = new Interessada("Siglano", leilao);

            foreach (var oferta in ofertas)
                leilao.ReceberLance(siglano, oferta);

            leilao.TerminarPregao();

            //Act/When - Método sob teste
            leilao.ReceberLance(siglano, 1000);

            //Assert/Then
            var qtdeObtido = leilao.Lances.Count();
            Assert.Equal(qtdeEsperada, qtdeObtido);
        }
    }
}
