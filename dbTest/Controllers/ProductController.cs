using dbTest.DTO;
using dbTest.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dbTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService productService;
        public ProductController(ProductService productService) {
            this.productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProducts()
        {
            return Ok(await productService.GetProducts());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            try
            {
                return Ok(await productService.GetProduct(id));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> LikeProduct(LikeProductRequest likeProductRequest)
        {
            try
            {
                await productService.LikeProductRequest(likeProductRequest);
                return Ok("The product was marked as desired");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete]
        public async Task<IActionResult> UnlikeProduct(LikeProductRequest likeProductRequest)
        {
            try
            {
                await productService.UnlikeProductRequest(likeProductRequest);
                return Ok("The product was unmarked as desired");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        //[HttpGet]
        //public async Task<IActionResult> GetLikedProductsByClient(int clientId)
        //{
        //    return Ok(await productService.GetLikedProductsByClient(clientId));
        //}
    }

    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly ProductService productService;
        public ClientController(ProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Producto>>> GetLikedProductsByClient(int id)
        {
            return Ok(await productService.GetLikedProductsByClient(id));
        }

    }
}
