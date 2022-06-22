using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebApi.Data;
using MyWebApi.Services;

namespace MyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoaisController : ControllerBase
    {
        private readonly ILoaiRepository _repository;

        public LoaisController(ILoaiRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_repository.GetAll());   
            }
            catch
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var result = _repository.GetById(id);
                if (result == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }
                return Ok(result);
                
            }
            catch
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, LoaiVM loaiVM)
        {
            if(id != loaiVM.MaLoai)
            {
                return BadRequest();
            }
            try
            {
                _repository.UpdateById(loaiVM);
                return StatusCode(StatusCodes.Status200OK);
            } 
            catch
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }
        [HttpDelete("{id}")]
        public  IActionResult Delete(int id)
        {
            try
            {
                _repository.DeleteById(id);
                return StatusCode(StatusCodes.Status200OK);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost]
        public IActionResult Create(LoaiModel model)
        {
            try
            {
                return Ok(_repository.Add(model));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
