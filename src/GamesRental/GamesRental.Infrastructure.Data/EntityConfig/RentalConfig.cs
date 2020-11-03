using GamesRental.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GamesRental.Infrastructure.Data.EntityConfig
{
    public class RentalConfig : IEntityTypeConfiguration<Rental>
    {
        public void Configure(EntityTypeBuilder<Rental> builder)
        {
            builder.ToTable("Rents");

            builder.Property(x => x.Date)
                   .HasColumnType("datetime")
                   .IsRequired();

            builder.Property(x => x.DateEstimated)
                   .HasColumnType("datetime")
                   .IsRequired(false);

            builder.Property(x => x.DateFinish)
                   .HasColumnType("datetime")
                   .IsRequired(false);

            builder.HasOne(x => x.Game)
                   .WithMany(x => x.Rents)
                   .HasForeignKey(x => x.IdGame)
                   .HasPrincipalKey(x => x.Id);

            builder.HasOne(x => x.Friend)
                   .WithMany(x => x.Rents)
                   .HasForeignKey(x => x.IdFriend)
                   .HasPrincipalKey(x => x.Id);            
        }
    }
}
