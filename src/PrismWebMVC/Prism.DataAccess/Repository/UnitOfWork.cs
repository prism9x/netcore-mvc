using Prism.DataAccess.Data;
using Prism.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prism.DataAccess.Repository
{
    public class UnitOfWork(PrismDbContext db) : IUnitOfWork
    {
        private AppUserRepository? _appUserRepository;
        private CategoryRepository? _categoryRepository;
        private ProductRepository? _productRepository;

        public IAppUserRepository AppUserRepository => _appUserRepository ??= new AppUserRepository(db);

        public ICategoryRepository CategoryRepository => _categoryRepository ??= new CategoryRepository(db);

        public IProductRepository ProductRepository => _productRepository ??= new ProductRepository(db);

        public async Task SaveChangeAsync()
        {
            await db.SaveChangesAsync();
        }
    }
}
