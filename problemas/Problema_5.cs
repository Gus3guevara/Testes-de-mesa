using System;
namespace Teste_Mesa
{
    public class Problema_5
    {
        public string Act()
        {
            Console.WriteLine("");
            double valorPresente, taxaJuros = 1.25D, percentualJuros, valorFuturo = 7390.61D;
            int periodoAno = 2;
            percentualJuros = (1 + (taxaJuros / 100));
            valorPresente = valorFuturo / Math.Pow(percentualJuros, periodoAno * 12);

            return ("| " + valorPresente.ToString("C").PadLeft(14, ' ') + " | " + taxaJuros.ToString("F2").PadLeft(12, ' ') + "% | " + periodoAno.ToString().PadLeft(15, ' ') + " | " + valorFuturo.ToString("C").PadLeft(12, ' ') + " |");
        }
        public void Table()
        {
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("| Valor Presente | Taxa de Juros | Periodo (Anos) | Valor Futuro |");
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine(Act());
            Console.WriteLine("------------------------------------------------------------");
        }
    }
}