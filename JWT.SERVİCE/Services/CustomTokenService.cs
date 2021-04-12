using JWT.CORE.Configuration;
using JWT.CORE.Dtos;
using JWT.CORE.Models;
using JWT.CORE.Services;
using JWT.SHARED.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace JWT.SERVİCE.Services
{
    public class CustomTokenService : ITokenServices
    {
        private readonly UserManager<Kullanici> _kullanici;
        private readonly CustomTokenOptions _customTokenOptions;
        public CustomTokenService(UserManager<Kullanici> kullanici,IOptions<CustomTokenOptions> options)
        {
            _kullanici = kullanici;
            _customTokenOptions = options.Value;

        }
        private string CreateRefreshToken()
        {
            var numberByte= new byte[32];
            using var rnd = RandomNumberGenerator.Create();
            rnd.GetBytes(numberByte);
            return Convert.ToBase64String(numberByte);
        }

        private IEnumerable<Claim> GetClaim(Kullanici kullanici,List<String> Audience)
        {
            var userList = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,kullanici.Id),
                new Claim(ClaimTypes.Email,kullanici.Email),
                 new Claim(ClaimTypes.Name,kullanici.UserName),
                  new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())



            };
            userList.AddRange(Audience.Select(x => new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Aud, x)));//JwtRegisteredClaimNames.Aud kendisine istek yapılmasına uygunmudur
            return userList;
        }
        private IEnumerable<Claim> GetClaimsByClient(Client client)
        {
            var claims = new List<Claim>();
            claims.AddRange(client.Audiences.Select(x => new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Aud, x)));//JwtRegisteredClaimNames.Aud kendisine istek
             new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString());
             new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub, client.Id.ToString());

             return claims;
        }


        public TokenDto CreateToken(Kullanici kullanici)
        {
            var AccesTokenOmru = DateTime.Now.AddMinutes(_customTokenOptions.AccesTokenO);
            var RefreshTokenOmru = DateTime.Now.AddMinutes(_customTokenOptions.RefreshTokenO);
            var SecuritKey = SignService.SimetrikAnahtar(_customTokenOptions.SecuritKey);
            SigningCredentials signingCredentials = new SigningCredentials(SecuritKey, SecurityAlgorithms.HmacSha256Signature);
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(

                issuer: _customTokenOptions.Issuer,
                expires: AccesTokenOmru,
                notBefore: DateTime.Now,
                claims: GetClaim(kullanici, _customTokenOptions.Audience),
                signingCredentials: signingCredentials);

            var handler = new JwtSecurityTokenHandler();
            var token= handler.WriteToken(jwtSecurityToken);

            var tokenDto = new TokenDto
            {
                AccessToken = token,
                RefreshToken = CreateRefreshToken(),
                AccessTokenExpiration = AccesTokenOmru,
                RefreshTokenExpiration = RefreshTokenOmru
            };
            return tokenDto;

            
        }

        public ClientTokenDto CreateToken(Client client)
        {
            var AccesTokenOmru = DateTime.Now.AddMinutes(_customTokenOptions.AccesTokenO);
            
            var SecuritKey = SignService.SimetrikAnahtar(_customTokenOptions.SecuritKey);
            SigningCredentials signingCredentials = new SigningCredentials(SecuritKey, SecurityAlgorithms.HmacSha256Signature);
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(

                issuer: _customTokenOptions.Issuer,
                expires: AccesTokenOmru,
                notBefore: DateTime.Now,
                claims: GetClaimsByClient(client),
                signingCredentials: signingCredentials);

            var handler = new JwtSecurityTokenHandler();
            var token = handler.WriteToken(jwtSecurityToken);

            var tokenDto = new ClientTokenDto
            {
                AccessToken = token,
               
                AccessTokenExpiration = AccesTokenOmru,
               
            };
            return tokenDto;
        }
    }
}
