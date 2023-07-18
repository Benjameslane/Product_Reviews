namespace ProductsReviewsWebAPI.DTOs
{
    public class ReviewDTO
    {
        public int Rating { get; set; }
        public string Text { get; set; }
        public int ProductId { get; set; }
        
    }
}