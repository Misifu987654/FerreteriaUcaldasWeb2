using System;
using System.Collections.Generic;

namespace WebFerreteria.Models;

public partial class Facturas
{
    public int Id { get; set; }

    public int? PedidoId { get; set; }

    public DateTime? Fecha { get; set; }

    public decimal? Total { get; set; }

    public virtual Pedidos? Pedido { get; set; }
}
