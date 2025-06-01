using System;
using System.Collections.Generic;

namespace WebFerreteria.Models;

public partial class Clientes
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Telefono { get; set; }

    public string? Correo { get; set; }

    public string? Direccion { get; set; }

    public virtual ICollection<Pedidos> Pedidos { get; set; } = new List<Pedidos>();
}
