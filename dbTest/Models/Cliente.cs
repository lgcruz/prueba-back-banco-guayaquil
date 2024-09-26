using System;
using System.Collections.Generic;

namespace dbTest.Models;

public partial class Cliente
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Telefono { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<Favorito> Favoritos { get; set; } = new List<Favorito>();
}
