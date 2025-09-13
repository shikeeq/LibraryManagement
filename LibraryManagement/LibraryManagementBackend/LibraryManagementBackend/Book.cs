namespace LibraryManagementBackend
{
    public class Book
    {
        public int BookId { get; set; } // 对应 book_id
        public string Category { get; set; }
        public string Title { get; set; }
        public string Press { get; set; }
        public int PublishYear { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}