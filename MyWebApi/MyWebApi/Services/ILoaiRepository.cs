using MyWebApi.Data;

namespace MyWebApi.Services
{
    public interface ILoaiRepository
    {
        List<LoaiVM> GetAll();
        LoaiVM GetById(int id);
        LoaiVM Add(LoaiModel loai);
        void UpdateById(LoaiVM loai);

        void DeleteById(int id);
    }
}
