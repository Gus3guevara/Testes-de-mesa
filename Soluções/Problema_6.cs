using System;
using System.Globalization;

namespace Teste_de_Mesa
{
    public class Problema_6
    {
        private decimal _valorPresente;
        private double _taxa;
        private int _meses;
        private int _dias;
        private DateTime _dataInicio;

        public decimal ValorPresente
        {
            get => _valorPresente;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("O valor presente investido deve ser maior do que zero.");
                _valorPresente = value;
            }
        }

        public double Taxa
        {
            get => _taxa;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("A taxa de juros deve ser maior do que zero.");
                _taxa = value;
            }
        }

        public int Meses
        {
            get => _meses;
            set
            {
                if (value < 0)
                    throw new ArgumentException("A quantidade de meses não pode ser negativa.");
                _meses = value;
            }
        }

        public int Dias
        {
            get => _dias;
            set
            {
                if (value < 0 || value >= 30)
                    throw new ArgumentException("A quantidade de dias deve estar entre 0 e 29.");
                _dias = value;
            }
        }

        public DateTime DataInicio
        {
            get => _dataInicio;
            set => _dataInicio = value;
        }

        public Problema_6(decimal valorPresente, double taxa, int meses, int dias, DateTime dataInicio)
        {
            ValorPresente = valorPresente;
            Taxa = taxa;
            Meses = meses;
            Dias = dias;
            DataInicio = dataInicio;
        }

        public void GerarTabela()
        {
            Console.WriteLine(new string('-', 122));
            Console.WriteLine("| {0,-12} | {1,-14} | {2,-10} | {3,-10} | {4,-14} | {5,-13} | {6,-14} | {7,-14} |",
                "Data (dd/MM)", "Vl. Investido", "Taxa Juros", "Período", "Valor Futuro", "Rendimento", "Renda Líquida", "Saldo Conta");
            Console.WriteLine(new string('-', 122));

            // Período 0
            decimal valorFuturoZero = ValorPresente;
            decimal rendimentoZero = 0m;
            ImprimirLinha(DataInicio, ValorPresente, Taxa, "0", valorFuturoZero, rendimentoZero);

            // Loop mês a mês
            for (int m = 1; m <= Meses; m++)
            {
                double fator = Math.Pow(1 + Taxa, m);
                decimal valorFuturo = ValorPresente * (decimal)fator;
                decimal rendimentoAcumulado = valorFuturo - ValorPresente;

                // Calcula a data somando os meses correspondentes
                DateTime dataCorrente = DataInicio.AddMonths(m);

                ImprimirLinha(dataCorrente, ValorPresente, Taxa, m.ToString(), valorFuturo, rendimentoAcumulado);
            }

            // Período final com dias adicionais
            if (Dias > 0)
            {
                double periodoTotal = Meses + (Dias / 30.0);
                double fatorFinal = Math.Pow(1 + Taxa, periodoTotal);

                decimal valorFuturoFinal = ValorPresente * (decimal)fatorFinal;
                decimal rendimentoFinal = valorFuturoFinal - ValorPresente;

                // Calcula a data exata somando meses e dias
                DateTime dataFinal = DataInicio.AddMonths(Meses).AddDays(Dias);

                string labelPeriodo = $"{Meses}m e {Dias}d";
                ImprimirLinha(dataFinal, ValorPresente, Taxa, labelPeriodo, valorFuturoFinal, rendimentoFinal);
            }

            Console.WriteLine(new string('-', 122));
        }

        private void ImprimirLinha(DateTime data, decimal vp, double taxa, string periodo, decimal vf, decimal rendimento)
        {
            decimal rendaLiquida = vf;
            decimal saldoConta = vf;

            // Formatação manual segura para fugir da dependência de CultureInfo
            string dataFormatada = data.ToString("dd/MM/yyyy"); // Explicitamente Dia/Mês/Ano
            string vpStr = "R$ " + vp.ToString("N2");
            string taxaStr = (taxa * 100).ToString("F2") + "%";
            string vfStr = "R$ " + vf.ToString("N2");
            string rendimentoStr = "R$ " + rendimento.ToString("N2");
            string rendaLiquidaStr = "R$ " + rendaLiquida.ToString("N2");
            string saldoContaStr = "R$ " + saldoConta.ToString("N2");

            Console.WriteLine("| {0,-12} | {1,-14} | {2,-10} | {3,-10} | {4,-14} | {5,-13} | {6,-14} | {7,-14} |",
                dataFormatada, vpStr, taxaStr, periodo, vfStr, rendimentoStr, rendaLiquidaStr, saldoContaStr);
        }
    }
}