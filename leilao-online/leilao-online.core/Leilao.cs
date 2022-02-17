using System.Linq;
using System.Collections.Generic;
using System;

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
        public double ValorDestino { get; }

        public Leilao(string peca, double valorDestino = 0)
        {
            Peca = peca;
            _lances = new List<Lance>();
            Status = StatusLeilao.ANTES_PREGAO;
            ValorDestino = valorDestino;
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
            if (Status != StatusLeilao.EM_ANDAMENTO)
                throw new InvalidOperationException("Não é possível terminar o pregão sem ter iniciado.");

            if (ValorDestino > 0)
            {
                //Modalidade de lance superior mais proxima
                Ganhador = Lances
                    .DefaultIfEmpty(new Lance(null, 0))
                    .Where(lance => lance.Valor > ValorDestino)
                    .OrderBy(lance => lance.Valor)
                    .FirstOrDefault();
            }
            else
            { 
                //Modalide de maior valor
                Ganhador = Lances
                    .DefaultIfEmpty(new Lance(null, 0))
                    .OrderBy(lance => lance.Valor)
                    .LastOrDefault();
            }

            Status = StatusLeilao.FINALIZADO;
        }
    }
}
