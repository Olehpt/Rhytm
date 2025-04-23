namespace Rhytm.Models
{
    public class PlaylistLink
    {
        public int Id { get; set; }
        public int TrackId { get; set; }
        public int PlaylistId { get; set; }
        public virtual Track Track { get; set; }
        public virtual Playlist Playlist { get; set; }
    }
}
