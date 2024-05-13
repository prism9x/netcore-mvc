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
    public class AppUserRepository : GenericRepository<AppUser>, IAppUserRepository
    {
        public AppUserRepository(PrismDbContext db) : base(db)
        {
        }
    }
}
