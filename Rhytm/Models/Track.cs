namespace Rhytm.Models
{
    public class Track
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FilePath { get; set; }
        public string? CoverPath { get; set; }
        public int Auditions { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime UploadDate { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public virtual ICollection<PlaylistLink> PlaylistLinks { get; set; }
        public virtual ICollection<LikedLink> LikedLinks { get; set; }
    }
}
