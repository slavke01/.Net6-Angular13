using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoCore;

namespace ToDoInfrastructure
{
    public class ToDoListConfiguration : IEntityTypeConfiguration<ToDoList>
    {
        public void Configure(EntityTypeBuilder<ToDoList> builder)
        {
            builder.HasMany(t => t.ListItems)
                   .WithOne()
                   .HasForeignKey(td => td.ToDoListId)
                   .OnDelete(DeleteBehavior.Cascade); ;
            builder.HasKey(x => x.Id);  
            builder.Property(t=>t.Id).ValueGeneratedOnAdd();
            builder.Property(t => t.Title).IsRequired();
            builder.Property(t => t.Owner).IsRequired();
            builder.Property(t => t.Title).HasMaxLength(20);
        }
    }
}
