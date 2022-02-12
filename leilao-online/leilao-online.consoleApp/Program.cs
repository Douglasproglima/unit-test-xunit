using LeilaoOnline.Core;
using System;

namespace LeilaoOnline.consoleApp
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
            
            leilao.ReceberLance(paulo, 850);
            
            //Act - Método sob teste
            leilao.TerminarPregao();

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

            leilao.ReceberLance(paulo, 100);
            leilao.ReceberLance(maria, 998);
            leilao.ReceberLance(douglas, 1075);
            leilao.ReceberLance(maria, 990);

            //Act - Método sob teste
            leilao.TerminarPregao();

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
