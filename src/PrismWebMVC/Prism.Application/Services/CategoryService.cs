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
    public class CategoryService(IUnitOfWork unitOfWork) : ICategoryService
    {
        public async Task<IEnumerable<Category>?> GetAll()
        {
            return await unitOfWork.CategoryRepository.GetAllAsync();
        }

        public async Task<Category?> GetById(int id)
        {
            return await unitOfWork.CategoryRepository.GetByIdAsync(id);
        }
    }
}
