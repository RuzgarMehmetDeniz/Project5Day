using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project5Day.WebApi.Context;
using Project5Day.WebApi.Dtos;
using Project5Day.WebApi.Entities;

namespace Project5Day.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApiContext _context;

        public ProductController(ApiContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult ProductList()
        {
            var values = _context.Products.Include(x=>x.Category).Select(x => new ResultProductDto
            {
                ProductId = x.ProductId,
                ProductName = x.ProductName,
                Stock = x.Stock,
                Price = x.Price,
                CategoryId = x.CategoryId,
                CategoryName = x.Category.CategoryName
            }).ToList();
            return Ok(values);
        }
        [HttpPost]
        public IActionResult CreateProduct(CreateProdcutDto dto)
        {
            var product = new Product
            {
                ProductName = dto.ProductName,
                Stock = dto.Stock,
                Price = dto.Price,
                CategoryId = dto.CategoryId
            };
            _context.Products.Add(product);
            _context.SaveChanges();
            return Ok("Ekleme Başarılı");
        }

    }
}
