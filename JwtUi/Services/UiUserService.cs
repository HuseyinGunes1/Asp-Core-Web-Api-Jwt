using JWT.CORE.Dtos;
using JwtUi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JwtUi.Services
{
    public class UiUserService
    {
        private readonly HttpClient _httpClient;
        public UiUserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<UiUserDto>  KullaniciEkle(UiUserDto uiUserDto)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(uiUserDto),Encoding.UTF8,"application/json");
            var response = await _httpClient.PostAsync("User/", stringContent);
            if (response.IsSuccessStatusCode)
            {
                uiUserDto = JsonConvert.DeserializeObject<UiUserDto>(await response.Content.ReadAsStringAsync());
                return uiUserDto;
            }
            else
            {
                return null;
            }
        }


        public async Task<UiUserDto> Listele()
        {
          
            var response = await _httpClient.GetAsync("User/");
            if (response.IsSuccessStatusCode)
            {

                return null;
            }
            else
            {
                return null;
            }
        }
    }
}
