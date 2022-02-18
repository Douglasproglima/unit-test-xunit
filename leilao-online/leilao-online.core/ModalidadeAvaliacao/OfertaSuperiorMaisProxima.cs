using LeilaoOnline.Core.Interface;
using System.Linq;

namespace LeilaoOnline.Core.ModalidadeAvaliacao
{
    public class OfertaSuperiorMaisProxima : IModalidadeAvaliacao
    {
        public double ValorDestino { get; private set; }
        public OfertaSuperiorMaisProxima(double valorDestino)
        {
            ValorDestino = valorDestino;
        }

        public Lance Avaliar(Leilao leilao)
        {
            return leilao.Lances
                    .DefaultIfEmpty(new Lance(null, 0))
                    .Where(lance => lance.Valor > ValorDestino)
                    .OrderBy(lance => lance.Valor)
                    .FirstOrDefault();
        }
    }
}
