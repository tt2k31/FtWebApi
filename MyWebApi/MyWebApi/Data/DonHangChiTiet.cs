using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyWebApi.Data
{
    public class DonHangChiTiet
    {
        public Guid MaDh { get; set; }
        [DisplayName("Ma hang hoa")]
        public int Id { get; set; }

        public int SoLuong { get; set; }
        public double DonGia { get; set; }
        public byte GiamGia { get; set; }

        //relationship
        public DonHang DonHang { get; set; }
        public HangHoa HangHoa { get; set; }
    }
}
