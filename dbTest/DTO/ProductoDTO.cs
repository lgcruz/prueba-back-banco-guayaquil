namespace dbTest.DTO
{
    public class ProductoDTO
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public string Descripcion { get; set; }

        public decimal Precio { get; set; }

        public string ImageSrc { get; set; } = null!;
    }
}
