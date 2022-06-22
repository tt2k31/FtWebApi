using MyWebApi.Models;

namespace MyWebApi.Services
{
    public interface IHangHoaRepository
    {
        List<HangHoaModel> GetAll(string? search, double? from, double? to, string sortBy, int page = 1);
       
    }
}
