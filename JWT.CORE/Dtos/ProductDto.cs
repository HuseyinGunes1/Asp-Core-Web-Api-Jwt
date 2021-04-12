using System;
using System.Collections.Generic;
using System.Text;

namespace JWT.CORE.Dtos
{
   public class ProductDto
    {
        public int id { get; set; }
        public string Name { get; set; }
    
        public int Stok { get; set; }
        public string UserId { get; set; }

    }
}
