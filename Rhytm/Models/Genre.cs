namespace Rhytm.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Track> Tracks { get; set; } = new List<Track>();
    }
}
