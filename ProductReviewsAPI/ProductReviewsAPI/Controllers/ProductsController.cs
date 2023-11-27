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
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Reviews/1
        //[HttpGet("{id}", Name = "Get")]
        //public IActionResult Get(int id)
        //{
        //    var Product = _context.Products.Find(id);
        //    if (Product == null)
        //    {
        //        return NotFound();
        //    }
        //    return StatusCode(200, Product);
        //}

        // POST: api/Products
        [HttpPost]
        public IActionResult Post([FromBody] Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return StatusCode(201, product);
        }

        // PUT: api/Products/1
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Product updatedProduct)
        {
            var existingProduct = _context.Products.Find(id);
            if (existingProduct == null)
            {
                return NotFound();
            }
            existingProduct.Name = updatedProduct.Name;
            existingProduct.Price = updatedProduct.Price;
            existingProduct.Reviews = updatedProduct.Reviews;
            _context.Products.Update(updatedProduct);
            _context.SaveChanges();
            return StatusCode(200, existingProduct);
        }

        // DELETE: api/Products/1
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var removedId = _context.Products.Find(id);
            _context.Products.Remove(removedId);
            _context.SaveChanges();
            return NoContent();
        }

        // Get: api/Products
        [HttpGet]
        public IActionResult GetAll([FromQuery] double? maxPrice)
        {
            var Products = _context.Products.ToList();
            if (maxPrice == null)
            {
                Products = Products.Where(p => p.Price == maxPrice).ToList();
            }
            return StatusCode(200, Products);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var Products = _context.Products.Select(p => new ProductDTO
            {
                Name = p.Name,
                Price = p.Price,
                Reviews = p.Reviews.Select(p => new ReviewDTO
                {
                    Text = p.Text,
                    Rating = p.Rating
                }).ToList()
            })
            .ToList();
            return Ok(Products);
        }

    }
}
