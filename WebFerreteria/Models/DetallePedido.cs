using System;
using System.Collections.Generic;

namespace WebFerreteria.Models;

public partial class DetallePedido
{
    public int Id { get; set; }

    public int? PedidoId { get; set; }

    public int? ProductoId { get; set; }

    public int? Cantidad { get; set; }

    public decimal? PrecioUnitario { get; set; }

    public virtual Pedidos? Pedido { get; set; }

    public virtual Productos? Producto { get; set; }
}
