using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBrokarageChallenge.Application.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
namespace StockBrokarageChallenge.Application.Shared.Data.EntitiesConfiguration
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name).HasMaxLength(50).IsRequired();
            builder.Property(c => c.Cpf).IsFixedLength().HasMaxLength(11).IsRequired();

            builder
                .HasOne(c => c.Account)
                .WithOne(a => a.Customer)
                .HasForeignKey<Account>(a => a.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
