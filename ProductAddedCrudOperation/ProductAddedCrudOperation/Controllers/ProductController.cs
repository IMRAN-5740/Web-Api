using ProductAddedCrudOperation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Http;

namespace ProductAddedCrudOperation.Controllers
{
    public class ProductController : ApiController
    {

        ApplicationDbContext _context=new ApplicationDbContext();
        /// <summary>
        /// Product Added Operation
        /// </summary>

        [HttpPost]
        public IHttpActionResult AddProduct([FromBody]Product product)
        {
            _context.Products.Add(product);
            int rowCount = _context.SaveChanges();
            if (rowCount > 0)
            {
                return Ok("Product Has Been Saved");
            }
            else
            {
                return BadRequest("Product Save Failed");
            }
        }
        /// <summary>
        /// Get All Product
        /// </summary>

        [HttpGet]
        public IHttpActionResult GetAllProduct()
        {
            var products = _context.Products.ToList();
            if (products.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(products);
            }
        }
       

        [HttpGet]
        /// <summary>
        /// Get By Id Search
        /// </summary>
        public IHttpActionResult GetbyID(int id)
        {
            var product = _context.Products.FirstOrDefault(p=>p.Id==id);
            if(product == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(product);
            }
        }
        /// <summary>
        /// Update Product
        /// </summary>

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
                int rowCount = _context.SaveChanges();  
                if(rowCount>0)
                {
                    return Ok(product);
                }
                else
                {
                    return BadRequest("Update Product Failed");
                }
            }
            
        }
        /// <summary>
        /// Delete Products
        /// </summary>
    
        [HttpDelete]
        public IHttpActionResult DeleteProducts(int id)
        {
            var product=_context.Products.FirstOrDefault(p=>p.Id==id);  
            if(product==null)
            {
                return NotFound();
            }
            else
            {
                _context.Products.Remove(product);  
                int rowCount = _context.SaveChanges();  
                if(rowCount>0)
                {
                    return Ok("Product Has Been Deleted");
                }
                else
                {
                    return BadRequest("Delete Failed");
                }
            }
        }
    }
}
