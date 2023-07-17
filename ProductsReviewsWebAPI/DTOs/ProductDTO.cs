using ProductsReviewsWebAPI.Models;

namespace ProductsReviewsWebAPI.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public double Price { get; set; }
        public ICollection<Review>? review { get; set; }

        public double AverageRating { get; set; }
    }
}
