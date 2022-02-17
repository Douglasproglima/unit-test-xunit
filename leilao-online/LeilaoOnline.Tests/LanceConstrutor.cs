using LeilaoOnline.Core;
using System;
using Xunit;

namespace LeilaoOnline.Tests
{
    public class LanceConstrutor
    {
        /* Give: Dado leilão com lance negativo
         * When: Quando o pregão/leilão estiver iniciado ou em andamento
         * Then: Então gera exceção
         */
        [Fact]
        public void LancaArgumentExceptionDadoValorNegativo()
        {
            //Arrange/Given - Cenário
            var valorNegativo = -200;

            //Act/When - Método sob teste
            Assert.Throws<ArgumentException>( () =>
                //Assert/Then
                new Lance(null, valorNegativo)
            );
        }
    }
}
