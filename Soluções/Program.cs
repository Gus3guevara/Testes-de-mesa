using System;

namespace Teste_de_Mesa
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string resposta;
            do
            {
                new Program().Act();
                Console.WriteLine("\nDeseja realizar outro teste de mesa? (S/N)");
                resposta = Console.ReadLine().ToUpper();
            }
            while (resposta == "S");
        }

        public void Act()
        {
            Console.Clear();
            Console.WriteLine("=================================================================================");
            Console.WriteLine("                         MENU DE TESTES DE MESA (C#)                             ");
            Console.WriteLine("=================================================================================");
            Console.WriteLine("Qual teste de mesa deseja executar? (6, 7 ou 8)");
            Console.Write("Informe o número do problema: ");

            if (int.TryParse(Console.ReadLine(), out int problema))
            {
                switch (problema)
                {
                    case 6:
                        Console.WriteLine("\n>>> Executando o Problema 6 (Simulador de Juros Compostos) <<<\n");
                        ExecutarProblema6();
                        break;
                    case 7:
                        Console.WriteLine("\n>>> Executando o Problema 7 (Resgate no 5º Mês - Cenários Padrão) <<<\n");
                        ExecutarProblema7();
                        break;
                    case 8:
                        Console.WriteLine("\n>>> Executando o Problema 8 (Simulador com Entradas do Usuário) <<<\n");
                        ExecutarProblema8();
                        break;
                    default:
                        Console.WriteLine("Opção inválida, tente novamente.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Entrada inválida. Digite apenas números.");
            }
        }

        // Executa a lógica de simulação do problema 6
        private void ExecutarProblema6()
        {
            Console.WriteLine("Escolha o modo de execução:");
            Console.WriteLine("1 - Executar Cenários do Enunciado (Padrão)");
            Console.WriteLine("2 - Realizar Simulação Personalizada");
            Console.Write("Opção: ");
            string opcao = Console.ReadLine();

            if (opcao == "1")
            {
                decimal[] valoresPresentes = { 1000m, 5500m, 12000m };
                double[] taxas = { 0.03, 0.0248, 0.02 };
                int meses = 8;
                int dias = 10;

                for (int k = 0; k < valoresPresentes.Length; k++)
                {
                    string vpFormatado = "R$ " + valoresPresentes[k].ToString("N2");
                    string taxaFormatada = (taxas[k] * 100).ToString("F2") + "%";

                    Console.WriteLine($"\n>>> Cenário {k + 1}: VP = {vpFormatado} | Taxa = {taxaFormatada} | Período = {meses} meses e {dias} dias <<<\n");
                    try
                    {
                        Problema_6 simulador = new Problema_6(valoresPresentes[k], taxas[k], meses, dias, DateTime.Now);
                        simulador.GerarTabela();
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"Erro de validação: {ex.Message}");
                    }
                }
            }
            else if (opcao == "2")
            {
                try
                {
                    Console.Write("Digite o Valor Presente (VP) em R$: ");
                    decimal vp = decimal.Parse(Console.ReadLine());

                    Console.Write("Digite a Taxa de Juros Mensal em % (ex: 3 para 3%): ");
                    double taxa = double.Parse(Console.ReadLine()) / 100.0;

                    Console.Write("Digite a quantidade de meses: ");
                    int meses = int.Parse(Console.ReadLine());

                    Console.Write("Digite a quantidade de dias adicionais: ");
                    int dias = int.Parse(Console.ReadLine());

                    Problema_6 simulador = new Problema_6(vp, taxa, meses, dias, DateTime.Now);
                    Console.WriteLine("\n>>> RESULTADO DA SUA SIMULAÇÃO <<<\n");
                    simulador.GerarTabela();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro nos dados digitados: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Opção inválida.");
            }
        }

        // Executa a lógica de simulação do problema 7 com cenários predefinidos
        private void ExecutarProblema7()
        {
            decimal[] valoresPresentes = { 1000m, 5500m, 12000m };
            double[] taxas = { 0.03, 0.0248, 0.02 };
            int meses = 8;
            int dias = 10;
            int mesResgate = 5;

            for (int k = 0; k < valoresPresentes.Length; k++)
            {
                string vpFormatado = "R$ " + valoresPresentes[k].ToString("N2");
                string taxaFormatada = (taxas[k] * 100).ToString("F2") + "%";

                Console.WriteLine($"\n>>> Cenário {k + 1} (Com Resgate no 5º Mês): VP = {vpFormatado} | Taxa = {taxaFormatada} | Período = {meses} meses e {dias} dias <<<\n");
                try
                {
                    Problema_7 simulador = new Problema_7(valoresPresentes[k], taxas[k], meses, dias, DateTime.Now, mesResgate);
                    simulador.Table();
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Erro de validação: {ex.Message}");
                }
            }
        }

        // Executa a leitura dinâmica dos dados para o problema 8
        private void ExecutarProblema8()
        {
            try
            {
                Console.Write("Digite o Valor Presente Investido (R$): ");
                decimal vp = decimal.Parse(Console.ReadLine());

                Console.Write("Digite a Taxa de Juros Mensal em % (ex: 2,48): ");
                double taxa = double.Parse(Console.ReadLine()) / 100.0;

                Console.Write("Digite a quantidade total de meses do período: ");
                int meses = int.Parse(Console.ReadLine());

                Console.Write("Digite a quantidade de dias adicionais: ");
                int dias = int.Parse(Console.ReadLine());

                Console.Write("Digite o mês em que ocorrerá o resgate do rendimento acumulado: ");
                int mesResgate = int.Parse(Console.ReadLine());

                Problema_8 simulador = new Problema_8(vp, taxa, meses, dias, DateTime.Now, mesResgate);
                Console.WriteLine("\n>>> TABELA DE EVOLUÇÃO E RESGATE PERSONALIZADO (PROBLEMA 8) <<<\n");
                simulador.Table();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro nos dados digitados: {ex.Message}");
            }
        }
    }
}