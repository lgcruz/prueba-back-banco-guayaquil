using dbTest.DTO;
using Microsoft.EntityFrameworkCore;

namespace dbTest.Services
{
    public class ProductService
    {
        private readonly TestBgContext _context;
        public ProductService(TestBgContext context)
        {
            _context = context;
        }
        public async Task<List<ProductoDTO>> GetProducts()
        {
            var products = await _context.Productos.ToListAsync();
            var productsDTO = new List<ProductoDTO>();
            foreach (var product in products)
            {
                var productDTO = new ProductoDTO();
                productDTO.Id = product.Id;
                productDTO.Nombre = product.Nombre;
                productDTO.Descripcion = product.Descripcion;
                productDTO.ImageSrc = product.ImageSrc;
                productDTO.Precio = product.Precio;

                productsDTO.Add(productDTO);
            }

            return productsDTO;
        }

        public async Task<List<ProductoDTO>> GetLikedProductsByClient(int clientId)
        {
            var favorites = await _context.Favoritos.Include(f => f.Producto).Where(f => f.ClienteId == clientId && f.Estado == 'A'.ToString()).ToListAsync();
            var productsDTO = new List<ProductoDTO>();
            foreach (var favorite in favorites)
            {
                var productDTO = new ProductoDTO();
                var product = favorite.Producto;
                productDTO.Id = product.Id;

                productDTO.Nombre = favorite.Producto.Nombre;
                productDTO.Descripcion = favorite.Producto.Descripcion;
                productDTO.Precio = favorite.Producto.Precio;
                productDTO.ImageSrc = favorite.Producto.ImageSrc;

                productsDTO.Add(productDTO);
            }

            return productsDTO;
        }

        public async Task<ProductoDTO> GetProduct(int id)
        {
            var product = await _context.Productos.FindAsync(id);
            if (product == null) {
                throw new Exception("Product not found");
            }
            var productDTO = new ProductoDTO();

            productDTO.Id = product.Id;
            productDTO.Nombre = product.Nombre;
            productDTO.Descripcion = product.Descripcion;
            productDTO.ImageSrc = product.ImageSrc;
            productDTO.Precio = product.Precio;

            return productDTO;
        }
        public async Task LikeProductRequest(LikeProductRequest likeProductRequest)
        {
            var query = _context.Favoritos.AsQueryable();
            var product = await _context.Productos.FindAsync(likeProductRequest.ProductId);
            if (product == null)
            {
                throw new Exception("Product not found");
            }
            else
            {
                query = query.Where(p => p.ProductoId == product.Id); // Filter by name
            }
            var client = await _context.Clientes.FindAsync(likeProductRequest.ClientId);
            if (client == null)
            {
                throw new Exception("Client not found");
            }
            else
            {
                query = query.Where(p => p.ClienteId == client.Id); // Filter by name
            }

            var favorites = await query.ToListAsync();
            if (favorites.Count > 0)
            {
                favorites[0].Estado = 'A'.ToString();
            } else
            {
                var favorito = new Favorito();

                favorito.Producto = product;
                favorito.Cliente = client;
                favorito.Estado = 'A'.ToString();

                _context.Favoritos.Add(favorito);
            }

            
            await _context.SaveChangesAsync();
        }

        public async Task UnlikeProductRequest(LikeProductRequest likeProductRequest)
        {
            var query = _context.Favoritos.AsQueryable();
            var product = await _context.Productos.FindAsync(likeProductRequest.ProductId);
            if (product == null)
            {
                throw new Exception("Product not found");
            } else
            {
                query = query.Where(p => p.ProductoId == product.Id); // Filter by name
            }
            var client = await _context.Clientes.FindAsync(likeProductRequest.ClientId);
            if (client == null)
            {
                throw new Exception("Client not found");
            } else
            {
                query = query.Where(p => p.ClienteId == client.Id); // Filter by name
            }

            var favorites = await query.FirstOrDefaultAsync();
            favorites.Estado = 'I'.ToString();
            await _context.SaveChangesAsync();
        }
    }
}
