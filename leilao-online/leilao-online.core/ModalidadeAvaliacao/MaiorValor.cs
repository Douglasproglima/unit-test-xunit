using LeilaoOnline.Core.Interface;
using System.Linq;

namespace LeilaoOnline.Core.ModalidadeAvaliacao
{
    public class MaiorValor : IModalidadeAvaliacao
    {
        public Lance Avaliar(Leilao leilao)
        {
            return leilao.Lances
                    .DefaultIfEmpty(new Lance(null, 0))
                    .OrderBy(lance => lance.Valor)
                    .LastOrDefault();
        }
    }
}
