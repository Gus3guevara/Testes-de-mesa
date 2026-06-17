using System;
using System.Globalization;

namespace Teste_de_Mesa
{
    public class Problema_7
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

        public Problema_7(decimal valorPresente, double taxa, int meses, int dias, DateTime dataInicio, int mesResgate)
        {
            if (valorPresente <= 0) throw new ArgumentException("O valor presente deve ser maior que zero.");
            if (taxa <= 0) throw new ArgumentException("A taxa de juros deve ser maior que zero.");
            if (mesResgate < 1 || mesResgate > meses) throw new ArgumentException("O mês do resgate deve ser coerente com o período total.");

            ValorPresente = valorPresente;
            Taxa = taxa;
            Meses = meses;
            Dias = dias;
            DataInicio = dataInicio;
            MesResgate = mesResgate;
        }

        public void Table()
        {
            // Tabela expandida para contemplar as colunas solicitadas de resgate
            Console.WriteLine(new string('-', 146));
            Console.WriteLine("| {0,-12} | {1,-14} | {2,-10} | {3,-10} | {4,-14} | {5,-13} | {6,-12} | {7,-16} | {8,-16} |",
                "Data (dd/MM)", "Vl. Investido", "Taxa Juros", "Período", "Valor Futuro", "Rendimento", "Resgate", "Sld. Líq. Rest.", "Rend. Restante");
            Console.WriteLine(new string('-', 146));

            decimal valorInvestidoAtivo = ValorPresente;
            decimal rendimentoAcumulado = 0m;
            decimal saldoAcumulado = ValorPresente;
            int mesesAposResgate = 0;

            // Período 0
            ImprimirLinha(DataInicio, valorInvestidoAtivo, Taxa, "0", valorInvestidoAtivo, 0m, 0m, valorInvestidoAtivo, 0m);

            for (int m = 1; m <= Meses; m++)
            {
                DateTime dataCorrente = DataInicio.AddMonths(m);
                decimal valorFuturo, rendimentoPeriodo, resgate = 0m, saldoLiquidoRestante, rendimentoRestante;

                if (m <= MesResgate)
                {
                    // Cálculo normal até o mês do resgate
                    double fator = Math.Pow(1 + Taxa, m);
                    valorFuturo = ValorPresente * (decimal)fator;
                    rendimentoPeriodo = valorFuturo - ValorPresente;
                    saldoLiquidoRestante = valorFuturo;
                    rendimentoRestante = rendimentoPeriodo;

                    if (m == MesResgate)
                    {
                        // Resgata TODO o rendimento acumulado no 5º mês
                        resgate = rendimentoPeriodo;
                        saldoLiquidoRestante = valorFuturo - resgate; // Volta a ser o valor principal inicial (P)
                        rendimentoRestante = 0m;

                        // Atualiza as variáveis de estado para reiniciar a capitalização
                        valorInvestidoAtivo = saldoLiquidoRestante;
                        saldoAcumulado = saldoLiquidoRestante;
                        rendimentoAcumulado = 0m;
                        mesesAposResgate = 0;
                    }

                    ImprimirLinha(dataCorrente, ValorPresente, Taxa, m.ToString(), valorFuturo, rendimentoPeriodo, resgate, saldoLiquidoRestante, rendimentoRestante);
                }
                else
                {
                    // Compõe a partir do novo saldo ativo do 5º mês
                    mesesAposResgate++;
                    double fator = Math.Pow(1 + Taxa, mesesAposResgate);
                    valorFuturo = valorInvestidoAtivo * (decimal)fator;
                    rendimentoPeriodo = valorFuturo - valorInvestidoAtivo;
                    saldoLiquidoRestante = valorFuturo;
                    rendimentoRestante = rendimentoPeriodo;

                    ImprimirLinha(dataCorrente, valorInvestidoAtivo, Taxa, m.ToString(), valorFuturo, rendimentoPeriodo, 0m, saldoLiquidoRestante, rendimentoRestante);
                }
            }

            // Período de dias finais calculados proporcionalmente sobre o saldo ativo remanescente
            if (Dias > 0)
            {
                DateTime dataFinal = DataInicio.AddMonths(Meses).AddDays(Dias);
                int totalMesesAposResgate = (Meses - MesResgate);
                double periodoProporcional = totalMesesAposResgate + (Dias / 30.0);

                double fatorFinal = Math.Pow(1 + Taxa, periodoProporcional);
                decimal valorFuturoFinal = valorInvestidoAtivo * (decimal)fatorFinal;
                decimal rendimentoFinal = valorFuturoFinal - valorInvestidoAtivo;

                string labelPeriodo = $"{Meses}m e {Dias}d";
                ImprimirLinha(dataFinal, valorInvestidoAtivo, Taxa, labelPeriodo, valorFuturoFinal, rendimentoFinal, 0m, valorFuturoFinal, rendimentoFinal);
            }

            Console.WriteLine(new string('-', 146));
        }

        private void ImprimirLinha(DateTime data, decimal vp, double taxa, string periodo, decimal vf, decimal rendimento, decimal resgate, decimal saldoLiquido, decimal rendimentoRestante)
        {
            string dataFormatada = data.ToString("dd/MM/yyyy");
            string vpStr = "R$ " + vp.ToString("N2");
            string taxaStr = (taxa * 100).ToString("F2") + "%";
            string vfStr = "R$ " + vf.ToString("N2");
            string rendimentoStr = "R$ " + rendimento.ToString("N2");
            string resgateStr = "R$ " + resgate.ToString("N2");
            string saldoLiquidoStr = "R$ " + saldoLiquido.ToString("N2");
            string rendRestanteStr = "R$ " + rendimentoRestante.ToString("N2");

            Console.WriteLine("| {0,-12} | {1,-14} | {2,-10} | {3,-10} | {4,-14} | {5,-13} | {6,-12} | {7,-16} | {8,-16} |",
                dataFormatada, vpStr, taxaStr, periodo, vfStr, rendimentoStr, resgateStr, saldoLiquidoStr, rendRestanteStr);
        }
    }
}