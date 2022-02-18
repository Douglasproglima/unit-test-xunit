using LeilaoOnline.Core;
using LeilaoOnline.Core.ModalidadeAvaliacao;
using System.Linq;
using Xunit;

namespace LeilaoOnline.Tests
{
    public class LeilaoRecebeOferta
    {
        /* Give: Dado leião iniciado E interessado X realizou o ultimo lance
         * When: Quando mesmo interessado X realiza o próximo lance
         * Then: Então leilão não aceita o segundo lance
         */
        [Fact]
        public void NaoPermiteProximoLanceParaMesmoClienteRealizouMesmoLance()
        {
            //Arrange/Given - Cenário
            var modaliade = new MaiorValor();
            var leilao = new Leilao("Van Gogh", modaliade);
            var fulano = new Interessada("Fulano", leilao);

            leilao.IniciarPregao();
            leilao.ReceberLance(fulano, 800);

            //Act/When - Método sob teste
            leilao.ReceberLance(fulano, 1000);

            //leilao.TerminarPregao();

            //Assert/Then
            int qtdeEsperada = 1;
            var qtdeObtido = leilao.Lances.Count();
            Assert.Equal(qtdeEsperada, qtdeObtido);
        }

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
            var modaliade = new MaiorValor();
            var leilao = new Leilao("Picasso", modaliade);
            var fulano = new Interessada("Fulano", leilao);
            var siglano = new Interessada("Siglano", leilao);
            
            leilao.IniciarPregao();

            for (int index = 0; index < ofertas.Length; index++)
            {
                var valor = ofertas[index];
                if((index % 2) == 0)
                    leilao.ReceberLance(siglano, valor);
                else
                    leilao.ReceberLance(fulano, valor);
            }

            leilao.TerminarPregao();

            //Act/When - Método sob teste
            leilao.ReceberLance(siglano, 1000);

            //Assert/Then
            var qtdeObtido = leilao.Lances.Count();
            Assert.Equal(qtdeEsperada, qtdeObtido);
        }
    }
}
