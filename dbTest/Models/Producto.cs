using System;
using System.Collections.Generic;

namespace dbTest.Models;

public partial class Producto
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public decimal Precio { get; set; }

    public string ImageSrc { get; set; } = null!;

    public virtual ICollection<Favorito> Favoritos { get; set; } = new List<Favorito>();
}
