using ProductAddedCrudOperation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace ProductAddedCrudOperation.Areas.Admin
{
    public class SetupController : Controller
    {
        // GET: Admin/Setup
        string baseUrl = "https://localhost:44321/";
        public async Task<ActionResult> Index()
        {
            List<Product> products = new List<Product>();
            using (var client = new HttpClient())
            {
                client.BaseAddress=new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage message = await client.GetAsync("api/Product/GetAllProduct");
                if(message.IsSuccessStatusCode)
                {
                    var productlist=message.Content.ReadAsStringAsync().Result;
                    products=JsonConvert.DeserializeObject<List<Product>>(productlist);
                }
            }
            return View(products); 
        }
    }
}