using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter401.FirstEFCoreProject
{
    class BookEntityConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("T_Books");//指定表名
            builder.Property(e=>e.Title).HasMaxLength(50).IsRequired();//指定标题最大长度为50，不能为空
            builder.Property(e=>e.AuthorName).HasMaxLength(20).IsRequired();//指定作者名字最大长度为20，不能为空
        }
    }
}
