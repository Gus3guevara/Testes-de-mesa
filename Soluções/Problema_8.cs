using System;
using System.Globalization;

namespace Teste_de_Mesa
{
    public class Problema_8
    {
        private decimal _valorPresente;
        private double _taxa;
        private int _meses;
        private int _dias;
        private DateTime _dataInicio;
        private int _mesResgate;

        public decimal ValorPresente { get => _valorPresente; set => _valorPresente = value; }
        public double Taxa { get => _taxa; set => _taxa = value; }
        public int Meses { get => _meses; set => _meses = value; }
        public int Dias { get => _dias; set => _dias = value; }
        public DateTime DataInicio { get => _dataInicio; set => _dataInicio = value; }
        public int MesResgate { get => _mesResgate; set => _mesResgate = value; }

        public Problema_8(decimal valorPresente, double taxa, int meses, int dias, DateTime dataInicio, int mesResgate)
        {
            if (valorPresente <= 0) throw new ArgumentException("O valor investido deve ser maior que zero.");
            if (taxa <= 0) throw new ArgumentException("A taxa de juros deve ser maior que zero.");
            if (mesResgate < 1 || mesResgate > meses) throw new ArgumentException("O período do resgate deve estar contido nos meses do investimento.");

            ValorPresente = valorPresente;
            Taxa = taxa;
            Meses = meses;
            Dias = dias;
            DataInicio = dataInicio;
            MesResgate = mesResgate;
        }

        public void Table()
        {
            // Cabeçalho da Tabela estilizada para o Problema 8
            Console.WriteLine(new string('-', 124));
            Console.WriteLine("| {0,-14} | {1,-10} | {2,-14} | {3,-14} | {4,-14} | {5,-14} | {6,-16} |",
                "Vl. Investido", "Taxa Juros", "Rendimento", "Período", "Resgate", "Saldo Líquido", "Data Prevista");
            Console.WriteLine(new string('-', 124));

            decimal valorAtivo = ValorPresente;
            int mesesAposResgate = 0;

            // Período 0
            ImprimirLinha(ValorPresente, Taxa, 0m, "0", 0m, ValorPresente, DataInicio);

            for (int m = 1; m <= Meses; m++)
            {
                DateTime dataCorrente = DataInicio.AddMonths(m);
                decimal rendimentoCalculado, valorFuturo, resgate = 0m, saldoLiquido;

                if (m <= MesResgate)
                {
                    double fator = Math.Pow(1 + Taxa, m);
                    valorFuturo = ValorPresente * (decimal)fator;
                    rendimentoCalculado = valorFuturo - ValorPresente;
                    saldoLiquido = valorFuturo;

                    if (m == MesResgate)
                    {
                        resgate = rendimentoCalculado;
                        saldoLiquido = valorFuturo - resgate; // Subtrai todo o rendimento do saldo
                        valorAtivo = saldoLiquido; // Altera o principal para o fluxo seguinte
                        mesesAposResgate = 0;
                    }

                    ImprimirLinha(ValorPresente, Taxa, rendimentoCalculado, m.ToString(), resgate, saldoLiquido, dataCorrente);
                }
                else
                {
                    mesesAposResgate++;
                    double fator = Math.Pow(1 + Taxa, mesesAposResgate);
                    valorFuturo = valorAtivo * (decimal)fator;
                    rendimentoCalculado = valorFuturo - valorAtivo;
                    saldoLiquido = valorFuturo;

                    ImprimirLinha(valorAtivo, Taxa, rendimentoCalculado, m.ToString(), 0m, saldoLiquido, dataCorrente);
                }
            }

            // Cálculo dos dias proporcionais remanescentes
            if (Dias > 0)
            {
                DateTime dataFinal = DataInicio.AddMonths(Meses).AddDays(Dias);
                int totalMesesAposResgate = (Meses - MesResgate);
                double periodoTotalAposResgate = totalMesesAposResgate + (Dias / 30.0);

                double fatorFinal = Math.Pow(1 + Taxa, periodoTotalAposResgate);
                decimal valorFuturoFinal = valorAtivo * (decimal)fatorFinal;
                decimal rendimentoFinal = valorFuturoFinal - valorAtivo;

                string labelPeriodo = $"{Meses}m e {Dias}d";
                ImprimirLinha(valorAtivo, Taxa, rendimentoFinal, labelPeriodo, 0m, valorFuturoFinal, dataFinal);
            }

            Console.WriteLine(new string('-', 124));
        }

        private void ImprimirLinha(decimal vp, double taxa, decimal rendimento, string periodo, decimal resgate, decimal saldoLiquido, DateTime data)
        {
            string vpStr = "R$ " + vp.ToString("N2");
            string taxaStr = (taxa * 100).ToString("F2") + "%";
            string rendimentoStr = "R$ " + rendimento.ToString("N2");
            string resgateStr = "R$ " + resgate.ToString("N2");
            string saldoLiquidoStr = "R$ " + saldoLiquido.ToString("N2");
            string dataStr = data.ToString("dd/MM/yyyy");

            Console.WriteLine("| {0,-14} | {1,-10} | {2,-14} | {3,-14} | {4,-14} | {5,-14} | {6,-16} |",
                vpStr, taxaStr, rendimentoStr, periodo, resgateStr, saldoLiquidoStr, dataStr);
        }
    }
}