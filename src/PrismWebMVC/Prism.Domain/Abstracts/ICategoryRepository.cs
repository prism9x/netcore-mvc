using Prism.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prism.Domain.Abstracts
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>?> GetAllAsync();
        Task<Category?> GetByIdAsync(int id);
    }
}
