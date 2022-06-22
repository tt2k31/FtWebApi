using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebApi.Services;

namespace MyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IHangHoaRepository _hangHoaRepository;

        public ProductController(IHangHoaRepository hangHoaRepository)
        {
            _hangHoaRepository = hangHoaRepository;
        }
        [HttpGet]
        public IActionResult getallp(string? search, double? from, double? to, string? sortBy, int page = 1)
        {
            try
            {
                var rs = _hangHoaRepository.GetAll(search, from, to, sortBy);
                return Ok(rs);
            }
            catch
            {
                return BadRequest();
            }
        }

    }
}
