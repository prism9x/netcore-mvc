using Prism.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prism.Application.Abstracts
{
    public interface IProductService
    {
        Task<Product> GetByIdAsync(int id);
    }
}
