using System;

namespace Desafio.Models;

public class Movimentacao
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Descricao { get; set; }
    public string Tipo { get; set; }
    public int Quantidade { get; set; }
}
