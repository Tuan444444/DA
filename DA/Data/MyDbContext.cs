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
       
        public DbSet<ChuNha> ChuNhas { get; set; }
        public DbSet<NguoiThue> NguoiThues { get; set; }
        public DbSet<TaiKhoan> TaiKhoans { get; set; }
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
        }
    }
}
