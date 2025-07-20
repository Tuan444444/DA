using DA.Models;
using DA.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;

namespace DA.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }
        public DbSet<Phong> Phongs { get; set; }
        public DbSet<ChuNha> ChuNhas { get; set; }
        public DbSet<NguoiThue> NguoiThues { get; set; }
        public DbSet<TaiKhoan> TaiKhoans { get; set; }
        public DbSet<DichVu> DichVus { get; set; }
        public DbSet<Phong_DichVu> Phong_DichVus { get; set; }
        public DbSet<HopDong> HopDongs { get; set; }
        public DbSet<HoaDon> HoaDons { get; set; }
        public DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; }
        public DbSet<PhanHoi> PhanHois { get; set; }
        public DbSet<LichSuLuuTru> LichSuLuuTrus { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaiKhoan>().ToTable("TaiKhoan");
            modelBuilder.Entity<ChuNha>().ToTable("ChuNha");
            modelBuilder.Entity<NguoiThue>().ToTable("NguoiThue");

            // 1:1 ChuNha - TaiKhoan
            modelBuilder.Entity<TaiKhoan>()
                .HasOne(t => t.ChuNha)
                .WithOne(cn => cn.TaiKhoan)
                .HasForeignKey<ChuNha>(cn => cn.MaTaiKhoan)
                .OnDelete(DeleteBehavior.Cascade);

            // 1:1 NguoiThue - TaiKhoan
            modelBuilder.Entity<TaiKhoan>()
                .HasOne(t => t.NguoiThue)
                .WithOne(nt => nt.TaiKhoan)
                .HasForeignKey<NguoiThue>(nt => nt.MaTaiKhoan)
                .OnDelete(DeleteBehavior.Cascade);
            //
            modelBuilder.Entity<Phong>().ToTable("Phong");
            // FK Phong -> ChuNha
            modelBuilder.Entity<Phong>()
    .HasOne(p => p.ChuNha)
    .WithMany(cn => cn.Phongs) // 🔧 Quan trọng: Khai báo đúng quan hệ ngược
    .HasForeignKey(p => p.MaChuNha)
    .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Phong_DichVu>()
           .HasKey(p => new { p.MaPhong, p.MaDichVu });

            // FK mapping chuẩn
            modelBuilder.Entity<Phong_DichVu>()
                .HasOne(p => p.Phong)
                .WithMany(p => p.Phong_DichVus)
                .HasForeignKey(p => p.MaPhong);

            modelBuilder.Entity<Phong_DichVu>()
                .HasOne(p => p.DichVu)
                .WithMany(d => d.Phong_DichVus)
                .HasForeignKey(p => p.MaDichVu);
        }
    }
}
