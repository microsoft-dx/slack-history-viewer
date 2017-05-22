using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SlackHistoryViewer.Database.Models;

namespace SlackHistoryViewer.Database
{
    public partial class SlackHistoryViewerDbContext : DbContext
    {
        //TODO Until System.Configuration is available
        private readonly string _connectionString = null;

        public virtual DbSet<Channels> Channels { get; set; }

        public virtual DbSet<Messages> Messages { get; set; }

        public virtual DbSet<Users> Users { get; set; }

        public SlackHistoryViewerDbContext(string connectionString)
        {
            this._connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this._connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Channels>(entity =>
            {
                entity.Property(e => e.ChannelId)
                    .IsRequired()
                    .HasColumnType("varchar(9)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(256)");
            });

            modelBuilder.Entity<Messages>(entity =>
            {
                entity.Property(e => e.JsonData)
                    .IsRequired()
                    .HasColumnType("varchar(max)");

                entity.Property(e => e.MessageId)
                    .IsRequired()
                    .HasColumnType("varchar(32)");

                entity.HasOne(d => d.Channel)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.ChannelId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Messages_ToChannels");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Messages_ToUsers");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(256)");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });
        }
    }
}