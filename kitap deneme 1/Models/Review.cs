using System.ComponentModel.DataAnnotations;

namespace kitap_deneme_1.Models
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }
        public int BookId { get; set; }
        public string ReviewerName { get; set; } = string.Empty;
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}