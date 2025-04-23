namespace Rhytm.Models
{
    public class Playlist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? CoverPath { get; set; }
        public int AuthorId { get; set; }
        public User Author { get; set; }
    }
}
