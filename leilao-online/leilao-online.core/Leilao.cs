using System.Linq;
using System.Collections.Generic;
using System;
using LeilaoOnline.Core.Interface;

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
        private IModalidadeAvaliacao _avaliador;

        public IEnumerable<Lance> Lances => _lances;
        public string Peca { get; }
        public Lance Ganhador { get; private set; }
        public StatusLeilao Status { get; private set; }

        public Leilao(string peca, IModalidadeAvaliacao avaliador)
        {
            Peca = peca;
            _lances = new List<Lance>();
            Status = StatusLeilao.ANTES_PREGAO;
            _avaliador = avaliador;
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

            Ganhador = _avaliador.Avaliar(this);

            Status = StatusLeilao.FINALIZADO;
        }
    }
}
