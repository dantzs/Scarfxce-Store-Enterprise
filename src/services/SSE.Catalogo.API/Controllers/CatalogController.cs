using Microsoft.AspNetCore.Mvc;
using SSE.Catalogo.API.Model;


namespace SSE.Catalogo.API.Controllers
{
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepository _productRepositoty;

        public CatalogController(IProductRepository productRepositoty)
        {
            _productRepositoty = productRepositoty;
        }

        [HttpGet("catalogo/produtos")]
        public async Task<IEnumerable<Product>> Index()
        {
            return await _productRepositoty.GetFindAll();
        }

        [HttpGet("catologo/produto/{id}")]
        public async Task<Product> ProductDetail(Guid id)
        {
            return await _productRepositoty.GetById(id);
        }
    } 
}
