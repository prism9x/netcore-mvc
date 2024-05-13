using Prism.DataAccess.Data;
using Prism.Domain.Abstracts;
using Prism.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prism.DataAccess.Repository
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(PrismDbContext db) : base(db)
        {
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await GetAllAsync(x => x.IsActive);
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await GetSingleAsync(x => x.Id == id);
        }
    }
}
