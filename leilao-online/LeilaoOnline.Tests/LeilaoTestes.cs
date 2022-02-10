﻿using LeilaoOnline.Core;
using Xunit;
namespace LeilaoOnline.Tests
{
    public class LeilaoTestes
    {
        [Fact]
        public void TesteLeilaoComApenasUmLances()
        {
            //Arrange - Cenário
            var leilao = new Leilao("Picasso");
            var paulo = new Interessada("Paulo", leilao);

            leilao.RecebeLance(paulo, 1000);

            //Act - Método sob teste
            leilao.TerminarPregao();

            //Assert
            var valorEsperado = 1000;
            var valorObtido = leilao.Ganhador.Valor;

            Assert.Equal(valorEsperado, valorObtido);
        }

        [Fact]
        public void TesteLeilaoComVariosLances()
        {
            //Arrange - Cenário
            var leilao = new Leilao("Picasso");
            var paulo = new Interessada("Paulo", leilao);
            var maria = new Interessada("Maria", leilao);
            var douglas = new Interessada("Douglas", leilao);

            leilao.RecebeLance(paulo, 100);
            leilao.RecebeLance(maria, 998);
            leilao.RecebeLance(douglas, 1000);
            leilao.RecebeLance(maria, 990);

            //Act - Método sob teste
            leilao.TerminarPregao();

            //Assert
            var valorEsperado = 1000;
            var valorObtido = leilao.Ganhador.Valor;
            Assert.Equal(valorEsperado, valorObtido);
        }

        [Fact]
        public void TesteLeilaoComQuatroClientes()
        {
            //Arrange - Cenário
            var leilao = new Leilao("Picasso");
            var paulo = new Interessada("Paulo", leilao);
            var maria = new Interessada("Maria", leilao);
            var douglas = new Interessada("Douglas", leilao);

            leilao.RecebeLance(paulo, 100);
            leilao.RecebeLance(maria, 998);
            leilao.RecebeLance(douglas, 1000);
            leilao.RecebeLance(maria, 990);
            leilao.RecebeLance(douglas, 1050);

            //Act - Método sob teste
            leilao.TerminarPregao();

            //Assert
            var valorEsperado = 1050;
            var valorObtido = leilao.Ganhador.Valor;
            
            Assert.Equal(valorEsperado, valorObtido);
            Assert.Equal(douglas, leilao.Ganhador.Cliente);
        }

        [Fact]
        public void TesteLeilaoComLancesOrdenadosPorValor()
        {
            //Arrange - Cenário
            var leilao = new Leilao("Picasso");
            var paulo = new Interessada("Paulo", leilao);
            var maria = new Interessada("Maria", leilao);
            var douglas = new Interessada("Douglas", leilao);

            leilao.RecebeLance(paulo, 100);
            leilao.RecebeLance(maria, 990);
            leilao.RecebeLance(maria, 998);
            leilao.RecebeLance(douglas, 1000);

            //Act - Método sob teste
            leilao.TerminarPregao();

            //Assert
            var valorEsperado = 1000;
            var valorObtido = leilao.Ganhador.Valor;
            Assert.Equal(valorEsperado, valorObtido);
        }
    }
}
