using SSE.Core.DomainObjects;

namespace SSE.Catalogo.API.Model
{
    public interface IProductReposiroty : IRepository<Product>
    {

        Task<IEnumerable<Product>> GetFindAll();
        Task<Product> GetById(Guid id);

        void Add(Product product);
        void Update(Product product);


    }
}
