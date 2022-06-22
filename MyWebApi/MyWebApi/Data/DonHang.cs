namespace MyWebApi.Data
{
    public enum TinhTrangDonHang 
    {
        New = 0, Payment = 1, Complete = 2, Cancel = -1
    }
    public class DonHang
    {
        public Guid MaDh { get; set; }
        public DateTime NgayDat { get; set; }
        public DateTime NgayGiao { get; set; }
        public TinhTrangDonHang tinhTrangDonHang { get; set; }
        public string NguoiNhan { get; set; }
        public string DiaChi { get; set; }
        public int SDT { get; set; }

        public ICollection<DonHangChiTiet> DonHangChiTiets { get; set; }
        public DonHang()
        {
            DonHangChiTiets = new List<DonHangChiTiet>();
        }
    }
}
