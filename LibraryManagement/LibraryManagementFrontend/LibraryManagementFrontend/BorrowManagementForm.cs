using System;
using System.Collections.Generic;
using System.Windows.Forms;
using LibraryManagementBackend;
using System.Linq;

namespace LibraryManagementFrontend.Forms
{
    public partial class BorrowManagementForm : Form
    {
        private BorrowRepository borrowRepository;
        private BookRepository bookRepository;
        private CardRepository cardRepository;

        public BorrowManagementForm(BorrowRepository borrowRepo, BookRepository bookRepo, CardRepository cardRepo)
        {
            InitializeComponent();
            borrowRepository = borrowRepo;
            bookRepository = bookRepo;
            cardRepository = cardRepo;
            LoadBorrowRecords();
        }

        private void LoadBorrowRecords()
        {
            var borrows = borrowRepository.GetAllBorrows();
            borrowListView.Items.Clear();
            foreach (var borrow in borrows)
            {
                var borrowTime = DateTimeOffset.FromUnixTimeSeconds(borrow.BorrowTime).LocalDateTime;
                var returnTime = borrow.ReturnTime == 0 ? "Not Returned" : DateTimeOffset.FromUnixTimeSeconds(borrow.ReturnTime).LocalDateTime.ToString();
                var item = new ListViewItem(new[]
                {
                    borrow.CardId.ToString(),
                    borrow.BookId.ToString(),
                    borrowTime.ToString(),
                    returnTime
                });
                borrowListView.Items.Add(item);
            }
        }

        private void btnBorrowBook_Click(object sender, EventArgs e)
        {
            try
            {
                int cardId = int.Parse(txtCardId.Text);
                int bookId = int.Parse(txtBookId.Text);
                var book = bookRepository.GetBookById(bookId);

                if (book != null && book.Stock > 0)
                {
                    var borrow = new Borrow
                    {
                        CardId = cardId,
                        BookId = bookId,
                        BorrowTime = DateTimeOffset.Now.ToUnixTimeSeconds(),
                        ReturnTime = 0
                    };
                    borrowRepository.AddBorrow(borrow);
                    book.Stock--;
                    bookRepository.UpdateBook(book);
                    MessageBox.Show("Book borrowed successfully.");
                }
                else
                {
                    MessageBox.Show("Book is not available.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            LoadBorrowRecords();
            LoadData();
        }

        private void btnReturnBook_Click(object sender, EventArgs e)
        {
            try
            {
                int cardId = int.Parse(txtCardId.Text);
                int bookId = int.Parse(txtBookId.Text);
                var borrow = borrowRepository.GetBorrowByCardAndBookId(cardId, bookId);

                if (borrow != null && borrow.ReturnTime == 0)
                {
                    borrow.ReturnTime = DateTimeOffset.Now.ToUnixTimeSeconds();
                    borrowRepository.UpdateBorrowReturnTime(borrow);

                    var book = bookRepository.GetBookById(bookId);
                    if (book != null)
                    {
                        book.Stock++;
                        bookRepository.UpdateBook(book);
                    }
                    MessageBox.Show("Book returned successfully.");
                }
                else
                {
                    MessageBox.Show("No active borrow record found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            LoadBorrowRecords();
            LoadData();
        }

        private void BorrowManagementForm_Load(object sender, EventArgs e)
        {
            // Set up columns for dataGridViewBooks
            dataGridViewBooks.AutoGenerateColumns = true;
            dataGridViewBooks.Columns.Add("BookId", "Book ID");
            dataGridViewBooks.Columns.Add("Category", "Category");
            dataGridViewBooks.Columns.Add("Title", "Title");
            dataGridViewBooks.Columns.Add("Press", "Press");
            dataGridViewBooks.Columns.Add("PublishYear", "Publish Year");
            dataGridViewBooks.Columns.Add("Author", "Author");
            dataGridViewBooks.Columns.Add("Price", "Price");
            dataGridViewBooks.Columns.Add("Stock", "Stock");

            // Set up columns for dataGridViewCards
            dataGridViewCards.AutoGenerateColumns = true;
            dataGridViewCards.Columns.Add("CardId", "Card ID");
            dataGridViewCards.Columns.Add("Name", "Name");
            dataGridViewCards.Columns.Add("Department", "Department");
            dataGridViewCards.Columns.Add("Type", "Type");

            // Set up columns for allBooks
            allBooks.AutoGenerateColumns = true;

            LoadData();
        }

        private void LoadData()
        {
            List<Book> books = bookRepository.GetAllBooks();
            List<Card> cards = cardRepository.GetAllCards();

            dataGridViewBooks.DataSource = books;
            dataGridViewCards.DataSource = cards;
            allBooks.DataSource = books;
            allBooks.Refresh();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void borrowListView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        // 查询书名按钮事件
        private void BtnSearchTitle_Click(object sender, EventArgs e)
        {
            string title = txtSearchTitle.Text.Trim();
            if (string.IsNullOrEmpty(title))
            {
                MessageBox.Show("请输入书名！");
                return;
            }
            var books = bookRepository.SearchBooksByTitle(title);
            allBooks.DataSource = null;
            allBooks.DataSource = books;
        }

        // 查询作者按钮事件
        private void BtnSearchAuthor_Click(object sender, EventArgs e)
        {
            string author = txtSearchAuthor.Text.Trim();
            if (string.IsNullOrEmpty(author))
            {
                MessageBox.Show("请输入作者！");
                return;
            }
            var books = bookRepository.SearchBooksByAuthor(author);
            allBooks.DataSource = null;
            allBooks.DataSource = books;
        }
    }
}