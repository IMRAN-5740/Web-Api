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
        /// <summary>
        /// Add Product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Get All Products
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Get By Id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Update Product 
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Delete Product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
