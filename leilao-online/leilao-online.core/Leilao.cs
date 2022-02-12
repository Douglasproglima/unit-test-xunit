using System.Linq;
using System.Collections.Generic;

namespace LeilaoOnline.Core
{
    public class Leilao
    {
        public enum StatusLeilao
        {
            ANTES_PREGAO,
            EM_ANDAMENTO,
            FINALIZADO
        }

        private IList<Lance> _lances;
        public IEnumerable<Lance> Lances => _lances;
        public string Peca { get; }
        public Lance Ganhador { get; private set; }
        public StatusLeilao Status { get; private set; }

        public Leilao(string peca)
        {
            Peca = peca;
            _lances = new List<Lance>();
            Status = StatusLeilao.ANTES_PREGAO;
        }

        public void RecebeLance(Interessada cliente, double valor)
        {
            if(Status.Equals(StatusLeilao.EM_ANDAMENTO))
                _lances.Add(new Lance(cliente, valor));
        }

        public void IniciaPregao()
        {
            Status = StatusLeilao.EM_ANDAMENTO;
        }

        public void TerminarPregao()
        {
            Status = StatusLeilao.FINALIZADO;
            Ganhador = Lances
                .DefaultIfEmpty(new Lance(null, 0))
                .OrderBy(lance => lance.Valor)
                .LastOrDefault();
        }
    }
}
