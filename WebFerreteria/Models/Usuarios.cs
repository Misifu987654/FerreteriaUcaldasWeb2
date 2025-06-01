using System;
using System.Collections.Generic;

namespace WebFerreteria.Models;

public partial class Usuarios
{
    public int Id { get; set; }

    public string? NombreCompleto { get; set; }

    public string? Correo { get; set; }

    public string? Contrasena { get; set; }

    public string? Rol { get; set; }

    public bool? Estado { get; set; }

    public virtual ICollection<Entregas> Entregas { get; set; } = new List<Entregas>();
}
