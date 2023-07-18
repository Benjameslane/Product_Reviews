using Microsoft.AspNetCore.Mvc;
using ProductsReviewsWebAPI.Data;
using ProductsReviewsWebAPI.DTOs;
using ProductsReviewsWebAPI.Models;
using System.Linq;

namespace ProductsReviewsWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReviewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Reviews
        [HttpGet]
        public IActionResult GetAll()
        {
            var reviews = _context.Reviews.ToList();
            return Ok(reviews);
        }

        // GET: api/Reviews/5
        [HttpGet("{id}")]
        public IActionResult GetReview(int id)
        {
            var review = _context.Reviews.FirstOrDefault(r => r.Id == id);
            if (review == null)
            {
                return NotFound();
            }

            return Ok(review);
        }

        // POST: api/Reviews
        [HttpPost]
        public IActionResult Post([FromBody] Review review)
        {
            if (ModelState.IsValid)
            {
                _context.Reviews.Add(review);
                _context.SaveChanges();


                return StatusCode(201, review);
            }
            return BadRequest(review);
        }

        // PUT: api/Reviews/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ReviewDTO reviewDto)
        {
            var existingReview = _context.Reviews.FirstOrDefault(r => r.Id == id);
            if (existingReview == null)
            {
                return NotFound();
            }

            existingReview.Rating = reviewDto.Rating;
            existingReview.Text = reviewDto.Text;
           

            _context.SaveChanges();

            return Ok(existingReview);
        }

        // DELETE: api/Reviews/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var review = _context.Reviews.FirstOrDefault(r => r.Id == id);
            if (review == null)
            {
                return NotFound();
            }

            _context.Reviews.Remove(review);
            _context.SaveChanges();

            return NoContent();
        }
    }
}