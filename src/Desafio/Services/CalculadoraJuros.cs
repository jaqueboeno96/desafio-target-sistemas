using System;

namespace Desafio.Services;

public static class CalculadoraJuros
{
    private const decimal TaxaDiaria =  0.025m;

    public static (int diasAtraso, decimal juros, decimal total) CalcularJuros(decimal valor, DateTime dataVencimento)
    {
        DateTime hoje = DateTime.Today;

        if (hoje <= dataVencimento)

            return (0, 0m, valor);
        int diasAtraso = (hoje - dataVencimento).Days;
        decimal juros = valor * TaxaDiaria * diasAtraso;
        decimal total = valor + juros;

        return (diasAtraso, juros, total);
    }
}
