using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProductAddedCrudOperation.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }    
        public string Type { get; set; }    
    }
   
}