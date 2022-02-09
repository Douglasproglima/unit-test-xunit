using leilao_online.core;
using System;

namespace leilao_online.consoleApp
{
    internal class Program
    {
        private static void ValidarValores(double valorEsperado, double valorObtido)
        {
            bool aprovado = valorEsperado == valorObtido;

            Console.WriteLine($"Aprovado {aprovado} | Vlr. Esperado: {valorEsperado} | Vlr. Obtido: {valorObtido}");
        }


        private static void TesteLeilaoComApenasUmLances()
        {
            //Arrange - Cenário
            var leilao = new Leilao("Picasso");
            var paulo = new Interessada("Paulo", leilao);
            
            leilao.RecebeLance(paulo, 850);
            
            //Act - Método sob teste
            leilao.TerminaPregao();

            //Assert
            var valorEsperado = 1000;
            var valorObtido = leilao.Ganhador.Valor;

            ValidarValores(valorEsperado, valorObtido);
        }


        private static void TesteLeilaoComVariosLances()
        {
            //Arrange - Cenário
            var leilao = new Leilao("Picasso");
            var paulo = new Interessada("Paulo", leilao);
            var maria = new Interessada("Maria", leilao);
            var douglas = new Interessada("Douglas", leilao);

            leilao.RecebeLance(paulo, 100);
            leilao.RecebeLance(maria, 1050);
            leilao.RecebeLance(douglas, 1075);
            leilao.RecebeLance(maria, 990);

            //Act - Método sob teste
            leilao.TerminaPregao();

            //Assert
            var valorEsperado = 1000;
            var valorObtido = leilao.Ganhador.Valor;
            ValidarValores(valorEsperado, valorObtido);
        }

        static void Main(string[] args)
        {
            TesteLeilaoComVariosLances();
            TesteLeilaoComApenasUmLances();
        }
    }
}
