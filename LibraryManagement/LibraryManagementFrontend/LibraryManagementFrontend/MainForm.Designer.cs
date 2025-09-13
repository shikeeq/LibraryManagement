namespace LibraryManagementFrontend.Forms
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnManageBooks = new Button();
            btnManageCards = new Button();
            btnManageBorrows = new Button();
            btnResetDatabase = new Button();
            textBox1 = new TextBox();
            SuspendLayout();
            // 
            // btnManageBooks
            // 
            btnManageBooks.Location = new Point(20, 23);
            btnManageBooks.Margin = new Padding(5, 6, 5, 6);
            btnManageBooks.Name = "btnManageBooks";
            btnManageBooks.Size = new Size(433, 77);
            btnManageBooks.TabIndex = 0;
            btnManageBooks.Text = "Manage Books";
            btnManageBooks.UseVisualStyleBackColor = true;
            btnManageBooks.Click += btnManageBooks_Click;
            // 
            // btnManageCards
            // 
            btnManageCards.Location = new Point(20, 112);
            btnManageCards.Margin = new Padding(5, 6, 5, 6);
            btnManageCards.Name = "btnManageCards";
            btnManageCards.Size = new Size(433, 77);
            btnManageCards.TabIndex = 1;
            btnManageCards.Text = "Manage Cards";
            btnManageCards.UseVisualStyleBackColor = true;
            btnManageCards.Click += btnManageCards_Click;
            // 
            // btnManageBorrows
            // 
            btnManageBorrows.Location = new Point(20, 200);
            btnManageBorrows.Margin = new Padding(5, 6, 5, 6);
            btnManageBorrows.Name = "btnManageBorrows";
            btnManageBorrows.Size = new Size(433, 77);
            btnManageBorrows.TabIndex = 2;
            btnManageBorrows.Text = "Manage Borrows";
            btnManageBorrows.UseVisualStyleBackColor = true;
            btnManageBorrows.Click += btnManageBorrows_Click;
            // 
            // btnResetDatabase
            // 
            btnResetDatabase.BackColor = Color.Red;
            btnResetDatabase.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnResetDatabase.ForeColor = SystemColors.ControlLightLight;
            btnResetDatabase.Location = new Point(20, 289);
            btnResetDatabase.Margin = new Padding(5, 6, 5, 6);
            btnResetDatabase.Name = "btnResetDatabase";
            btnResetDatabase.Size = new Size(433, 77);
            btnResetDatabase.TabIndex = 3;
            btnResetDatabase.Text = "Reset Database";
            btnResetDatabase.UseVisualStyleBackColor = false;
            btnResetDatabase.Click += btnResetDatabase_Click;
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.Yellow;
            textBox1.Location = new Point(303, 384);
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(150, 31);
            textBox1.TabIndex = 4;
            textBox1.Text = "Hello, admin";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(473, 430);
            Controls.Add(textBox1);
            Controls.Add(btnResetDatabase);
            Controls.Add(btnManageBorrows);
            Controls.Add(btnManageCards);
            Controls.Add(btnManageBooks);
            Margin = new Padding(5, 6, 5, 6);
            Name = "MainForm";
            Text = "Library Management System";
            FormClosed += MainForm_FormClosed;
            Load += MainForm_Load_1;
            Click += btnResetDatabase_Click;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button btnManageBooks;
        private System.Windows.Forms.Button btnManageCards;
        private System.Windows.Forms.Button btnManageBorrows;
        private Button btnResetDatabase;
        private TextBox textBox1;
    }
}