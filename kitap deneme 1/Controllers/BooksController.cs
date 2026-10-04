using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using kitap_deneme_1.Models;

namespace kitap_deneme_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BooksController(AppDbContext context)
        {
            _context = context;
        }

        // 1. Kitapları Listele, Filtrele ve Sayfala (GET)
        [HttpGet]
        public async Task<IActionResult> GetBooks(
            [FromQuery] string? search = null,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var query = _context.Books.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(b => b.Title.Contains(search) || b.Author.Contains(search));
            }

            var totalCount = await query.CountAsync();

            var books = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(b => new
                {
                    b.BookId,
                    b.Title,
                    b.Author,
                    AverageRating = b.Reviews.Any() ? Math.Round(b.Reviews.Average(r => r.Rating), 1) : 0,
                    ReviewCount = b.Reviews.Count
                })
                .ToListAsync();

            return Ok(new
            {
                TotalBooks = totalCount,
                CurrentPage = page,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
                Data = books
            });
        }

        // 2. Tek Bir Kitabı Detayları ve Tüm Yorumlarıyla Getir (GET)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var book = await _context.Books
                .Include(b => b.Reviews)
                .FirstOrDefaultAsync(b => b.BookId == id);

            if (book == null)
            {
                return NotFound(new { message = $"{id} ID'li kitap bulunamadı." });
            }

            return Ok(new
            {
                book.BookId,
                book.Title,
                book.Author,
                AverageRating = book.Reviews.Any() ? Math.Round(book.Reviews.Average(r => r.Rating), 1) : 0,
                ReviewCount = book.Reviews.Count,
                Reviews = book.Reviews.OrderByDescending(r => r.CreatedAt).ToList()
            });
        }

        // 3. Yeni Kitap Ekle (POST)
        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] Book book)
        {
            if (string.IsNullOrWhiteSpace(book.Title) || string.IsNullOrWhiteSpace(book.Author))
            {
                return BadRequest("Kitap başlığı ve yazar adı boş bırakılamaz.");
            }

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBookById), new { id = book.BookId }, book);
        }

        // 4. Kitaba Yorum/Puan Ekle (POST)
        [HttpPost("add-review")]
        public async Task<IActionResult> AddReview([FromBody] Review review)
        {
            if (review.Rating < 1 || review.Rating > 10)
            {
                return BadRequest("Puan 1 ile 10 arasında olmalıdır.");
            }

            var bookExists = await _context.Books.AnyAsync(b => b.BookId == review.BookId);
            if (!bookExists)
            {
                return NotFound("Yorum eklenmek istenen kitap veritabanında bulunamadı.");
            }

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Yorum ve puan başarıyla kaydedildi!", review });
        }

        // 5. Kitap Sil (DELETE)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound(new { message = "Silinmek istenen kitap bulunamadı." });
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return Ok(new { message = $"'{book.Title}' başarıyla silindi." });
        }
    }
}