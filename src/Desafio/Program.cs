using System;
using Desafio.Services;
using Desafio.Models;
using System.Globalization;
using System.Linq;


namespace Desafio
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\n===== DESAFIO TARGET SISTEMAS =====");
                Console.WriteLine("1. Cálculo de Comissão de Vendedores");
                Console.WriteLine("2. Controle de Estoque");
                Console.WriteLine("3. Cálculo de Juros por Atraso");
                Console.WriteLine("0. Sair");
                Console.Write("Escolha uma opção: ");

                switch (Console.ReadLine())
                {
                    case "1": Desafio1(); break;
                    case "2": Desafio2(); break;
                    case "3": Desafio3(); break;
                    case "0": return;
                    default: Console.WriteLine("Opção inválida. Tente novamente."); break;
                }
            }
        }

        static void Desafio1() 
        {
            Console.WriteLine("\n===== DESAFIO 1: Cálculo de Comissão de Vendedores =====");
            
            var vendas = new List<Venda>
            {
                new Venda { Vendedor = "João Silva", Valor = 1200.50m },
                new Venda { Vendedor = "João Silva", Valor = 950.75m },
                new Venda { Vendedor = "João Silva", Valor = 1800.00m },
                new Venda { Vendedor = "João Silva", Valor = 1400.30m },
                new Venda { Vendedor = "João Silva", Valor = 1100.90m },
                new Venda { Vendedor = "João Silva", Valor = 1550.00m },
                new Venda { Vendedor = "João Silva", Valor = 1700.80m },
                new Venda { Vendedor = "João Silva", Valor = 250.30m },
                new Venda { Vendedor = "João Silva", Valor = 480.75m },
                new Venda { Vendedor = "João Silva", Valor = 320.40m },

                new Venda { Vendedor = "Maria Souza", Valor = 2100.40m },
                new Venda { Vendedor = "Maria Souza", Valor = 1350.60m },
                new Venda { Vendedor = "Maria Souza", Valor = 950.20m },
                new Venda { Vendedor = "Maria Souza", Valor = 1600.75m },
                new Venda { Vendedor = "Maria Souza", Valor = 1750.00m },
                new Venda { Vendedor = "Maria Souza", Valor = 1450.90m },
                new Venda { Vendedor = "Maria Souza", Valor = 400.50m },
                new Venda { Vendedor = "Maria Souza", Valor = 180.20m },
                new Venda { Vendedor = "Maria Souza", Valor = 90.75m },

                new Venda { Vendedor = "Carlos Oliveira", Valor = 800.50m },
                new Venda { Vendedor = "Carlos Oliveira", Valor = 1200.00m },
                new Venda { Vendedor = "Carlos Oliveira", Valor = 1950.30m },
                new Venda { Vendedor = "Carlos Oliveira", Valor = 1750.80m },
                new Venda { Vendedor = "Carlos Oliveira", Valor = 1300.60m },
                new Venda { Vendedor = "Carlos Oliveira", Valor = 300.40m },
                new Venda { Vendedor = "Carlos Oliveira", Valor = 500.00m },
                new Venda { Vendedor = "Carlos Oliveira", Valor = 125.75m },

                new Venda { Vendedor = "Ana Lima", Valor = 1000.00m },
                new Venda { Vendedor = "Ana Lima", Valor = 1100.50m },
                new Venda { Vendedor = "Ana Lima", Valor = 1250.75m },
                new Venda { Vendedor = "Ana Lima", Valor = 1400.20m },
                new Venda { Vendedor = "Ana Lima", Valor = 1550.90m },
                new Venda { Vendedor = "Ana Lima", Valor = 1650.00m },
                new Venda { Vendedor = "Ana Lima", Valor = 75.30m },
                new Venda { Vendedor = "Ana Lima", Valor = 420.90m },
                new Venda { Vendedor = "Ana Lima", Valor = 315.40m }
            };

            var resultado = vendas 
                .GroupBy(v => v.Vendedor)
                .Select(g => new
                {
                    Vendedor = g.Key,
                    TotalVendas = g.Sum(v => v.Valor),
                    Comissao = g.Sum(v => CalculadoraComissao.CalcularComissao(v.Valor))
                })
                .OrderByDescending(c => c.Comissao);


            foreach (var item in resultado)
            {
                Console.WriteLine($"Vendedor: {item.Vendedor}");
                Console.WriteLine($"Total de Vendas: R$ {item.TotalVendas:F2}");
                Console.WriteLine($"Comissão: R$ {item.Comissao:F2}");
            }
        }

        static void Desafio2()
        {
            var gerenciador = new GerenciadorEstoque();

            while (true)
            {
                Console.WriteLine("\n===== DESAFIO 2: Controle de Estoque =====");
                Console.WriteLine("Produtos disponíveis:");
                
                foreach (var p in gerenciador.ObterProdutos())
                    Console.WriteLine($"  {p.CodigoProduto} - {p.DescricaoProduto} (Estoque: {p.Estoque})");

                Console.Write("\nDigite o código do produto (ou 0 para sair): ");
                if (!int.TryParse(Console.ReadLine(), out int codigo) || codigo == 0) break;

                Console.Write("Tipo (E = Entrada / S = Saída): ");
                string tipo = Console.ReadLine().ToUpper();
                if (tipo != "E" && tipo != "S")
                {
                    Console.WriteLine("Tipo inválido. Tente novamente.");
                    continue;
                }

                Console.Write("Quantidade: ");
                if (!int.TryParse(Console.ReadLine(), out int qtd) || qtd <= 0)
                {
                    Console.WriteLine("Quantidade inválida. Tente novamente.");
                    continue;
                }

                Console.Write("Descrição da movimentação: ");
                string descricao = Console.ReadLine();
    
                try 
                {
                    var mov = gerenciador.Movimentar(codigo, tipo, qtd, descricao);
                    var produto = gerenciador.BuscarProduto(codigo);

                    Console.WriteLine($"\n✅ Movimentação registrada!");
                    Console.WriteLine($"ID único: {mov.Id}");
                    Console.WriteLine($"Tipo: {mov.Tipo}");
                    Console.WriteLine($"Descrição: {mov.Descricao}");
                    Console.WriteLine($"Quantidade movimentada: {mov.Quantidade}");
                    Console.WriteLine($"Estoque final de '{produto.DescricaoProduto}': {produto.Estoque}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\n❌ Erro ao registrar movimentação: {ex.Message}");
                }
            }
        }

        static void Desafio3()
        {
            Console.WriteLine("\n===== DESAFIO 3: Cálculo de Juros por Atraso =====");

            Console.Write("Digite o valor: R$");
            if (!decimal.TryParse(Console.ReadLine(), out decimal valor) || valor <= 0)
            {
                Console.WriteLine("Valor inválido. Tente novamente.");
                return;
            }
            
            Console.Write("Data de vencimento (dd/MM/yyyy): ");
            if(!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dataVencimento))
            {
                Console.WriteLine("Data de vencimento inválida. Tente novamente.");
                return;
            }

            var (diasAtraso, juros, total) = CalculadoraJuros.CalcularJuros(valor, dataVencimento);

            if (diasAtraso == 0)
            {
                Console.WriteLine("\nTítulo ainda não vencido. Sem juros");
                return;
            }
            
            Console.WriteLine("\n===== RESULTADO =====");
            Console.WriteLine($"Data de vencimento: {dataVencimento:dd/MM/yyyy}");
            Console.WriteLine($"Data de hoje: {DateTime.Today:dd/MM/yyyy}");
            Console.WriteLine($"Dias em atraso: {diasAtraso}");
            Console.WriteLine($"Valor original: R$ {valor:F2}");
            Console.WriteLine($"Juros (2,5% ao dia): R$ {juros:F2}");
            Console.WriteLine($"Valor total com juros: R$ {total:F2}");
        }
    }
}