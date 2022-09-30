using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApiDemo2.Models;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace WebApiDemo2.Areas.Admin
{
    public class SetupController : Controller
    {
        // GET: Admin/Setup
        string baseUrl = "https://localhost:44333/";
        public async Task<ActionResult> Index()
        {
            List<Product> products = new List<Product>();
            using (var clients = new HttpClient())
            {
                clients.BaseAddress = new Uri(baseUrl);
                clients.DefaultRequestHeaders.Clear();
                clients.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage responce = await clients.GetAsync("api/Product/GetAllProducts");
                if(responce.IsSuccessStatusCode)
                {
                    var productList = responce.Content.ReadAsStringAsync().Result;
                    products=JsonConvert.DeserializeObject<List<Product>>(productList);
                }
            }
            
                return View(products);
        }
        public ActionResult ProductAdd()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> ProductAdd(Product product)
        {
            string message = "";
            using (var clients = new HttpClient())
            {
                clients.BaseAddress = new Uri(baseUrl);
                clients.DefaultRequestHeaders.Clear();
                clients.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage responce = await clients.PostAsJsonAsync<Product>("api/Product/AddProduct",product);
                if (responce.IsSuccessStatusCode)
                {
                    var productList = responce.Content.ReadAsStringAsync().Result;
                    message = "Save Data Successfully";
                }
                else

                {
                    message = "Save Failed";
                }
                ViewBag.message = message;
            }
            return View();
        }
        public async Task<ActionResult> Edit(int id)
        {
            if(id == 0 || id<0)
                {
                return RedirectToAction("Index");
                }
            Product product = new Product();
            using (var clients = new HttpClient())
            {
                clients.BaseAddress = new Uri(baseUrl);
                clients.DefaultRequestHeaders.Clear();
                clients.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage responce = await clients.GetAsync("api/Product/GetById?id="+id);
                if (responce.IsSuccessStatusCode)
                {
                    var productData = responce.Content.ReadAsStringAsync().Result;
                    product=JsonConvert.DeserializeObject<Product>(productData);
                    
                }
            }
            return View(product);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Product product)
        {
            string message = "";
            using (var clients = new HttpClient())
            {
                clients.BaseAddress = new Uri(baseUrl);
                clients.DefaultRequestHeaders.Clear();
                clients.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage responce = await clients.PutAsJsonAsync<Product>("api/Product/UpdateProduct", product);
                if (responce.IsSuccessStatusCode)
                {   
                    message = "Update Data Successfully";
                    return RedirectToAction("Index");
                    
                   
                }
                else

                {
                    message = "Update Failed";
                }
                ViewBag.message = message;
            }
            return View(product);
        }
        [HttpPost]
        public async Task<ActionResult> Delete(int productID)
        {
            if (productID == 0 || productID < 0)
            {
                return RedirectToAction("Index");
            }
          
            using (var clients = new HttpClient())
            {
                clients.BaseAddress = new Uri(baseUrl);
                clients.DefaultRequestHeaders.Clear();
                clients.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage responce = await clients.DeleteAsync("api/Product/DeleteProduct?id=" + productID);
                if (responce.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");

                }
            }
            return RedirectToAction("Index");
        }
    }
}