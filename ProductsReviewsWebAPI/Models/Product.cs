namespace ProductsReviewsWebAPI.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public double Price { get; set; }
        public ICollection<Review>? review { get; set; }
    }
}
