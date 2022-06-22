using Microsoft.EntityFrameworkCore;
using MyWebApi.Data;

namespace MyWebApi.Services
{
    public class LoaiRepository : ILoaiRepository
    {
        private readonly MyDbContext _context;

        public LoaiRepository(MyDbContext context)
        {
            _context = context;
        }

        public LoaiVM Add(LoaiModel loai)
        {
            var _loai = new Loai
            {
                TenName = loai.TenName
            };
            _context.Add(_loai);
            _context.SaveChanges();
            return new LoaiVM
            {
                MaLoai = _loai.MaLoai,
                TenName = _loai.TenName,
            };
        }

        public void DeleteById(int id)
        {
            var loai = _context.Loais.SingleOrDefault(l => l.MaLoai == id);
            if (loai != null)
            {
                _context.Remove(loai);
                _context.SaveChanges();
            };
            
        }

        public List<LoaiVM> GetAll()
        {
            var loais = _context.Loais.Select(x => new LoaiVM
            {
                MaLoai = x.MaLoai,
                TenName = x.TenName,
            });
            return loais.ToList();
        }

        public LoaiVM GetById(int id)
        {
            var loai = _context.Loais.SingleOrDefault(l => l.MaLoai == id);
            if(loai == null)
            {
                return null;
            }
            return new LoaiVM
            {
                MaLoai = loai.MaLoai,
                TenName = loai.TenName,
            };
        }

        public void UpdateById(LoaiVM loai)
        {
            var _loai = _context.Loais.SingleOrDefault(l => l.MaLoai == loai.MaLoai);
            if (_loai != null)
            {
                _loai.TenName = loai.TenName;
                _context.SaveChanges();
            };
        }
    }
}
