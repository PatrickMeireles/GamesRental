using GamesRental.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GamesRental.Infrastructure.Data.EntityConfig
{
    public class FriendConfig : IEntityTypeConfiguration<Friend>
    {
        public void Configure(EntityTypeBuilder<Friend> builder)
        {
            builder.ToTable("Friends");

            builder.Property(x => x.Email)
                   .HasColumnType("varchar(180)")
                   .IsRequired();

            builder.Property(x => x.Name)
                   .HasColumnType("varchar(180)")
                   .IsRequired();
        }
    }
}
