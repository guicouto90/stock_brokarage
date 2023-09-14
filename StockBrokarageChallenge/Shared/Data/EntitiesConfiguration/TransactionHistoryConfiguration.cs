using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBrokarageChallenge.Application.Shared.Models;

namespace StockBrokarageChallenge.Application.Shared.Data.EntitiesConfiguration
{
    public class TransactionHistoryConfiguration : IEntityTypeConfiguration<TransactionHistory>
    {
        public void Configure(EntityTypeBuilder<TransactionHistory> builder)
        {
            builder.HasKey(th => th.Id);

            builder.Property(th => th.TransactionValue).IsRequired();
            builder.Property(th => th.TypeTransaction).HasConversion<string>().IsRequired();
            builder.Property(th => th.StockQuantity);
            builder.Property(th => th.StockCode).HasMaxLength(5);
            builder.Property(th => th.Date).IsRequired();
        }
    }
}
