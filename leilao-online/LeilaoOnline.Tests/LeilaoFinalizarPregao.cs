using LeilaoOnline.Core;
using System.Collections;
using System.Collections.Generic;

namespace LeilaoOnline.Tests
{
    public class LeilaoFinalizarPregao : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            var leilaoCenario1 = new Leilao("Peça");
            var lancesCenario1 = new List<Lance> {
                new Lance(new Interessada(string.Empty, leilaoCenario1), 800),
                new Lance(new Interessada(string.Empty, leilaoCenario1), 900),
                new Lance(new Interessada(string.Empty, leilaoCenario1), 1_000),
                new Lance(new Interessada(string.Empty, leilaoCenario1), 1_100)
            };

            yield return new object[] {
                lancesCenario1[3],
                leilaoCenario1,
                lancesCenario1
            };

            Leilao leilaoCenario2 = new Leilao("Peça");
            var lancesCenario2 = new List<Lance> {
                new Lance(new Interessada(string.Empty, leilaoCenario2), 800),
                new Lance(new Interessada(string.Empty, leilaoCenario2), 900),
                new Lance(new Interessada(string.Empty, leilaoCenario2), 1_000),
                new Lance(new Interessada(string.Empty, leilaoCenario2), 700)
            };

            yield return new object[] {
                lancesCenario2[2],
                leilaoCenario2,
                lancesCenario2
            };

            Leilao leilaoCenario3 = new Leilao("Peça");
            var lancesCenario3 = new List<Lance> {
                new Lance(new Interessada(string.Empty, leilaoCenario3), 800)
            };

            yield return new object[] {
                lancesCenario3[0],
                leilaoCenario3,
                lancesCenario3
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}