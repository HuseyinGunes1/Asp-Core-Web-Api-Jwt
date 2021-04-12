using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace JWT.SERVİCE
{
   public static class ObjectMapper
    {
        private static readonly Lazy<IMapper> lazy = new Lazy<IMapper>(() =>
          {
              var config = new MapperConfiguration(x => x.AddProfile<DtoMapper>());
              return config.CreateMapper();
          });
        public static IMapper MapperDeneme => lazy.Value;
    }
}
