using System;
namespace Teste_Mesa
{
    public class Problema_3
    {
        public string Act()
        {
            Console.WriteLine("Qual o valor atual?");
            double valorPresente = double.Parse(Console.ReadLine());
            Console.WriteLine("Qual a taxa de juros anual?");
            double taxaJuros = double.Parse(Console.ReadLine());
            Console.WriteLine("Qual o periodo em anos?");
            int periodoAnos = int.Parse(Console.ReadLine());
            double percentualJuros = (1 + (taxaJuros / 100));
            double valorFuturo = valorPresente * Math.Pow(percentualJuros, periodoAnos);
            Console.WriteLine("O valor futuro(resultado do investimento) é: " + valorFuturo.ToString("C"));
            return (
                ("\n------------------------------------------------------------") +
                ("\n| Valor Presente | Taxa de Juros | Periodo (meses) | Valor Futuro |") +
                ("\n------------------------------------------------------------") +
               ("\n| " + valorPresente.ToString("C").PadLeft(14, ' ') + " | " + taxaJuros.ToString("F2").PadLeft(12, ' ') + "% | " + periodoAnos.ToString().PadLeft(15, ' ') + " | " + valorFuturo.ToString("C").PadLeft(12, ' ') + " |") +
                ("\n------------------------------------------------------------")); ;
        }
        public void Table()
        {
            Console.WriteLine(Act());


        }

    }
}