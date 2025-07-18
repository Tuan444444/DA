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
    }
}
