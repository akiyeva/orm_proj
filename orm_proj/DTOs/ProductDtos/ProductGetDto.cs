namespace orm_proj.DTOs
{
    public class ProductGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public override string ToString()
        {
            return $"{Id}. {Name} {Price}$, description: {Description}, {Stock} left.";
        }
    }
}
