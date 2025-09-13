using LibraryManagementBackend;
using System;
using System.Windows.Forms;

namespace LibraryManagementFrontend.Forms
{
    public partial class LoginForm : Form
    {
        private const string AdminPassword = "admin"; // Admin password (can be read from configuration file)
        private string _currentLoginMode = string.Empty;

        public LoginForm()
        {
            InitializeComponent();
            _currentLoginMode = "User"; // UserLogin is the default mode
            lblUsernameOrCardId.Text = "Card ID:";
            lblPassword.Visible = true;
            lblUsernameOrCardId.Visible = true;
            txtUsernameOrCardId.Visible = true;
            txtPassword.Visible = true;
            txtUsernameOrCardId.PasswordChar = '\0';
        }

        private void btnAdminLogin_Click(object sender, EventArgs e)
        {
            // Check if switching to AdminLogin
            if (_currentLoginMode != "Admin")
            {
                // Clear input fields when switching modes
                txtUsernameOrCardId.Clear();
                txtPassword.Clear();
                _currentLoginMode = "Admin"; // Update the current login mode
            }

            // Display input fields for AdminLogin
            lblUsernameOrCardId.Text = "Admin Password:";
            lblUsernameOrCardId.Visible = true;
            lblPassword.Visible = false;
            txtUsernameOrCardId.Visible = true;
            txtPassword.Visible = false;
            txtUsernameOrCardId.PasswordChar = '*';

            // If the password is empty, ignore the event
            if (string.IsNullOrEmpty(txtUsernameOrCardId.Text))
            {
                return; // Directly return without further logic
            }

            // Validate admin password
            if (txtUsernameOrCardId.Text == AdminPassword)
            {
                MessageBox.Show("Admin login successful!");
                var mainForm = new MainForm(this);
                mainForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid admin password.");
            }
        }

        private void btnUserLogin_Click(object sender, EventArgs e)
        {
            // Check if switching to UserLogin
            if (_currentLoginMode != "User")
            {
                // Clear input fields when switching modes
                txtUsernameOrCardId.Clear();
                txtPassword.Clear();
                _currentLoginMode = "User"; // Update the current login mode
            }

            // Display input fields for UserLogin
            lblUsernameOrCardId.Text = "Card ID:";
            lblPassword.Visible = true;
            lblUsernameOrCardId.Visible = true;
            txtUsernameOrCardId.Visible = true;
            txtPassword.Visible = true;
            txtUsernameOrCardId.PasswordChar = '\0';

            // If the password is empty, ignore the event
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                return; // Directly return without further logic
            }

            // Validate user login
            if (int.TryParse(txtUsernameOrCardId.Text, out int cardId))
            {
                var cardRepository = new CardRepository("Driver={ODBC Driver 17 for SQL Server};Server=LAPTOP-476JT8H0\\MSSQLSERVER_11;Database=db00;Trusted_Connection=Yes;");
                var card = cardRepository.GetCardById(cardId);

                if (card != null && txtPassword.Text == "password") // Assume all user passwords a"
                {
                    MessageBox.Show("User login successful!");
                    var userForm = new UserForm(card, this); // Navigate to UserForm
                    userForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid card ID or password.");
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid card ID.");
            }
        }

        private void btnViewBooks_Click(object sender, EventArgs e)
        {
            // Go to the page to view all books
            var viewBooksForm = new ViewBooksForm(this);
            viewBooksForm.Show();
            this.Hide();
        }
    }
}
