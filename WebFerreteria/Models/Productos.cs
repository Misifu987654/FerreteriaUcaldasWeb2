using System;
using System.Collections.Generic;

namespace WebFerreteria.Models;

public partial class Productos
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public decimal? Precio { get; set; }

    public int? Cantidad { get; set; }

    public virtual ICollection<DetallePedido> DetallePedido { get; set; } = new List<DetallePedido>();
}
