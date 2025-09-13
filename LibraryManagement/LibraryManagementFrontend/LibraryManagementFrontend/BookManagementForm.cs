using System;
using System.Windows.Forms;
using LibraryManagementBackend;
using System.Collections.Generic;

namespace LibraryManagementFrontend.Forms
{
    public partial class BookManagementForm : Form
    {
        private readonly BookRepository _bookRepository;

        // 构造函数，注册窗体加载事件
        public BookManagementForm(BookRepository bookRepository)
        {
            InitializeComponent();
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
            this.Load += BookManagementForm_Load;
        }

        private void BookManagementForm_Load(object sender, EventArgs e)
        {
            RefreshBookList();
        }

        private void btnAddBook_Click(object sender, EventArgs e)
        {
            string category = txtCategory.Text;
            string title = txtTitle.Text;
            string press = txtPress.Text;
            string publishYearText = txtPublishYear.Text;
            string author = txtAuthor.Text;
            string priceText = txtPrice.Text;
            string stockText = txtStock.Text;

            if (string.IsNullOrWhiteSpace(category) ||
                string.IsNullOrWhiteSpace(title) ||
                string.IsNullOrWhiteSpace(press) ||
                string.IsNullOrWhiteSpace(publishYearText) ||
                string.IsNullOrWhiteSpace(author) ||
                string.IsNullOrWhiteSpace(priceText) ||
                string.IsNullOrWhiteSpace(stockText))
            {
                MessageBox.Show("All fields except Book ID are required. Please fill in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(publishYearText, out int publishYear) ||
                !decimal.TryParse(priceText, out decimal price) ||
                !int.TryParse(stockText, out int stock))
            {
                MessageBox.Show("Please enter valid numbers.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Book newBook = new Book
            {
                Category = category,
                Title = title,
                Press = press,
                PublishYear = publishYear,
                Author = author,
                Price = price,
                Stock = stock
            };

            try
            {
                _bookRepository.AddBook(newBook);
                MessageBox.Show("Book added successfully.");
                RefreshBookList();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdateBook_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtBookId.Text, out int bookId))
            {
                MessageBox.Show("Please enter a valid book ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Book existingBook = _bookRepository.GetBookById(bookId);
            if (existingBook == null)
            {
                MessageBox.Show("Book not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string category = txtCategory.Text;
            string title = txtTitle.Text;
            string press = txtPress.Text;
            string publishYearText = txtPublishYear.Text;
            string author = txtAuthor.Text;
            string priceText = txtPrice.Text;
            string stockText = txtStock.Text;

            if (!string.IsNullOrWhiteSpace(category))
                existingBook.Category = category;
            if (!string.IsNullOrWhiteSpace(title))
                existingBook.Title = title;
            if (!string.IsNullOrWhiteSpace(press))
                existingBook.Press = press;
            if (!string.IsNullOrWhiteSpace(publishYearText))
            {
                if (int.TryParse(publishYearText, out int publishYear))
                    existingBook.PublishYear = publishYear;
                else
                {
                    MessageBox.Show("Please enter a valid publish year.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            if (!string.IsNullOrWhiteSpace(author))
                existingBook.Author = author;
            if (!string.IsNullOrWhiteSpace(priceText))
            {
                if (decimal.TryParse(priceText, out decimal price))
                    existingBook.Price = price;
                else
                {
                    MessageBox.Show("Please enter a valid price.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            if (!string.IsNullOrWhiteSpace(stockText))
            {
                if (int.TryParse(stockText, out int stock))
                    existingBook.Stock = stock;
                else
                {
                    MessageBox.Show("Please enter a valid stock quantity.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            try
            {
                _bookRepository.UpdateBook(existingBook);
                MessageBox.Show("Book updated successfully.");
                RefreshBookList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeleteBook_Click(object sender, EventArgs e)
        {
            string bookIdText = txtBookId.Text;

            if (string.IsNullOrWhiteSpace(bookIdText))
            {
                MessageBox.Show("Please enter a valid book ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(bookIdText, out int bookId))
            {
                MessageBox.Show("Book ID must be a valid number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                Book bookToDelete = _bookRepository.GetBookById(bookId);
                if (bookToDelete == null)
                {
                    MessageBox.Show($"Book with ID {bookId} does not exist, cannot delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _bookRepository.DeleteBook(bookId);
                MessageBox.Show("Book deleted successfully.");
                RefreshBookList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnViewBooks_Click(object sender, EventArgs e)
        {
            List<Book> books = _bookRepository.GetAllBooks();

            lstBooks.Items.Clear();
            lstBooks.Columns.Clear();

            lstBooks.Columns.Add("Book ID", 100);
            lstBooks.Columns.Add("Category", 100);
            lstBooks.Columns.Add("Title", 100);
            lstBooks.Columns.Add("Press", 100);
            lstBooks.Columns.Add("Publish Year", 100);
            lstBooks.Columns.Add("Author", 100);
            lstBooks.Columns.Add("Price", 100);
            lstBooks.Columns.Add("Stock", 100);

            foreach (var book in books)
            {
                ListViewItem item = new ListViewItem(book.BookId.ToString());
                item.SubItems.Add(book.Category);
                item.SubItems.Add(book.Title);
                item.SubItems.Add(book.Press);
                item.SubItems.Add(book.PublishYear.ToString());
                item.SubItems.Add(book.Author);
                item.SubItems.Add(book.Price.ToString("F2"));
                item.SubItems.Add(book.Stock.ToString());
                lstBooks.Items.Add(item);
            }

            lstBooks.View = View.Details;
        }

        private void buttonBatchImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "CSV文件 (*.csv)|*.csv",
                Title = "选择要导入的书籍CSV文件"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var books = new List<Book>();
                    var lines = System.IO.File.ReadAllLines(openFileDialog.FileName);
                    foreach (var line in lines)
                    {
                        // 假设CSV格式: category,title,press,publish_year,author,price,stock
                        var parts = line.Split(',');
                        if (parts.Length != 7) continue;
                        if (!int.TryParse(parts[3], out int year)) continue;
                        if (!decimal.TryParse(parts[5], out decimal price)) continue;
                        if (!int.TryParse(parts[6], out int stock)) continue;

                        books.Add(new Book
                        {
                            Category = parts[0],
                            Title = parts[1],
                            Press = parts[2],
                            PublishYear = year,
                            Author = parts[4],
                            Price = price,
                            Stock = stock
                        });
                    }

                    if (books.Count > 0)
                    {
                        _bookRepository.AddBooks(books);
                        MessageBox.Show($"成功导入 {books.Count} 本书籍。");
                        RefreshBookList();
                    }
                    else
                    {
                        MessageBox.Show("没有有效的书籍数据被导入。");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("导入失败: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void RefreshBookList()
        {
            List<Book> books = _bookRepository.GetAllBooks();
            dataGridViewBooks.DataSource = null;
            dataGridViewBooks.DataSource = books;
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }
    }
}