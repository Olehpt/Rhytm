namespace Rhytm.Models
{
    public class LikedLink
    {
        public int Id { get; set; }
        public int TrackId { get; set; }
        public int UserId { get; set; }
        public virtual Track Track { get; set; }
        public virtual User User { get; set; }
    }
}
