namespace Desafio.Services;


public static class CalculadoraComissao
{
    public static decimal CalcularComissao(decimal valor)
    {
        if (valor < 100.00m) return 0m;
        if (valor < 500.00m) return valor * 0.01m;
        return valor * 0.05m;
    }
}
