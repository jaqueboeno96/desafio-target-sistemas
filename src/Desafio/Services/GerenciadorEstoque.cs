using System;
using System.Collections.Generic;
using System.Linq;
using Desafio.Models;

namespace Desafio.Services;


public class GerenciadorEstoque
{
    private readonly List<Produto> _estoque;
    private readonly List<Movimentacao> _movimentacoes = new List<Movimentacao>();

    public GerenciadorEstoque()
    {
         _estoque = new List<Produto>
         {
                new Produto { CodigoProduto = 101, DescricaoProduto = "Caneta Azul", Estoque = 150 },
                new Produto { CodigoProduto = 102, DescricaoProduto = "Caderno Universitário", Estoque = 75 },
                new Produto { CodigoProduto = 103, DescricaoProduto = "Borracha Branca", Estoque = 200 },
                new Produto { CodigoProduto = 104, DescricaoProduto = "Lápis Preto HB", Estoque = 320 },
                new Produto { CodigoProduto = 105, DescricaoProduto = "Marcador de Texto Amarelo", Estoque = 90 },
        };
    }

    public IEnumerable<Produto> ObterProdutos() => _estoque;
    
    public Produto? BuscarProduto(int codigoProduto) => _estoque.FirstOrDefault(p => p.CodigoProduto == codigoProduto);

    public Movimentacao Movimentar(int codigoProduto, string tipo, int quantidade, string descricao)
    {
        var produto = BuscarProduto(codigoProduto);
        if (produto == null) throw new Exception("Produto não encontrado");

        bool isEntrada = tipo is "E" or "Entrada";
        bool isSaida = tipo is "S" or "Saída";

        if (!isEntrada && !isSaida)
            throw new Exception("Tipo inválido. Use E (Entrada) ou S (Saída).");

        if (isSaida && quantidade > produto.Estoque)
            throw new Exception("Estoque insuficiente");

        if (isEntrada) produto.Estoque += quantidade;
        else produto.Estoque -= quantidade;

        var movimentacao = new Movimentacao
        {
            Descricao = descricao,
            Tipo = isEntrada ? "Entrada" : "Saída",
            Quantidade = quantidade,
        };
        _movimentacoes.Add(movimentacao);
        return movimentacao;
    }
}
