using System.ComponentModel.DataAnnotations;

namespace kitap_deneme_1.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;

        public List<Review> Reviews { get; set; } = new();
    }
}