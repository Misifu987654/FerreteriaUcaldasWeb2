using System;
using System.Collections.Generic;

namespace WebFerreteria.Models;

public partial class Entregas
{
    public int Id { get; set; }

    public int? PedidoId { get; set; }

    public int? EntregadoPor { get; set; }

    public string? DireccionEntrega { get; set; }

    public DateTime? FechaEntrega { get; set; }

    public virtual Usuarios? EntregadoPorNavigation { get; set; }

    public virtual Pedidos? Pedido { get; set; }


}

