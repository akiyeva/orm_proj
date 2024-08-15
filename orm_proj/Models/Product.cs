using orm_proj.Models.Common;

namespace orm_proj.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock {  get; set; }  
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set;}

        public override string ToString()
        {
            return $"{Name} {Price}$, description: {Description}, {Stock} left.";
        }
    }
}
