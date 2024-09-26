using System;
using System.Collections.Generic;

namespace dbTest.Models;

public partial class Favorito
{
    public int ClienteId { get; set; }

    public int ProductoId { get; set; }

    public string? Estado { get; set; }

    public virtual Cliente Cliente { get; set; } = null!;

    public virtual Producto Producto { get; set; } = null!;
}
