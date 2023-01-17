namespace eShopSolution.Data.Entities
{
    // Quan he nhieu - nhieu
    public class ProductInCategory
    {
        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}