using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoCore;

namespace ToDoInfrastructure
{
    public class ToDoListItemConfiguration : IEntityTypeConfiguration<ToDoListItem>
    {
        public void Configure(EntityTypeBuilder<ToDoListItem> builder)
        {
            /*
            builder.HasOne(td => td.ToDoList)
                .WithMany(tdl => tdl.ListItems)
                .HasForeignKey(td => td.ToDoListId)
                .OnDelete(DeleteBehavior.Cascade);
            */
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(t => t.Description)
                   .IsRequired();
            builder.Property(t => t.Description)
                   .HasMaxLength(150);//mozda treba izmjenit
        }
    }
}
