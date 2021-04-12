using JWT.CORE.Configuration;
using JWT.CORE.Dtos;
using JWT.CORE.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace JWT.CORE.Services
{
    public interface ITokenServices
    {
        TokenDto CreateToken(Kullanici kullanici);
        ClientTokenDto CreateToken(Client client);


    }
}
