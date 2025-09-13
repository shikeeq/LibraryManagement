using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms;
using System.Xml.Linq;

namespace LibraryManagementFrontend.Forms
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            btnAdminLogin = new Button();
            btnUserLogin = new Button();
            btnViewBooks = new Button();
            txtUsernameOrCardId = new TextBox();
            txtPassword = new TextBox();
            lblUsernameOrCardId = new Label();
            lblPassword = new Label();
            textBox1 = new TextBox();
            SuspendLayout();
            // 
            // btnAdminLogin
            // 
            btnAdminLogin.Location = new Point(20, 154);
            btnAdminLogin.Name = "btnAdminLogin";
            btnAdminLogin.Size = new Size(200, 50);
            btnAdminLogin.TabIndex = 0;
            btnAdminLogin.Text = "Admin Login";
            btnAdminLogin.UseVisualStyleBackColor = true;
            btnAdminLogin.Click += btnAdminLogin_Click;
            // 
            // btnUserLogin
            // 
            btnUserLogin.Location = new Point(240, 154);
            btnUserLogin.Name = "btnUserLogin";
            btnUserLogin.Size = new Size(200, 50);
            btnUserLogin.TabIndex = 1;
            btnUserLogin.Text = "User Login";
            btnUserLogin.UseVisualStyleBackColor = true;
            btnUserLogin.Click += btnUserLogin_Click;
            // 
            // btnViewBooks
            // 
            btnViewBooks.Location = new Point(460, 154);
            btnViewBooks.Name = "btnViewBooks";
            btnViewBooks.Size = new Size(200, 50);
            btnViewBooks.TabIndex = 2;
            btnViewBooks.Text = "View All Books";
            btnViewBooks.UseVisualStyleBackColor = true;
            btnViewBooks.Click += btnViewBooks_Click;
            // 
            // txtUsernameOrCardId
            // 
            txtUsernameOrCardId.Location = new Point(196, 54);
            txtUsernameOrCardId.Name = "txtUsernameOrCardId";
            txtUsernameOrCardId.Size = new Size(300, 31);
            txtUsernameOrCardId.TabIndex = 3;
            txtUsernameOrCardId.Visible = false;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(196, 104);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new Size(300, 31);
            txtPassword.TabIndex = 4;
            txtPassword.Visible = false;
            // 
            // lblUsernameOrCardId
            // 
            lblUsernameOrCardId.Location = new Point(20, 54);
            lblUsernameOrCardId.Name = "lblUsernameOrCardId";
            lblUsernameOrCardId.Size = new Size(158, 31);
            lblUsernameOrCardId.TabIndex = 5;
            lblUsernameOrCardId.Text = "Username/ID:";
            lblUsernameOrCardId.Visible = false;
            // 
            // lblPassword
            // 
            lblPassword.Location = new Point(20, 104);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(158, 31);
            lblPassword.TabIndex = 6;
            lblPassword.Text = "Password:";
            lblPassword.Visible = false;
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.Yellow;
            textBox1.ForeColor = Color.Teal;
            textBox1.Location = new Point(20, 8);
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(402, 31);
            textBox1.TabIndex = 7;
            textBox1.Text = "Welcome to use the library management system";
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(700, 262);
            Controls.Add(textBox1);
            Controls.Add(btnAdminLogin);
            Controls.Add(btnUserLogin);
            Controls.Add(btnViewBooks);
            Controls.Add(txtUsernameOrCardId);
            Controls.Add(txtPassword);
            Controls.Add(lblUsernameOrCardId);
            Controls.Add(lblPassword);
            Name = "LoginForm";
            Text = "Library Management System Login";
            ResumeLayout(false);
            PerformLayout();
        }

        private Button btnAdminLogin;
        private Button btnUserLogin;
        private Button btnViewBooks;
        private TextBox txtUsernameOrCardId;
        private TextBox txtPassword;
        private Label lblUsernameOrCardId;
        private Label lblPassword;
        private TextBox textBox1;
    }
}
