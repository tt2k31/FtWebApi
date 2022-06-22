using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebApi.Data;

namespace MyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoaiController : ControllerBase
    {
        private readonly MyDbContext _myDbContext;

        public LoaiController(MyDbContext myDbContext)
        {
            _myDbContext = myDbContext;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var dsLoai = _myDbContext.Loais.ToList();
            return Ok(dsLoai);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var Loai = _myDbContext.Loais.SingleOrDefault(l => l.MaLoai == id);
            if (Loai == null) { return NotFound(); }
            return Ok(Loai);
        }
        [HttpPost]
        [Authorize]
        public IActionResult Create(LoaiModel model)
        {
            try 
            {
                var loai = new Loai
                {
                    TenName = model.TenName
                };
                _myDbContext.Add(loai);
                _myDbContext.SaveChanges();
                return StatusCode(StatusCodes.Status201Created, loai);
            } 
            catch
            {
                return BadRequest();
            }

        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, LoaiModel model)
        {
            var Loai = _myDbContext.Loais.SingleOrDefault(l => l.MaLoai == id);
            if (Loai == null)
            { return NotFound(); }

            Loai.TenName = model.TenName;
            _myDbContext.SaveChanges();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id)
        {
            var Loai = _myDbContext.Loais.SingleOrDefault(l => l.MaLoai == id);
            if (Loai == null) { return NotFound(); }
            _myDbContext.Remove(Loai);
            _myDbContext.SaveChanges();
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
