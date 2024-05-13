using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prism.Domain.Abstracts
{
    public interface IUnitOfWork
    {
        IAppUserRepository AppUserRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IProductRepository ProductRepository { get; }

        Task SaveChangeAsync();
    }
}
