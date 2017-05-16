using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RestoreFromLocal
{
    public partial class SlackHistoryViewerDbContext : DbContext
    {
        public virtual DbSet<Channels> Channels { get; set; }
        public virtual DbSet<Messages> Messages { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            optionsBuilder.UseSqlServer(@"Data Source=shv.database.windows.net;Initial Catalog=SlackHistoryViewer;Integrated Security=SSPI; User ID = msp; Password = 1234QWERasdf;Trusted_Connection=False;Encrypt=True");
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