using System;
using System.Collections.Generic;
using System.Text;

namespace JWT.SHARED.Configurations
{
    public class CustomTokenOptions
    {
        public List<string> Audience { get; set; }
        public string Issuer { get; set; }
        public double AccesTokenO { get; set; }
        public double RefreshTokenO { get; set; }
        public string SecuritKey { get; set; }


    }
}
