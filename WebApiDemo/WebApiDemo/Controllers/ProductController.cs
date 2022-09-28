using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApiDemo.Controllers
{
    public class ProductController : ApiController
    { static List<string> products = new List<string>()
    {
        "Laptop","DeskTop","Remote"
    };
        [HttpGet]
        public List<string>GetAll()
        {
            return products;
        }
        [HttpGet]
        public string GetProductNamebyID(int id)
        {
            return products[id];
        }
        [HttpGet]
        public void Submit(string ProductName) 
        {
            products.Add(ProductName);
        }
        [HttpGet]
        public void Update(int id,string ProductName)
        {
            products[id] = ProductName;
        }
        [HttpGet]
        public void Delete(int id)
        {
            products.RemoveAt(id);
        }
    }
}
