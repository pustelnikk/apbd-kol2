using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using kol2b.Models;

namespace kol2b.Models
{
    public class MusicDbContext : DbContext
    {
        public DbSet<Album> Album { get; set; }
        public DbSet<Musician> Musician { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<MusicianTrack> MusicianTrack { get; set; }
        public DbSet<MusicLabel> MusicLabel { get; set; }

        public MusicDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var musicians = new List<Musician>
            {
                new Musician
                {
                    IdMusician = 1,
                    FirstName = "Jan",
                    LastName = "Kowalski",
                    Nickname = "Lil Drzoni"

                },
                new Musician
                {
                    IdMusician = 2,
                    FirstName = "Piotr",
                    LastName = "Nowag",
                    Nickname = "Big papi"

                }
            };

            var musicLabels = new List<MusicLabel>
            {
                new MusicLabel
                {
                    IdMusicLabel = 1,
                    Name = "Moya"
                },
                new MusicLabel
                {
                    IdMusicLabel = 2,
                    Name = "Twoya"
                }
            };

            var musicianTrack = new List<MusicianTrack>
            {
                new MusicianTrack
                {
                    IdTrack = 1,
                    IdMusician = 1
                },
                new MusicianTrack
                {
                    IdTrack = 2,
                    IdMusician = 2
                }
            };
            var tracks = new List<Track>
            {
                new Track 
                {
                    IdTrack = 1,
                    TrackName = "song1",
                    Duration = 4,
                    IdMusicAlbum = 1
                },

                new Track
                {
                    IdTrack = 2,
                    TrackName = "song2",
                    Duration = 3,
                    IdMusicAlbum = 1
                },
                new Track
                {
                    IdTrack = 3,
                    TrackName = "song3",
                    Duration = 6,
                    IdMusicAlbum = 2
                }
            };

            var albums = new List<Album>
            {
                new Album
                {
                    IdAlbum = 1,
                    AlbumName = "name1",
                    PublishDate=DateTime.Now,
                    IdMusicLabel=1
                },
                new Album
                {
                    IdAlbum = 2,
                    AlbumName = "name2",
                    PublishDate=DateTime.Now,
                    IdMusicLabel=2
                },
                new Album
                {
                    IdAlbum = 3,
                    AlbumName = "name3",
                    PublishDate=DateTime.Now,
                    IdMusicLabel=2
                },
            };
            modelBuilder.Entity<Musician>(e =>
            {

                e.HasKey(e => e.IdMusician);
                e.Property(e => e.FirstName).HasMaxLength(30).IsRequired();
                e.Property(e => e.LastName).HasMaxLength(50).IsRequired();
                e.Property(e => e.Nickname).HasMaxLength(20).IsRequired(false);
                e.HasData(musicians);
                e.ToTable("Musician");
            });

            modelBuilder.Entity<MusicLabel>(e =>
            {
                e.HasKey(e => e.IdMusicLabel);
                e.Property(e => e.Name).HasMaxLength(30).IsRequired();
                e.HasData(musicLabels);
                e.ToTable("MusicLabel");
            });

            modelBuilder.Entity<MusicianTrack>(e =>
            {
                e.HasKey(e => new { e.IdMusician, e.IdTrack });

                e.HasOne(e => e.Musician)
                .WithMany(e => e.MusicianTrack)
                .HasForeignKey(e => e.IdMusician)
                .OnDelete(DeleteBehavior.ClientSetNull);
                
                e.HasOne(e => e.Track)
                .WithMany(e => e.MusicianTrack)
                .HasForeignKey(e => e.IdTrack)
                .OnDelete(DeleteBehavior
                .ClientSetNull);

                e.HasData(musicianTrack);
                e.ToTable("Musician_Track");

            });

            modelBuilder.Entity<Track>(e => {
                e.HasKey(e => e.IdTrack);
                e.Property(e => e.TrackName).HasMaxLength(20).IsRequired();
                e.Property(e => e.Duration).IsRequired();

                e.HasOne(e => e.Album)
                .WithMany(e => e.Track)
                .HasForeignKey(e => e.IdMusicAlbum)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .IsRequired(false);


                e.HasData(tracks);
                e.ToTable("Track");

            });

            modelBuilder.Entity<Album>(e =>
            {
                e.HasKey(e => e.IdAlbum);
                e.Property(e => e.AlbumName).HasMaxLength(30).IsRequired();
                e.Property(e => e.PublishDate).IsRequired();

                e.HasOne(e => e.MusicLabel)
                .WithMany(e => e.Album)
                .HasForeignKey(e => e.IdMusicLabel)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .IsRequired();
               
                e.HasData(albums);
                e.ToTable("Album");
            });
        }
    }
}
