﻿using DA.Models;
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
            modelBuilder.Entity<Phong>().ToTable("Phong");
            modelBuilder.Entity<DichVu>().ToTable("DichVu");
            modelBuilder.Entity<Phong_DichVu>().ToTable("Phong_DichVu");
            modelBuilder.Entity<HopDong>().ToTable("HopDongs");
            modelBuilder.Entity<HoaDon>().ToTable("HoaDons"); // KHỚP TÊN BẢNG
            modelBuilder.Entity<ChiTietHoaDon>().ToTable("ChiTietHoaDon");
            modelBuilder.Entity<PhanHoi>().ToTable("PhanHoi");
            modelBuilder.Entity<LichSuLuuTru>().ToTable("LichSuLuuTrus");

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
                .WithMany()
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

            modelBuilder.Entity<LichSuLuuTru>()
    .HasOne(x => x.NguoiThue)
    .WithMany()
    .HasForeignKey(x => x.MaNguoiThue)
    .OnDelete(DeleteBehavior.Cascade); // hoặc Restrict

            modelBuilder.Entity<LichSuLuuTru>()
.HasOne(x => x.Phong)
.WithMany()
.HasForeignKey(x => x.MaPhong)
.OnDelete(DeleteBehavior.Restrict); // hoặc NoAction

            modelBuilder.Entity<HopDong>()
    .HasOne(h => h.NguoiThue)
    .WithMany()
    .HasForeignKey(h => h.MaNguoiThue)
    .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<HopDong>()
                .HasOne(h => h.Phong)
                .WithMany()
                .HasForeignKey(h => h.MaPhong)
                .OnDelete(DeleteBehavior.Restrict); // Tránh multiple cascade paths

            modelBuilder.Entity<HoaDon>()
       .HasOne(hd => hd.HopDong)
       .WithMany(hd => hd.HoaDons)
       .HasForeignKey(hd => hd.MaHopDong);

        }
    }
}
