using LibraryManagementBackend;

namespace LibraryManagementFrontend.Forms
{
    partial class BorrowManagementForm
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            label1 = new System.Windows.Forms.Label();
            borrowButton = new System.Windows.Forms.Button();
            returnButton = new System.Windows.Forms.Button();
            borrowListView = new System.Windows.Forms.ListView();
            cardIdColumn = new System.Windows.Forms.ColumnHeader();
            bookIdColumn = new System.Windows.Forms.ColumnHeader();
            borrowTimeColumn = new System.Windows.Forms.ColumnHeader();
            returnTimeColumn = new System.Windows.Forms.ColumnHeader();
            txtCardId = new System.Windows.Forms.TextBox();
            txtBookId = new System.Windows.Forms.TextBox();
            labelCardId = new System.Windows.Forms.Label();
            labelBookId = new System.Windows.Forms.Label();
            dataGridViewBooks = new System.Windows.Forms.DataGridView();
            dataGridViewCards = new System.Windows.Forms.DataGridView();
            allBooks = new System.Windows.Forms.DataGridView();
            columnHeader1 = new System.Windows.Forms.ColumnHeader();
            columnHeader2 = new System.Windows.Forms.ColumnHeader();
            columnHeader3 = new System.Windows.Forms.ColumnHeader();
            columnHeader4 = new System.Windows.Forms.ColumnHeader();
            label2 = new System.Windows.Forms.Label();
            txtSearchTitle = new System.Windows.Forms.TextBox();
            txtSearchAuthor = new System.Windows.Forms.TextBox();
            btnSearchTitle = new System.Windows.Forms.Button();
            btnSearchAuthor = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)dataGridViewBooks).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewCards).BeginInit();
            ((System.ComponentModel.ISupportInitialize)allBooks).BeginInit();
            SuspendLayout();

            // label1
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(20, 17);
            label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(179, 25);
            label1.TabIndex = 0;
            label1.Text = "Borrow Management";

            // txtSearchTitle
            txtSearchTitle.Location = new System.Drawing.Point(400, 17);
            txtSearchTitle.Name = "txtSearchTitle";
            txtSearchTitle.Size = new System.Drawing.Size(150, 31);
            txtSearchTitle.TabIndex = 20;
            txtSearchTitle.PlaceholderText = "输入书名";

            // btnSearchTitle
            btnSearchTitle.Location = new System.Drawing.Point(560, 17);
            btnSearchTitle.Name = "btnSearchTitle";
            btnSearchTitle.Size = new System.Drawing.Size(80, 31);
            btnSearchTitle.TabIndex = 21;
            btnSearchTitle.Text = "查书名";
            btnSearchTitle.UseVisualStyleBackColor = true;
            btnSearchTitle.Click += BtnSearchTitle_Click;

            // txtSearchAuthor
            txtSearchAuthor.Location = new System.Drawing.Point(660, 17);
            txtSearchAuthor.Name = "txtSearchAuthor";
            txtSearchAuthor.Size = new System.Drawing.Size(150, 31);
            txtSearchAuthor.TabIndex = 22;
            txtSearchAuthor.PlaceholderText = "输入作者";

            // btnSearchAuthor
            btnSearchAuthor.Location = new System.Drawing.Point(820, 17);
            btnSearchAuthor.Name = "btnSearchAuthor";
            btnSearchAuthor.Size = new System.Drawing.Size(80, 31);
            btnSearchAuthor.TabIndex = 23;
            btnSearchAuthor.Text = "查作者";
            btnSearchAuthor.UseVisualStyleBackColor = true;
            btnSearchAuthor.Click += BtnSearchAuthor_Click;

            // borrowButton
            borrowButton.Location = new System.Drawing.Point(25, 433);
            borrowButton.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            borrowButton.Name = "borrowButton";
            borrowButton.Size = new System.Drawing.Size(125, 44);
            borrowButton.TabIndex = 1;
            borrowButton.Text = "Borrow Book";
            borrowButton.UseVisualStyleBackColor = true;
            borrowButton.Click += btnBorrowBook_Click;

            // returnButton
            returnButton.Location = new System.Drawing.Point(167, 433);
            returnButton.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            returnButton.Name = "returnButton";
            returnButton.Size = new System.Drawing.Size(125, 44);
            returnButton.TabIndex = 2;
            returnButton.Text = "Return Book";
            returnButton.UseVisualStyleBackColor = true;
            returnButton.Click += btnReturnBook_Click;

            // borrowListView
            borrowListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { cardIdColumn, bookIdColumn, borrowTimeColumn, returnTimeColumn });
            borrowListView.FullRowSelect = true;
            borrowListView.GridLines = true;
            borrowListView.Location = new System.Drawing.Point(25, 48);
            borrowListView.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            borrowListView.Name = "borrowListView";
            borrowListView.Size = new System.Drawing.Size(1221, 342);
            borrowListView.TabIndex = 4;
            borrowListView.UseCompatibleStateImageBehavior = false;
            borrowListView.View = System.Windows.Forms.View.Details;
            borrowListView.SelectedIndexChanged += borrowListView_SelectedIndexChanged;

            // cardIdColumn
            cardIdColumn.Text = "Card ID";
            cardIdColumn.Width = 200;

            // bookIdColumn
            bookIdColumn.Text = "Book ID";
            bookIdColumn.Width = 200;

            // borrowTimeColumn
            borrowTimeColumn.Text = "Borrow Time";
            borrowTimeColumn.Width = 200;

            // returnTimeColumn
            returnTimeColumn.Text = "Return Time";
            returnTimeColumn.Width = 200;

            // txtCardId
            txtCardId.Location = new System.Drawing.Point(108, 502);
            txtCardId.Name = "txtCardId";
            txtCardId.Size = new System.Drawing.Size(100, 31);
            txtCardId.TabIndex = 5;

            // txtBookId
            txtBookId.Location = new System.Drawing.Point(316, 502);
            txtBookId.Name = "txtBookId";
            txtBookId.Size = new System.Drawing.Size(100, 31);
            txtBookId.TabIndex = 6;

            // labelCardId
            labelCardId.AutoSize = true;
            labelCardId.Location = new System.Drawing.Point(26, 504);
            labelCardId.Name = "labelCardId";
            labelCardId.Size = new System.Drawing.Size(84, 25);
            labelCardId.TabIndex = 7;
            labelCardId.Text = "Card ID*:";

            // labelBookId
            labelBookId.AutoSize = true;
            labelBookId.Location = new System.Drawing.Point(230, 504);
            labelBookId.Name = "labelBookId";
            labelBookId.Size = new System.Drawing.Size(88, 25);
            labelBookId.TabIndex = 8;
            labelBookId.Text = "Book ID*:";

            // dataGridViewBooks
            dataGridViewBooks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewBooks.Location = new System.Drawing.Point(25, 450);
            dataGridViewBooks.Name = "dataGridViewBooks";
            dataGridViewBooks.RowHeadersWidth = 62;
            dataGridViewBooks.Size = new System.Drawing.Size(664, 200);
            dataGridViewBooks.TabIndex = 11;

            // dataGridViewCards
            dataGridViewCards.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCards.Location = new System.Drawing.Point(25, 550);
            dataGridViewCards.Name = "dataGridViewCards";
            dataGridViewCards.RowHeadersWidth = 62;
            dataGridViewCards.Size = new System.Drawing.Size(664, 200);
            dataGridViewCards.TabIndex = 12;

            // allBooks
            allBooks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            allBooks.Location = new System.Drawing.Point(25, 607);
            allBooks.Name = "allBooks";
            allBooks.RowHeadersWidth = 62;
            allBooks.Size = new System.Drawing.Size(1221, 365);
            allBooks.TabIndex = 13;

            // columnHeader1
            columnHeader1.Text = "Card ID";
            columnHeader1.Width = 100;

            // columnHeader2
            columnHeader2.Text = "Book ID";
            columnHeader2.Width = 100;

            // columnHeader3
            columnHeader3.Text = "Borrow Time";
            columnHeader3.Width = 100;

            // columnHeader4
            columnHeader4.Text = "Return Time";
            columnHeader4.Width = 100;

            // label2
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(29, 567);
            label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(86, 25);
            label2.TabIndex = 10;
            label2.Text = "All Books";

            // BorrowManagementForm
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1258, 1000);
            Controls.Add(txtSearchTitle);
            Controls.Add(btnSearchTitle);
            Controls.Add(txtSearchAuthor);
            Controls.Add(btnSearchAuthor);
            Controls.Add(label2);
            Controls.Add(allBooks);
            Controls.Add(txtBookId);
            Controls.Add(labelBookId);
            Controls.Add(txtCardId);
            Controls.Add(labelCardId);
            Controls.Add(borrowListView);
            Controls.Add(returnButton);
            Controls.Add(borrowButton);
            Controls.Add(label1);
            Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            Name = "BorrowManagementForm";
            Text = "Borrow Management";
            Load += BorrowManagementForm_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewBooks).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewCards).EndInit();
            ((System.ComponentModel.ISupportInitialize)allBooks).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button borrowButton;
        private System.Windows.Forms.Button returnButton;
        private System.Windows.Forms.ListView borrowListView;
        private System.Windows.Forms.ColumnHeader cardIdColumn;
        private System.Windows.Forms.ColumnHeader bookIdColumn;
        private System.Windows.Forms.ColumnHeader borrowTimeColumn;
        private System.Windows.Forms.ColumnHeader returnTimeColumn;
        private System.Windows.Forms.TextBox txtCardId;
        private System.Windows.Forms.TextBox txtBookId;
        private System.Windows.Forms.Label labelCardId;
        private System.Windows.Forms.Label labelBookId;
        private System.Windows.Forms.DataGridView dataGridViewBooks;
        private System.Windows.Forms.DataGridView dataGridViewCards;
        private System.Windows.Forms.DataGridView allBooks;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSearchTitle;
        private System.Windows.Forms.TextBox txtSearchAuthor;
        private System.Windows.Forms.Button btnSearchTitle;
        private System.Windows.Forms.Button btnSearchAuthor;
    }
}