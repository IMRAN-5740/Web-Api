using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApiDemo.Models;

namespace WebApiDemo.Areas.Admin.Controllers
{
    public class SetupController : Controller
    {
        // GET: Admin/Setup
        private string baseUrl = "https://localhost:44396/";
        public async Task<ActionResult> Index()
        {
            List<Product> products = new List<Product>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage message = await client.GetAsync("api/Product/GetAll");
                if(message.IsSuccessStatusCode)
                {
                    var productList=message.Content.ReadAsStringAsync().Result;
                    products=JsonConvert.DeserializeObject<List<Product>>(productList);

                }
            }

            return View(products);
        }
    }
}