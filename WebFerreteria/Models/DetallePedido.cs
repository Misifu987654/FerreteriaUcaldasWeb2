using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebFerreteria.Models;

public partial class DetallePedido
{
    public int Id { get; set; }

    public int? PedidoId { get; set; }

    public int? ProductoId { get; set; }

    [Range(1, int.MaxValue)]
    public int? Cantidad { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal? PrecioUnitario { get; set; }

    public virtual Pedidos? Pedido { get; set; }

    public virtual Productos? Producto { get; set; }
}
