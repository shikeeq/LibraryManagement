namespace LibraryManagementFrontend.Forms
{
    partial class UserForm
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
            btnViewBooks = new Button();
            btnBorrowManagement = new Button();
            txtWelcomeMessage = new TextBox();
            SuspendLayout();
            // 
            // btnViewBooks
            // 
            btnViewBooks.Location = new Point(20, 23);
            btnViewBooks.Name = "btnViewBooks";
            btnViewBooks.Size = new Size(433, 77);
            btnViewBooks.TabIndex = 0;
            btnViewBooks.Text = "View All Books";
            btnViewBooks.UseVisualStyleBackColor = true;
            btnViewBooks.Click += btnViewBooks_Click;
            // 
            // btnBorrowManagement
            // 
            btnBorrowManagement.Location = new Point(20, 112);
            btnBorrowManagement.Name = "btnBorrowManagement";
            btnBorrowManagement.Size = new Size(433, 77);
            btnBorrowManagement.TabIndex = 1;
            btnBorrowManagement.Text = "Borrow Management";
            btnBorrowManagement.UseVisualStyleBackColor = true;
            btnBorrowManagement.Click += btnBorrowManagement_Click;
            // 
            // txtWelcomeMessage
            // 
            txtWelcomeMessage.BackColor = Color.Chartreuse;
            txtWelcomeMessage.Location = new Point(280, 195);
            txtWelcomeMessage.Name = "txtWelcomeMessage";
            txtWelcomeMessage.ReadOnly = true;
            txtWelcomeMessage.Size = new Size(173, 31);
            txtWelcomeMessage.TabIndex = 2;
            txtWelcomeMessage.Text = "Hello, user <cardID>";
            // 
            // UserForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(473, 233);
            Controls.Add(txtWelcomeMessage);
            Controls.Add(btnBorrowManagement);
            Controls.Add(btnViewBooks);
            Name = "UserForm";
            Text = "User Dashboard";
            FormClosed += UserForm_FormClosed;
            ResumeLayout(false);
            PerformLayout();
        }

        private Button btnViewBooks;
        private Button btnBorrowManagement;
        private TextBox txtWelcomeMessage;
    }
}
