using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using HelloAngularApp.Models;
using WebApplication4.Models;

namespace HelloAngularApp.Controllers
{
    [Route("api/products")]
    public class ProductController : Controller
    {
        ApplicationContext db;
        public ProductController(ApplicationContext context)
        {
            db = context;
            if (!db.Products.Any())
            {
                db.Products.Add(new Product { Item_name = "iPhone X", Item_type = "Apple",  });
                db.Products.Add(new Product { Item_name = "Galaxy S8", Item_type = "Samsung", });
                db.Products.Add(new Product { Item_name = "Pixel 2", Item_type = "Google", });
                db.SaveChanges();
            }
        }
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return db.Products.ToList();
        }

        [HttpGet("{id}")]
        public Product Get(int id)
        {
            Product product = db.Products.FirstOrDefault(x => x.Id == id);
            return product;
        }

        [HttpPost]
        public IActionResult Post([FromBody]Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return Ok(product);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Product product)
        {
            if (ModelState.IsValid)
            {
                db.Update(product);
                db.SaveChanges();
                return Ok(product);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Product product = db.Products.FirstOrDefault(x => x.Id == id);
            if (product != null)
            {
                db.Products.Remove(product);
                db.SaveChanges();
            }
            return Ok(product);
        }
    }
}