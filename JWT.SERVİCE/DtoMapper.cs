using AutoMapper;
using JWT.CORE.Dtos;
using JWT.CORE.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace JWT.SERVİCE
{
   internal class DtoMapper:Profile //internal tanımlı olduğu assemly de geçerli farklı librry veya projede geçerli olmaz //private sadece kendi clasında geçerli
    {
        public DtoMapper()
        {
            CreateMap<ProductDto, Products>().ReverseMap();
            CreateMap<KullaniciDto, Kullanici>().ReverseMap();
        }
    }
}
