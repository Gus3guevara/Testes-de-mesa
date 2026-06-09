using System;
using Teste_Mesa;

public class Program
{

    public void Act()
    {
        Console.WriteLine("Qual teste de mesa deseja executar? (1, 2, 3 ou 4)");
        Console.WriteLine("informe o numero do problema;");
        int problema = int.Parse(Console.ReadLine());
        switch (problema)
        {
            case 1:

                Console.WriteLine("Problema 1:");
                new Problema_1().Table();
                break;
            case 2:
                Console.WriteLine("\nProblema 2:");
                new Problema_2().Table();
                break;
            case 3:
                Console.WriteLine("\nProblema 3:");
                new Problema_3().Table();
                break;
            case 4:
                Console.WriteLine("\nProblema 4:");
                new Problema_4().Model();
                break;
            default:
                Console.WriteLine("Opção invalida, tente novamente.");
                break;
        }
    }
    public static void Main(string[] args)
    {
        string resposta;
        do
        {
            new Program().Act();
            Console.WriteLine("Deseja realizar outro teste de mesa? (S/N)");
            resposta = Console.ReadLine().ToUpper();
        }
        while (resposta == "S");

    }
}

