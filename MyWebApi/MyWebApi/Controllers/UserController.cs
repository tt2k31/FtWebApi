using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyWebApi.Data;
using MyWebApi.Models;
using MyWebApi.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly AppSettings _appsetting;

        public UserController(MyDbContext context, IOptions<AppSettings> optionsMonitor)
        {
            _context = context;
            _appsetting = optionsMonitor.Value;
        }

        [HttpPost("Login")]
        public IActionResult Validate(LoginModel model)
        {
            var user = _context.NguoiDungs.
                SingleOrDefault(nd => nd.UserName == model.UserName
                                    && nd.Password == model.Password);
            if(user == null) //sai
            {
                return Ok(new ApiRespone
                {
                    Success = false,
                    Message = "ko coas dau"
                });
            }
            //cap token
            return Ok(new ApiRespone
            {
                Success = true,
                Message = "TC",
                Data = GenToken(user)
            });
        }
        private string GenToken(NguoiDung nguoi)
        {
            var JWtTokenHander = new JwtSecurityTokenHandler();
            var secretKeyByte = Encoding.UTF8.GetBytes(_appsetting.SecretKey);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, nguoi.HoTen ),
                    new Claim(ClaimTypes.Email, nguoi.Email ),
                    new Claim("UserName", nguoi.UserName ),
                    new Claim("Id", nguoi.Id.ToString()),
                    //role

                    new Claim("TokenId", Guid.NewGuid().ToString()),

                }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey
                                (secretKeyByte), SecurityAlgorithms.HmacSha512Signature)

            };
            var token = JWtTokenHander.CreateToken(tokenDescription);

            return JWtTokenHander.WriteToken(token);
        }
    }
}
