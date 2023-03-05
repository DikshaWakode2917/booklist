
namespace BooksListitingApis.Models
{
    [Index(nameof(ISBN), IsUnique = true)]
    public class Books
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
    }
}
