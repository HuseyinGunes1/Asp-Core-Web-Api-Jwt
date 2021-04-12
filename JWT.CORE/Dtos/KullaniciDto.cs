using System;
using System.Collections.Generic;
using System.Text;

namespace JWT.CORE.Dtos
{
   public class KullaniciDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string EMail { get; set; }
        public string City { get; set; }
    }
}
