namespace Rhytm.Models
{
    public class Authorship
    {
        public int Id { get; set; }
        public int TrackId { get; set; }
        public int ArtistId { get; set; }
        public virtual Artist Artist { get; set; }
        public virtual Track Track { get; set; }  
    }
}
