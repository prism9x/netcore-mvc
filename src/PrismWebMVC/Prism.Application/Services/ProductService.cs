using Prism.Application.Abstracts;
using Prism.Domain.Abstracts;
using Prism.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prism.Application.Services
{
    public class ProductService(IUnitOfWork unitOfWork) : IProductService
    {
        public async Task<Product> GetByIdAsync(int id)
        {
            return await unitOfWork.ProductRepository.GetByIdAsync(id);
        }
    }
}
