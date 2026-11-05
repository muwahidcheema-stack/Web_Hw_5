namespace Web_HW___5.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int AuthorId { get; set; }
    }
}
