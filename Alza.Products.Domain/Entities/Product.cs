using System;
using System.Collections.Generic;
using System.Text;

namespace Alza.Products.Domain.Entities
{
    public class Product : EntityBase
    {
        public string Name { get; set; }
        public string ImgUri { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
    }
}
