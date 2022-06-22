using Microsoft.EntityFrameworkCore;
using MyWebApi.Data;
using MyWebApi.Models;

namespace MyWebApi.Services
{
    public class HangHoaRepository : IHangHoaRepository
    {
        private readonly MyDbContext _context;
        public static int PAGESIZE { get; set; } = 5;

        public HangHoaRepository(MyDbContext context)
        {
            _context = context;
        }

        public List<HangHoaModel> GetAll(string? search, double? from, double? to, string sortBy, int page = 1)
        {
            var allProduct = _context.hangHoas.Include(hh => hh.Loai).AsQueryable();

            #region Filtering
            if (!string.IsNullOrEmpty(search))
            {
                allProduct = allProduct.Where(hh => hh.TenHh.Contains(search));
            }
            if(from.HasValue)
            {
                allProduct = allProduct.Where(hh => hh.DonGia >= from);
            }
            if (to.HasValue)
            {
                allProduct = allProduct.Where(hh => hh.DonGia <= to);
            }
            #endregion

            #region Sort
            //sx theo tenhh
            allProduct = allProduct.OrderBy(hh => hh.TenHh);
            if(!string.IsNullOrEmpty(sortBy))
            {
                switch(sortBy)
                {
                    case "tenhh_desc":
                        allProduct = allProduct.OrderBy(hh => hh.TenHh);
                        break;
                    case "gia_asc":
                        allProduct = allProduct.OrderBy(hh => hh.DonGia);
                        break;
                    case "gia_desc":
                        allProduct = allProduct.OrderBy(hh => hh.DonGia);
                        break;
                }
            }
            #endregion

            //#region paging
            ////allProduct = allProduct.Skip((page - 1) * PAGESIZE).Take(PAGESIZE);

            //#endregion
            //var rs = allProduct.Select(hh => new HangHoaModel
            //{
            //    MaHangHoa = hh.Id,
            //    TenHangHoa = hh.TenHh,
            //    DonGia = hh.DonGia,
            //    TenLoai = hh.Loai.TenName,
            //});

            //return rs.ToList();


            var rs = PageList<MyWebApi.Data.HangHoa>.Create(allProduct, page, PAGESIZE);
            return rs.Select(hh => new HangHoaModel
            {
                MaHangHoa = hh.Id,
                TenHangHoa = hh.TenHh,
                DonGia = hh.DonGia,
                TenLoai = hh.Loai?.TenName,
            }).ToList();

        }
    }
}
