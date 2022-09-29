using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiDemo2.Models;

namespace WebApiDemo2.Controllers
{
    public class ProductController : ApiController
    {
        ApplicationDbContext _context=new ApplicationDbContext();
        [HttpPost]
        public IHttpActionResult AddProduct([FromBody]Product product)
        {
            _context.Products.Add(product);
            int rowCount = _context.SaveChanges();
            if (rowCount > 0)
            {
                return Ok(product);
            }
            else
            {
                return BadRequest("Product Save Failed");
            }

        }
        [HttpGet]
        public IHttpActionResult GetAllProducts()
        {
            var products=_context.Products.ToList();
            if(products.Count ==0)
            {
                return NotFound();
            }
            else
            {
                return Ok(products);
            }
        }
        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            var product=_context.Products.FirstOrDefault(p => p.Id == id);
            if(product==null)
            {
                return NotFound();
            }
            else
            {
                return Ok(product);
            }
        }
        [HttpPut]
        public IHttpActionResult UpdateProduct([FromBody]Product product)
        {
            if(product.Id<=0)
            {
                return NotFound();
            }
            else
            {
                _context.Entry(product).State = System.Data.Entity.EntityState.Modified;
                int rowCount=_context.SaveChanges();
                if(rowCount > 0)
                {
                    return Ok(product);
                }
                else
                {
                    return BadRequest("Update Failed");
                }
            }
        }
        [HttpDelete]
        public IHttpActionResult DeleteProduct(int id)
        {
            var product= _context.Products.FirstOrDefault(p => p.Id == id);
          
            if(product==null)
            {
                return NotFound();
            
            }
            else
            {
                _context.Products.Remove(product);
                int rowCount=_context.SaveChanges();
                if(rowCount>0)
                {
                    return Ok("Product Has been Deleted");
                }
                else
                {
                    return BadRequest("Delete Failed");
                }
            }
        }
    }
}
