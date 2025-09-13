using System;
using System.Windows.Forms;
using LibraryManagementBackend;

namespace LibraryManagementFrontend.Forms
{
    public partial class MainForm : Form
    {
        private readonly BookRepository _bookRepository;
        private readonly CardRepository _cardRepository;
        private readonly BorrowRepository _borrowRepository;
        private Form loginForm;

        public MainForm(Form loginForm)
        {
            InitializeComponent();
            string connectionString = "Driver={ODBC Driver 17 for SQL Server};Server=LAPTOP-476JT8H0\\MSSQLSERVER_11;Database=db00;Trusted_Connection=Yes;";
            _bookRepository = new BookRepository(connectionString);
            _cardRepository = new CardRepository(connectionString);
            _borrowRepository = new BorrowRepository(connectionString);
            this.loginForm = loginForm;
        }

        private void btnManageBooks_Click(object sender, EventArgs e)
        {
            BookManagementForm bookForm = new BookManagementForm(_bookRepository);
            bookForm.ShowDialog();
        }

        private void btnManageCards_Click(object sender, EventArgs e)
        {
            CardManagementForm cardForm = new CardManagementForm(_cardRepository);
            cardForm.ShowDialog();
        }

        private void btnManageBorrows_Click(object sender, EventArgs e)
        {
            BorrowManagementForm borrowForm = new BorrowManagementForm(_borrowRepository, _bookRepository, _cardRepository);
            borrowForm.ShowDialog();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Initialization code can go here
        }

        private void MainForm_Load_1(object sender, EventArgs e)
        {

        }

        private void btnResetDatabase_Click(object sender, EventArgs e)
        {
            // Display confirmation dialog asking if the user is sure they want to reset the database
            DialogResult result = MessageBox.Show("Are you sure you want to reset the database?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            // If the user clicks "Yes", reset the database
            if (result == DialogResult.Yes)
            {
                // Reset the database to its initial state
                _bookRepository.ResetDatabase();
                _cardRepository.ResetDatabase();
                _borrowRepository.ResetDatabase();
                MessageBox.Show("Database reset successful.");
            }
            // If the user clicks "No", do nothing
            else
            {
                // Do nothing
                MessageBox.Show("Database reset cancelled.");
            }
        }
        
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // When the MainForm is closed, clear the contents of all TextBox controls in the LoginForm
            foreach (Control control in loginForm.Controls)
            {
                if (control is TextBox textBox && textBox.Name != "textBox1" && textBox.Name != "textBox2")
                {
                    textBox.Clear();
                }
            }

            // When the MainForm is closed, re-show the LoginForm
            loginForm.Show();
        }
    }
}
