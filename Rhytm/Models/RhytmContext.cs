using Microsoft.EntityFrameworkCore;
namespace Rhytm.Models
{
    public class RhytmContext:DbContext
    {
        public virtual DbSet<Track> Tracks { get; set; }
        public virtual DbSet<Artist> Artists { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Playlist> Playlists { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<LikedLink> LikedLinks { get; set; }
        public virtual DbSet<PlaylistLink> PlaylistLinks { get; set; }
        public virtual DbSet<Authorship> Authorships { get; set; }
    }
}
