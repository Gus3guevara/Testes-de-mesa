using System;
namespace Teste_Mesa
{
    public class Problema_2
    {
        public string Act(double valorPresente, double taxaJuros, int periodoMes, double valorFuturo)
        {
            Console.WriteLine("");

            return ("| " + valorPresente.ToString("C").PadLeft(14, ' ') + " | " + taxaJuros.ToString("F2").PadLeft(12, ' ') + "% | " + periodoMes.ToString().PadLeft(15, ' ') + " | " + valorFuturo.ToString("C").PadLeft(12, ' ') + " |");
        }
        public void Calculo(bool verifier = false)
        {
            double valorPresente = 3800D, taxaJuros = 1.25D, percentualJuros;
            int periodoMes = 6;
            percentualJuros = (1 + (taxaJuros / 100));
            for (int i = 0; i < periodoMes; i++)
            {
                if (verifier == true)
                {
                    Console.WriteLine("Deseja retirar?");
                    Console.WriteLine("1-Sim 2-Não");
                    int opcao = int.Parse(Console.ReadLine());
                    switch (opcao)
                    {
                        case 1:
                            Console.WriteLine("qual o valor?");
                            double valorRetirada = double.Parse(Console.ReadLine());
                            valorPresente -= valorRetirada;
                            break;

                        case 2:
                            Console.WriteLine("Continuando o processo...");
                            break;
                        default:
                            Console.WriteLine("Opção invalida, tente novamente.");

                            break;
                    }
                }
                double valorFuturo = valorPresente * Math.Pow(percentualJuros, i);
                Console.WriteLine(Act(valorPresente, taxaJuros, i + 1, valorFuturo));
            }

        }
        public void Table()
        {
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("| Valor Presente | Taxa de Juros | Periodo (meses) | Valor Futuro |");
            Console.WriteLine("------------------------------------------------------------");
            Calculo(false);
            Console.WriteLine("------------------------------------------------------------");

        }

    }
}