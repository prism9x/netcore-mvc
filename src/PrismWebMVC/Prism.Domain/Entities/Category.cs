using Prism.Domain.Entities.Common;

namespace Prism.Domain.Entities
{
    public class Category : BaseEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public ICollection<Product>? Products { get; set; }
    }
}
