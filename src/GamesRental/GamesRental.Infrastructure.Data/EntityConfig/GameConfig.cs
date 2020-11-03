using GamesRental.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GamesRental.Infrastructure.Data.EntityConfig
{
    public class GameConfig : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.ToTable("Games");

            builder.Property(x => x.Name)
                    .HasColumnType("varchar(180)")
                    .IsRequired();

            builder.Property(x => x.Description)
                   .HasColumnType("text")
                   .IsRequired(false);

            builder.Property(x => x.Genre)
                    .HasColumnType("int")
                    .IsRequired();

            builder.Property(x => x.ReleaseDate)
                    .HasColumnType("dateTime")
                    .IsRequired(false);
        }
    }
}
