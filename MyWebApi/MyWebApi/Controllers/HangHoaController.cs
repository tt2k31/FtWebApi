using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebApi.Models;

namespace MyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HangHoaController : ControllerBase
    {
        public static List<HangHoa> hangHoas = new List<HangHoa>();

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(hangHoas);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            try
            {
                var hh = hangHoas.SingleOrDefault(h => h.MaHangHoa == Guid.Parse(id));
                if (hh == null)
                {
                    return NotFound();
                }
                return Ok(hh);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult Create(HangHoaVM hangHoaVM)
        {
            var hh = new HangHoa
            {
                MaHangHoa = Guid.NewGuid(),
                TenHangHoa = hangHoaVM.TenHangHoa,
                DonGia = hangHoaVM.DonGia
            };
            hangHoas.Add(hh);
            return Ok(new
            {
                Success = true,
                data = hh
            });
        }
        [HttpPut("id")]
        public IActionResult Edit(string id, HangHoa hhEdit)
        {
            try
            {
                var hh = hangHoas.SingleOrDefault(h => h.MaHangHoa == Guid.Parse(id));
                if (hh == null)
                {
                    return NotFound();
                }
                if (id != hh.MaHangHoa.ToString())
                {
                    return BadRequest();
                }
                hh.TenHangHoa = hhEdit.TenHangHoa;
                hh.DonGia = hhEdit.DonGia;

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
