namespace Revision.LINQ.Models
{
    /// <summary>
    /// Model Sản phẩm sử dụng trong demo LINQ
    /// </summary>
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }

        // Constructor không tham số (cho Entity Framework)
        public Product()
        {
        }

        public Product(int id, string name, string category, decimal price, int stock)
        {
            Id = id;
            Name = name;
            Category = category;
            Price = price;
            Stock = stock;
        }

        public override string ToString()
        {
            return $"[{Id}] {Name} - {Category} - {Price:C} - Tồn kho: {Stock}";
        }
    }
}
