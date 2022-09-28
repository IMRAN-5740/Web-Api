using Microsoft.Ajax.Utilities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Web.Http;
using System.Web.Http.Cors;

using WebApiDemo.Models;
using EnableCorsAttribute = System.Web.Http.Cors.EnableCorsAttribute;

namespace WebApiDemo.Controllers
{

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ProductController : ApiController
    { 
        //static List<string> products = new List<string>()
        //{
        //    "Laptop","DeskTop","Remote"
        //};
        //[HttpGet]
        //public List<string>GetAll()
        //{
        //    return products;
        //}
        //[HttpGet]
        //public string GetProductNamebyID(int id)
        //{
        //    return products[id];
        //}
        //[HttpGet]
        //public void Submit(string ProductName) 
        //{
        //    products.Add(ProductName);
        //}
        //[HttpGet]
        //public void Update(int id,string ProductName)
        //{
        //    products[id] = ProductName;
        //}
        //[HttpGet]
        //public void Delete(int id)
        //{
        //    products.RemoveAt(id);
        //}
        ApplicationDbContext _context=new ApplicationDbContext();
        /// <summary>
        /// New Product Added
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Add([FromBody]Product product)
        {
            _context.Products.Add(product);
            int rowCount=_context.SaveChanges();
            if(rowCount>0)
            {
                //return Ok("Product Has Been Saved.");
                return Ok(product);
            }
            else
            {
                return BadRequest("Save Failed.");
            }
        }
        /// <summary>
        /// Get All Product
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var products = _context.Products.ToList();
            if(products.Count==0)
            { 
                return NotFound(); 
            }
            else
            {
                return Ok(products);
            }
        }
        /// <summary>
        /// Search Product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetByID(int id)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == id);//lamda expression
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
        public IHttpActionResult Update([FromBody]Product product)
        {
            if(product.Id<=0)
            {
                return NotFound();
            }
            else
            {
                _context.Entry(product).State = System.Data.Entity.EntityState.Modified;
                int rowCount=_context.SaveChanges();
                if(rowCount>0)
                {
                    return Ok(product);
                }
                else
                {
                    return BadRequest("Modified Failed");
                }
            }

        }
        /// <summary>
        /// Deleted Product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var product= _context.Products.FirstOrDefault(x => x.Id == id);
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
