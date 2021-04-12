using JWT.CORE.Dtos;
using JWT.CORE.Models;
using JWT.CORE.Services;
using JWT.SERVİCE.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController :CustomBaseController
    {
        private readonly IServiceGeneric<Products,ProductDto> _serviceGeneric;

        public ProductController(IServiceGeneric<Products, ProductDto> serviceGeneric)
        {
            _serviceGeneric = serviceGeneric;
        }

        [HttpGet]
        public async Task<IActionResult> GetProduct()
        {
            return ActionInstance(await _serviceGeneric.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> SaveProduct(ProductDto productDto)
        {
            return ActionInstance(await _serviceGeneric.AddAsync(productDto));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(ProductDto productDto)
        {
            return ActionInstance(await _serviceGeneric.Update(productDto,productDto.id));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            return ActionInstance(await _serviceGeneric.Remove(id));
        }


    }
}
