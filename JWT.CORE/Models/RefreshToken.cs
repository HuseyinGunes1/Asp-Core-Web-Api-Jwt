using System;
using System.Collections.Generic;
using System.Text;

namespace JWT.CORE.Models
{
   public class RefreshToken
    {
        public string UserId { get; set; }
        public string Code { get; set; }
        public DateTime OTraih { get; set; }
    }
}
