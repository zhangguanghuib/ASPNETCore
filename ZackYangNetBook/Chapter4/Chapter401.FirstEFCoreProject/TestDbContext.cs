using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter401.FirstEFCoreProject
{
    class TestDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; } = null!;//DbSet属性，表示数据库中的表

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            string connStr = @"Data Source=.\sqlexpress;Initial Catalog=EFCoreTest;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;";//连接字符串
            optionsBuilder.UseSqlServer(connStr);//使用SQL Server数据库
            //optionsBuilder.LogTo(Console.WriteLine);//输出SQL语句到控制台
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}
