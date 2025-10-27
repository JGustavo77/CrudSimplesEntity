namespace FrutasDoSeuZe.Models;


public class ItemPedido
{
    public int Id { get; set; }
    public int FrutaId { get; set; }
    public int PedidoId { get; set; }
    public int Quantidade { get; set; } = 0;
    public Fruta Fruta { get; set; }
    public Pedido Pedido { get; set; }
}
