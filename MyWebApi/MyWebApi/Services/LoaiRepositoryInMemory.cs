using MyWebApi.Data;

namespace MyWebApi.Services
{
    public class LoaiRepositoryInMemory : ILoaiRepository
    {
        static List<LoaiVM> loaiVMs = new List<LoaiVM> 
        {
            new LoaiVM{ MaLoai = 1, TenName ="tv"},
            new LoaiVM{ MaLoai = 2, TenName ="dh"},
            new LoaiVM{ MaLoai = 3, TenName ="xm"},
            new LoaiVM{ MaLoai = 4, TenName ="tl"}

        };
        public LoaiVM Add(LoaiModel loai)
        {
            var _loai = new LoaiVM
            {
                MaLoai = loaiVMs.Max(l => l.MaLoai) + 1,
                TenName = loai.TenName
            };
            loaiVMs.Add(_loai);
            return _loai;
        }

        public void DeleteById(int id)
        {
            var _loai = loaiVMs.SingleOrDefault(l => l.MaLoai == id);
            if (_loai != null)
            {
                loaiVMs.Remove(_loai);
            }
        }

        public List<LoaiVM> GetAll()
        {
            return loaiVMs;
        }

        public LoaiVM GetById(int id)
        {
            return loaiVMs.SingleOrDefault(l => l.MaLoai == id);
        }

        public void UpdateById(LoaiVM loai)
        {
            var _loai = loaiVMs.SingleOrDefault(l => l.MaLoai == loai.MaLoai);
            if (_loai != null)
            {
                _loai.TenName = loai.TenName;
            }
        }
    }
}
