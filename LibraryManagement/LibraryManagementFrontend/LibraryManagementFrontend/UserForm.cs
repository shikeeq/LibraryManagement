using System;
using System.Windows.Forms;
using LibraryManagementBackend;

namespace LibraryManagementFrontend.Forms
{
    public partial class UserForm : Form
    {
        private readonly Card _card;
        private readonly BookRepository _bookRepository;
        private readonly BorrowRepository _borrowRepository;
        private readonly CardRepository _cardRepository;
        private readonly Form _loginForm;

        public UserForm(Card card, Form loginForm)
        {
            InitializeComponent();
            _card = card ?? throw new ArgumentNullException(nameof(card));
            _loginForm = loginForm;
            txtWelcomeMessage.Text = $"Hello, user {_card.CardId}";

            string connectionString = "Driver={ODBC Driver 17 for SQL Server};Server=LAPTOP-476JT8H0\\MSSQLSERVER_11;Database=db00;Trusted_Connection=Yes;";
            _bookRepository = new BookRepository(connectionString);
            _borrowRepository = new BorrowRepository(connectionString);
            _cardRepository = new CardRepository(connectionString);
        }

        private void btnViewBooks_Click(object sender, EventArgs e)
        {
            // Open ViewBooksForm
            var viewBooksForm = new ViewBooksForm(this);
            viewBooksForm.Show();
            this.Hide();
        }

        private void btnBorrowManagement_Click(object sender, EventArgs e)
        {
            // Open BorrowManagementForm
            var borrowManagementForm = new BorrowManagementForm(_borrowRepository, _bookRepository, _cardRepository);

            // Use ShowDialog to open the form, ensuring subsequent logic is executed after the form is displayed
            borrowManagementForm.Shown += (s, args) =>
            {
                // Use reflection to access controls of BorrowManagementForm
                var txtCardId = borrowManagementForm.Controls.Find("txtCardId", true).FirstOrDefault() as TextBox;
                if (txtCardId != null)
                {
                    txtCardId.Text = _card.CardId.ToString(); // Set to the CardID of the logged-in user
                    txtCardId.ReadOnly = true; // Set as read-only
                }
            };

            borrowManagementForm.ShowDialog();
        }

        private void UserForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // When UserForm is closed, clear the content of all TextBox controls in the LoginForm
            foreach (Control control in _loginForm.Controls)
            {
                if (control is TextBox textBox && textBox.Name != "textBox1" && textBox.Name != "textBox2")
                {
                    textBox.Clear();
                }
            }

            // When UserForm is closed, re-display the LoginForm
            _loginForm.Show();
        }
    }
}
