namespace LibraryManagementBackend
{
    public class Borrow
    {
        public int CardId { get; set; }
        public int BookId { get; set; }
        public long BorrowTime { get; set; } // Unix 时间戳
        public long ReturnTime { get; set; } // Unix 时间戳，未归还为0
    }
}