using System.Linq;
using System.Collections.Generic;

namespace LeilaoOnline.Core
{
    /*CICLO TDD - Test Driven Domain
     * 1 - Novo Teste
     * 2 - Falha do Teste
     * 3 - Correção do teste(Em produção ou cenário)
     * 4 - Teste Success
     * 5 - Refatorar
     */

    public class Leilao
    {
        public enum StatusLeilao
        {
            ANTES_PREGAO,
            EM_ANDAMENTO,
            FINALIZADO
        }

        private Interessada _ultimoCliente;
        private IList<Lance> _lances;
        public IEnumerable<Lance> Lances => _lances;
        public string Peca { get; }
        public Lance Ganhador { get; private set; }
        public StatusLeilao Status { get; private set; }

        public Leilao(string peca)
        {
            Peca = peca;
            _lances = new List<Lance>();
            //Status = StatusLeilao.EM_ANDAMENTO;
            Status = StatusLeilao.ANTES_PREGAO;
        }

        private bool ProximoLanceEhValido(Interessada cliente, double valor)
        {
            bool valido = (StatusLeilao.EM_ANDAMENTO.Equals(Status)) &&
                          (cliente != _ultimoCliente);

            return valido;
        }

        public void ReceberLance(Interessada cliente, double valor)
        {
            if (this.ProximoLanceEhValido(cliente, valor))
            {
                _lances.Add(new Lance(cliente, valor));
                _ultimoCliente = cliente;
            }
        }

        public void IniciarPregao()
        {
            Status = StatusLeilao.EM_ANDAMENTO;
        }

        public void TerminarPregao()
        {
            Ganhador = Lances
                .DefaultIfEmpty(new Lance(null, 0))
                .OrderBy(lance => lance.Valor)
                .LastOrDefault();

            Status = StatusLeilao.FINALIZADO;
        }
    }
}
