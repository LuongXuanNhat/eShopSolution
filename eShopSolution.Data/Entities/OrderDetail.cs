namespace eShopSolution.Data.Entities
{
    public class OrderDetail
    {
        public int OrderId { set; get; }
        public int ProductId { set; get; }
        public int Quantity { set; get; }
        public decimal Price { set; get; }

        // Quan he 1-n (Order la so it)
        public Order Order { set; get; }
        public Product Product { set; get; }

    }
}