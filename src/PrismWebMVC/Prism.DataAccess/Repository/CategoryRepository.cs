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
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(PrismDbContext db) : base(db)
        {
        }

        public async Task<IEnumerable<Category>?> GetAllAsync()
        {
            return await GetAllAsync(x => x.IsActive);
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await GetSingleAsync(x => x.Id == id);
        }
    }
}
