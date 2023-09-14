using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBrokarageChallenge.Application.Shared.Models;

namespace StockBrokarageChallenge.Application.Shared.Data.EntitiesConfiguration
{
    public class StockHistoryPriceCofiguration
        : IEntityTypeConfiguration<StockHistoryPrice>
    {
        public void Configure(EntityTypeBuilder<StockHistoryPrice> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.ActualPrice).HasPrecision(10, 2).IsRequired();

            builder.Property(shp => shp.Id).UseIdentityColumn();
        }
    }
}
