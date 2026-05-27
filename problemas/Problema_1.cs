using System;
namespace Teste_Mesa
{
    public class Problema_1
    {
        public string Act()
        {
            Console.WriteLine("");
            double valorPresente = 1000D, taxaJuros = 5.3D, percentualJuros;
            int periodoMes = 6;
            percentualJuros = (1 + (taxaJuros / 100));
            double valorFuturo = valorPresente * Math.Pow(percentualJuros, periodoMes);

            return ("| " + valorPresente.ToString("C").PadLeft(14, ' ') + " | " + taxaJuros.ToString("F2").PadLeft(12, ' ') + "% | " + periodoMes.ToString().PadLeft(15, ' ') + " | " + valorFuturo.ToString("C").PadLeft(12, ' ') + " |");
        }
        public void Table()
        {
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("| Valor Presente | Taxa de Juros | Periodo (meses) | Valor Futuro |");
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine(Act());
            Console.WriteLine("------------------------------------------------------------");

        }
    }
}
