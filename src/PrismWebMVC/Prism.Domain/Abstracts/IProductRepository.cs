using Prism.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prism.Domain.Abstracts
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>?> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
    }
}
