using System;
using System.Collections.Generic;
using System.Windows.Forms;
using LibraryManagementBackend;

namespace LibraryManagementFrontend.Forms
{
    public partial class ViewBooksForm : Form
    {
        private readonly BookRepository _bookRepository;
        private Form loginForm;

        public ViewBooksForm(Form loginForm)
        {
            InitializeComponent();
            string connectionString = "Driver={ODBC Driver 17 for SQL Server};Server=LAPTOP-476JT8H0\\MSSQLSERVER_11;Database=db00;Trusted_Connection=Yes;";
            _bookRepository = new BookRepository(connectionString);
            this.loginForm = loginForm;
        }

        private void ViewBooksForm_Load(object sender, EventArgs e)
        {
            LoadBooks();
        }

        private void LoadBooks()
        {
            try
            {
                List<Book> books = _bookRepository.GetAllBooks();
                dataGridViewBooks.DataSource = books;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading books: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ViewBooksForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            loginForm.Show();
        }
    }
}