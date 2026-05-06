using System;
using Teste_Mesa;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Teste de Mesa");
        Console.WriteLine("-------------");
        Console.WriteLine("Qual teste deseja executar? (1, 2 ou 3)");
        string input = Console.ReadLine();
        switch (input)
        {
            case "1":
                new Teste_Mesa_1().Functionality();
                break;
            case "2":
                new Teste_Mesa_2().Functionality();
                break;
            case "3":
                new Teste_Mesa_3().Functionality();
                break;
            default:
                Console.WriteLine("Opção não encontrada");
                break;
        }

    }
}

