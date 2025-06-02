using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebFerreteria.Models;

public partial class Productos
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal? Precio { get; set; }

    public int? Cantidad { get; set; }

    public virtual ICollection<DetallePedido> DetallePedido { get; set; } = new List<DetallePedido>();
}
