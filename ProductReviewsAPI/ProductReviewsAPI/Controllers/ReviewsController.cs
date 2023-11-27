using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductReviewsAPI.Data;
using ProductReviewsAPI.Models;

namespace ProductReviewsAPI.Controllers
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
        public IActionResult Get()
        {
            var Review = _context.Reviews.ToList();
            return StatusCode(200, Review);
        }

        // GET: api/Reviews/1
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var Review = _context.Reviews.Find(id);
            if (Review == null)
            {
                return NotFound();
            }
            return StatusCode(200, Review);
        }


        // POST: api/Reviews
        [HttpPost]
        public IActionResult Post([FromBody] Review review)
        {
            _context.Reviews.Add(review);
            _context.SaveChanges();
            return StatusCode(201, review);
        }

        // PUT: api/Products/2
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Review updateReview)
        {
            var existingReview = _context.Reviews.Find(id);
            if (existingReview == null)
            {
                return NotFound();
            }
            existingReview.Text = updateReview.Text;
            existingReview.Rating = updateReview.Rating;
            _context.Reviews.Update(updateReview);
            _context.SaveChanges();
            return StatusCode(200, existingReview);
        }

        // DELETE: api/Products/1
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var removedId = _context.Reviews.Find(id);
            _context.Reviews.Remove(removedId);
            _context.SaveChanges();
            return NoContent();
        }

        // Get: api/Reviews/ProductId
        [HttpGet("search/{keyword}")]
        public IActionResult SearchReviews([FromQuery] int ProductId)
        {
            var productReviews = _context.Reviews.Where(r => r.ProductId == ProductId).ToList();
            return StatusCode(200, productReviews);
        }
    }
}
