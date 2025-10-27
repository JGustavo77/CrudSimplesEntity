namespace FrutasDoSeuZe.Models;

public class Pedido
{
    public int Id { get; set; }
    public DateTime Data { get; set; }
    public required string Tipo { get; set; }
    public string? Descricao { get; set; }
    public decimal ValorTotal { get; set; } = 0;
    public List<ItemPedido> Itens { get; set; } = new();
}
