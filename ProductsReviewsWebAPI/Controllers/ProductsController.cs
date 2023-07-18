using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsReviewsWebAPI.Data;
using ProductsReviewsWebAPI.DTOs;
using ProductsReviewsWebAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductsReviewsWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: api/<ProductsController>
        [HttpGet]
        public IActionResult GetAll([FromQuery] double? maxPrice)
        {
            // check if maxPrice has value, which means it is supplied in the query parameters
            if (maxPrice.HasValue)
            {
                // if maxPrice is supplied, filter products by price, and convert to list
                var products = _context.Products.Where(p => p.Price <= maxPrice.Value).ToList();
                return Ok(products);
            }
            else
            {
                // if maxPrice is not supplied, return all products
                var products = _context.Products.ToList();
                return Ok(products);
            }
        }


        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            var product = _context.Products.Include(p => p.Reviews).Select(p => new ProductDTO
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Reviews = p.Reviews.Select(r => new ReviewDTO
                {
                    Text = r.Text,
                    Rating = r.Rating,
                }).ToList()

            }).FirstOrDefault(r => r.Id == id);

            if (product == null)
            {
                return NotFound(); // Returns NotFound when there is no products with such id
            }
            return Ok(product); // Returns 200 status code
        }

        // POST api/<ProductsController>
        [HttpPost]
        public IActionResult Post([FromBody]Product product)
        {
             _context.Products.Include(p => p.Reviews);
            _context.Products.Add((Product)product);
            _context.Products.Include(p => p.Reviews);
            _context.SaveChanges();
            return StatusCode(201, product);
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ProductDTO productDto)
        {
            var existingProduct = _context.Products.FirstOrDefault(p => p.Id == id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            existingProduct.Name = productDto.Name;
            existingProduct.Price = productDto.Price;

            _context.SaveChanges();

            return Ok(existingProduct);
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var music = _context.Products.FirstOrDefault(m => m.Id == id);
            if (music == null)
            {
                return NotFound();
            }
            _context.Products.Remove(music);
            _context.SaveChanges();
            return NoContent(); // Returns NoContent status code


        }
        [HttpGet("search/{price}")]
        public IActionResult Search(double price)
        {
            var products = _context.Products.Where(p => p.Price == price).ToList();

            if (products == null || products.Count == 0)
            {
                return NotFound();
            }

            return Ok(products);
        }
    }
    //var products = _context.products.FirstOrDefault(p => .price == price);
}
