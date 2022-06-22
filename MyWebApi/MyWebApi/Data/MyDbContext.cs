using Microsoft.EntityFrameworkCore;

namespace MyWebApi.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions options) : base(options)
        { }

        public DbSet<HangHoa> hangHoas { get; set; }
        public DbSet<Loai> Loais { get; set; }
        public DbSet<DonHang> donHang { get; set; }
        public DbSet<DonHangChiTiet> DonHangChiTiets { get; set; }
        public DbSet<NguoiDung> NguoiDungs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<DonHang>(e =>
            {
                e.ToTable("DonHang");
                e.HasKey(dh => dh.MaDh);
                e.Property(dh => dh.NgayDat).HasDefaultValueSql("getutcdate()");
                e.Property(dh => dh.NguoiNhan).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<DonHangChiTiet>(e => 
            {
                e.ToTable("ChiTietDonHAng");
                e.HasKey(e => new { e.MaDh, e.Id});
                
                e.HasOne(e => e.DonHang)
                        .WithMany(e => e.DonHangChiTiets)
                        .HasForeignKey(e => e.MaDh)
                        .HasConstraintName("FK_DHCT_DH");

                e.HasOne(e => e.HangHoa)
                        .WithMany(e => e.DonHangChiTiets)
                        .HasForeignKey(e => e.Id)
                        .HasConstraintName("FK_DHCT_HH");
            });
            modelBuilder.Entity<NguoiDung>(e =>
            {
                e.HasIndex(e => e.Id).IsUnique();
                e.Property(e => e.HoTen).IsRequired().HasMaxLength(50);
                e.Property(e => e.Email).IsRequired().HasMaxLength(50);

            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
