using System;
namespace Teste_Mesa
{
    public class Problema_4
    {
        public string Act(double valorPresente, double taxaJuros, int periodoMes, double rendimento, double rendaLiquida, double rendaAcumulada, double saque, double saldo)
        {
            string txtSaque = saque > 0 ? saque.ToString("C") : "";

            return "| " + valorPresente.ToString().PadLeft(14, ' ') +
                   " | " + (taxaJuros.ToString("F2") + "%").PadLeft(13, ' ') +
                   " | " + periodoMes.ToString().PadLeft(15, ' ') +
                   " | " + rendimento.ToString("C").PadLeft(14, ' ') +
                   " | " + rendaLiquida.ToString("C").PadLeft(14, ' ') +
                   " | " + rendaAcumulada.ToString("C").PadLeft(15, ' ') +
                   " | " + txtSaque.PadLeft(11, ' ') +
                   " | " + saldo.ToString("C").PadLeft(14, ' ') + " |";
        }

        public void Model(bool verifier = false)
        {
            double valorPresente = 0;
            while (true)
            {
                Console.WriteLine("Qual o valor atual?");
                string input = Console.ReadLine();
                if (double.TryParse(input, out valorPresente) && valorPresente >= 0)
                {
                    break;
                }
                Console.WriteLine("Valor inválido ou nulo. Tente novamente.");
            }

            double taxaJuros = 0;
            while (true)
            {
                Console.WriteLine("Qual a taxa de juros (em %)?");
                string input = Console.ReadLine();
                if (double.TryParse(input, out taxaJuros) && taxaJuros >= 0)
                {
                    break;
                }
                Console.WriteLine("Taxa inválida ou nula. Tente novamente.");
            }

            int periodoIcognita = 0;
            while (true)
            {
                Console.WriteLine("Qual o periodo de tempo da simulação?");
                string input = Console.ReadLine();
                if (int.TryParse(input, out periodoIcognita) && periodoIcognita > 0)
                {
                    break;
                }
                Console.WriteLine("Período inválido. Digite um número inteiro maior que zero.");
            }

            int opcao_0 = 0;
            int periodoMes = 0;
            int periodoAnos = 0;
            while (true)
            {
                Console.WriteLine("Em meses ou anos? 1-Meses 2-Anos");
                string input = Console.ReadLine();
                if (int.TryParse(input, out opcao_0) && (opcao_0 == 1 || opcao_0 == 2))
                {
                    switch (opcao_0)
                    {
                        case 1:
                            periodoMes = periodoIcognita;
                            periodoAnos = periodoMes / 12;
                            break;
                        case 2:
                            periodoAnos = periodoIcognita;
                            periodoMes = periodoAnos * 12;
                            break;
                    }
                    break;
                }
                Console.WriteLine("Opção inválida, tente novamente.");
            }

            int opcaoRetirada = 0;
            while (true)
            {
                Console.WriteLine("Deseja retirar durante o processo? 1-Sim 2-Não");
                string input = Console.ReadLine();
                if (int.TryParse(input, out opcaoRetirada) && (opcaoRetirada == 1 || opcaoRetirada == 2))
                {
                    break;
                }
                Console.WriteLine("Opção inválida, tente novamente.");
            }

            double valorRetirada = 0;
            int periodoRetirada = -1;

            switch (opcaoRetirada)
            {
                case 1:
                    verifier = true;
                    while (true)
                    {
                        Console.WriteLine("Qual o valor?");
                        string input = Console.ReadLine();
                        if (double.TryParse(input, out valorRetirada) && valorRetirada > 0)
                        {
                            break;
                        }
                        Console.WriteLine("Valor de saque inválido. Digite um valor numérico positivo.");
                    }

                    DateTime timeRetirada = DateTime.MinValue;
                    while (true)
                    {
                        Console.WriteLine("Quando deseja retirar? (a simulação considera o dia de hoje como inicio)");
                        string input = Console.ReadLine();

                        if (string.IsNullOrWhiteSpace(input))
                        {
                            Console.WriteLine("A data não pode ser vazia ou nula. Tente novamente.");
                            continue;
                        }

                        if (DateTime.TryParse(input, out timeRetirada))
                        {
                            periodoRetirada = ((timeRetirada.Year - CalendarioAno()) * 12) + timeRetirada.Month - CalendarioMes();

                            // Permite saque a partir do mês 0 até o limite da simulação
                            if (periodoRetirada < 0 || periodoRetirada > periodoMes)
                            {
                                Console.WriteLine("A data do saque deve estar dentro do intervalo da simulação. Tente novamente.");
                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Formato de data inválido. Tente novamente.");
                        }
                    }
                    break;

                case 2:
                    verifier = false;
                    break;
            }

            // Exibição do Cabeçalho atualizado
            Console.WriteLine("\n-----------------------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("| Valor Presente | Taxa de Juros | Periodo de a.m. |   Rendimento   | Renda Líquida  | Renda Acumulada |   Resgate   |     SALDO      |");
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------------");

            double saldoAtual = valorPresente;

            // O loop agora começa em 0 para gerar a linha do estado inicial
            for (int i = 0; i <= periodoMes; i++)
            {
                double rendimento = 0;
                double rendaLiquida = 0;
                double rendaAcumulada = 0;
                double saqueDoMes = 0;

                if (i == 0)
                {
                    rendimento = valorPresente;
                    rendaLiquida = 0;
                    rendaAcumulada = valorPresente;
                }
                else
                {
                    // Cálculo dos juros mensais baseados no saldo do mês anterior
                    rendaLiquida = saldoAtual * (taxaJuros / 100);
                    rendimento = saldoAtual + rendaLiquida;
                    rendaAcumulada = rendimento;
                }

                // Processamento de saque
                if (verifier && i == periodoRetirada)
                {
                    if (rendaAcumulada >= valorRetirada)
                    {
                        saqueDoMes = valorRetirada;
                        saldoAtual = rendaAcumulada - valorRetirada;
                    }
                    else
                    {
                        saqueDoMes = rendaAcumulada;
                        saldoAtual = 0;
                    }
                    verifier = false; // Desativa saques adicionais
                }
                else
                {
                    saldoAtual = rendaAcumulada;
                }

                // Imprime a linha com os novos campos
                Console.WriteLine(Act(valorPresente, taxaJuros, i, rendimento, rendaLiquida, rendaAcumulada, saqueDoMes, saldoAtual));

                if (saldoAtual <= 0 && i == periodoRetirada)
                {
                    Console.WriteLine("=> O saldo foi totalmente resgatado. Simulação finalizada.");
                    break;
                }
            }
        }

        public void Table()
        {
            Model(false);
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------------");
        }

        public int CalendarioMes()
        {
            return DateTime.Now.Month;
        }

        public int CalendarioAno()
        {
            return DateTime.Now.Year;
        }
    }
}