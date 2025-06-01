using System;
using System.Collections.Generic;

namespace WebFerreteria.Models;

public partial class Pedidos
{
    public int Id { get; set; }

    public int? ClienteId { get; set; }

    public DateTime? Fecha { get; set; }

    public string? Estado { get; set; }

    public virtual Clientes? Cliente { get; set; }

    public virtual ICollection<DetallePedido> DetallePedido { get; set; } = new List<DetallePedido>();

    public virtual ICollection<Entregas> Entregas { get; set; } = new List<Entregas>();

    public virtual ICollection<Facturas> Facturas { get; set; } = new List<Facturas>();
}
