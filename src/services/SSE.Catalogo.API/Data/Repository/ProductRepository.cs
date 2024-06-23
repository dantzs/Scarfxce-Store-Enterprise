using Microsoft.EntityFrameworkCore;
using SSE.Catalogo.API.Model;
using SSE.Core.Data;

namespace SSE.Catalogo.API.Data.Repository
{

    public class ProductRepository : IProductReposiroty
    {
        private readonly CatalogoContext _context;

        public ProductRepository (CatalogoContext context)
        {
            _context = context;
        }

        public IUnitOfWorkc UnitOfWork => _context;

        public async Task<Product> GetById(Guid id)
        {
           return await _context.products.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> GetFindAll()
        {
            return await _context.products.AsNoTracking().ToListAsync();
        }


        public void Add(Product product)
        {
            _context.products.Add(product);
        }
        public void Update(Product product)
        {
            _context.Update(product);
        }
        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
