using LibraryManagementBackend;

namespace LibraryManagementFrontend.Forms
{
    partial class BookManagementForm
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
            label1 = new Label();
            dataGridViewBooks = new DataGridView();
            buttonAdd = new Button();
            buttonUpdate = new Button();
            buttonDelete = new Button();
            buttonBatchImport = new Button();
            lstBooks = new ListView();
            txtBookId = new TextBox();
            txtCategory = new TextBox();
            txtTitle = new TextBox();
            txtPress = new TextBox();
            txtPublishYear = new TextBox();
            txtAuthor = new TextBox();
            txtPrice = new TextBox();
            txtStock = new TextBox();
            lblBookId = new Label();
            lblCategory = new Label();
            lblTitle = new Label();
            lblPress = new Label();
            lblPublishYear = new Label();
            lblAuthor = new Label();
            lblPrice = new Label();
            lblStock = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridViewBooks).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft Sans Serif", 14F);
            label1.Location = new Point(20, 17);
            label1.Margin = new Padding(5, 0, 5, 0);
            label1.Name = "label1";
            label1.Size = new Size(252, 32);
            label1.TabIndex = 0;
            label1.Text = "Book Management";
            label1.Click += label1_Click;
            // 
            // dataGridViewBooks
            // 
            dataGridViewBooks.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewBooks.Location = new Point(27, 96);
            dataGridViewBooks.Margin = new Padding(5, 6, 5, 6);
            dataGridViewBooks.Name = "dataGridViewBooks";
            dataGridViewBooks.RowHeadersWidth = 62;
            dataGridViewBooks.Size = new Size(1000, 577);
            dataGridViewBooks.TabIndex = 1;
            // 
            // buttonAdd
            // 
            buttonAdd.Location = new Point(27, 712);
            buttonAdd.Margin = new Padding(5, 6, 5, 6);
            buttonAdd.Name = "buttonAdd";
            buttonAdd.Size = new Size(125, 44);
            buttonAdd.TabIndex = 2;
            buttonAdd.Text = "Add";
            buttonAdd.UseVisualStyleBackColor = true;
            buttonAdd.Click += btnAddBook_Click;
            // 
            // buttonUpdate
            // 
            buttonUpdate.Location = new Point(162, 712);
            buttonUpdate.Margin = new Padding(5, 6, 5, 6);
            buttonUpdate.Name = "buttonUpdate";
            buttonUpdate.Size = new Size(125, 44);
            buttonUpdate.TabIndex = 3;
            buttonUpdate.Text = "Update";
            buttonUpdate.UseVisualStyleBackColor = true;
            buttonUpdate.Click += btnUpdateBook_Click;
            // 
            // buttonDelete
            // 
            buttonDelete.Location = new Point(297, 712);
            buttonDelete.Margin = new Padding(5, 6, 5, 6);
            buttonDelete.Name = "buttonDelete";
            buttonDelete.Size = new Size(125, 44);
            buttonDelete.TabIndex = 4;
            buttonDelete.Text = "Delete";
            buttonDelete.UseVisualStyleBackColor = true;
            buttonDelete.Click += btnDeleteBook_Click;
            // 
            // buttonBatchImport
            // 
            buttonBatchImport.Location = new Point(432, 712);
            buttonBatchImport.Margin = new Padding(5, 6, 5, 6);
            buttonBatchImport.Name = "buttonBatchImport";
            buttonBatchImport.Size = new Size(150, 44);
            buttonBatchImport.TabIndex = 5;
            buttonBatchImport.Text = "批量导入";
            buttonBatchImport.UseVisualStyleBackColor = true;
            buttonBatchImport.Click += buttonBatchImport_Click;
            // 
            // lstBooks
            // 
            lstBooks.Location = new Point(27, 800);
            lstBooks.Name = "lstBooks";
            lstBooks.Size = new Size(1000, 200);
            lstBooks.TabIndex = 6;
            lstBooks.UseCompatibleStateImageBehavior = false;
            lstBooks.View = View.Details;
            // 
            // txtBookId
            // 
            txtBookId.Location = new Point(39, 812);
            txtBookId.Name = "txtBookId";
            txtBookId.Size = new Size(100, 31);
            txtBookId.TabIndex = 7;
            // 
            // txtCategory
            // 
            txtCategory.Location = new Point(172, 812);
            txtCategory.Name = "txtCategory";
            txtCategory.Size = new Size(100, 31);
            txtCategory.TabIndex = 8;
            // 
            // txtTitle
            // 
            txtTitle.Location = new Point(311, 812);
            txtTitle.Name = "txtTitle";
            txtTitle.Size = new Size(100, 31);
            txtTitle.TabIndex = 9;
            // 
            // txtPress
            // 
            txtPress.Location = new Point(448, 812);
            txtPress.Name = "txtPress";
            txtPress.Size = new Size(100, 31);
            txtPress.TabIndex = 10;
            // 
            // txtPublishYear
            // 
            txtPublishYear.Location = new Point(593, 812);
            txtPublishYear.Name = "txtPublishYear";
            txtPublishYear.Size = new Size(100, 31);
            txtPublishYear.TabIndex = 11;
            // 
            // txtAuthor
            // 
            txtAuthor.Location = new Point(725, 812);
            txtAuthor.Name = "txtAuthor";
            txtAuthor.Size = new Size(100, 31);
            txtAuthor.TabIndex = 12;
            // 
            // txtPrice
            // 
            txtPrice.Location = new Point(842, 812);
            txtPrice.Name = "txtPrice";
            txtPrice.Size = new Size(100, 31);
            txtPrice.TabIndex = 13;
            // 
            // txtStock
            // 
            txtStock.Location = new Point(955, 812);
            txtStock.Name = "txtStock";
            txtStock.Size = new Size(100, 31);
            txtStock.TabIndex = 14;
            // 
            // lblBookId
            // 
            lblBookId.AutoSize = true;
            lblBookId.Location = new Point(51, 784);
            lblBookId.Name = "lblBookId";
            lblBookId.Size = new Size(76, 25);
            lblBookId.TabIndex = 15;
            lblBookId.Text = "Book ID";
            // 
            // lblCategory
            // 
            lblCategory.AutoSize = true;
            lblCategory.Location = new Point(179, 784);
            lblCategory.Name = "lblCategory";
            lblCategory.Size = new Size(84, 25);
            lblCategory.TabIndex = 16;
            lblCategory.Text = "Category";
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(339, 784);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(44, 25);
            lblTitle.TabIndex = 17;
            lblTitle.Text = "Title";
            // 
            // lblPress
            // 
            lblPress.AutoSize = true;
            lblPress.Location = new Point(470, 784);
            lblPress.Name = "lblPress";
            lblPress.Size = new Size(53, 25);
            lblPress.TabIndex = 18;
            lblPress.Text = "Press";
            // 
            // lblPublishYear
            // 
            lblPublishYear.AutoSize = true;
            lblPublishYear.Location = new Point(587, 784);
            lblPublishYear.Name = "lblPublishYear";
            lblPublishYear.Size = new Size(106, 25);
            lblPublishYear.TabIndex = 19;
            lblPublishYear.Text = "Publish Year";
            // 
            // lblAuthor
            // 
            lblAuthor.AutoSize = true;
            lblAuthor.Location = new Point(741, 784);
            lblAuthor.Name = "lblAuthor";
            lblAuthor.Size = new Size(67, 25);
            lblAuthor.TabIndex = 20;
            lblAuthor.Text = "Author";
            // 
            // lblPrice
            // 
            lblPrice.AutoSize = true;
            lblPrice.Location = new Point(865, 784);
            lblPrice.Name = "lblPrice";
            lblPrice.Size = new Size(49, 25);
            lblPrice.TabIndex = 21;
            lblPrice.Text = "Price";
            // 
            // lblStock
            // 
            lblStock.AutoSize = true;
            lblStock.Location = new Point(972, 784);
            lblStock.Name = "lblStock";
            lblStock.Size = new Size(55, 25);
            lblStock.TabIndex = 22;
            lblStock.Text = "Stock";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            label2.ForeColor = Color.Crimson;
            label2.Location = new Point(39, 892);
            label2.Name = "label2";
            label2.Size = new Size(702, 30);
            label2.TabIndex = 23;
            label2.Text = "For Add, all fields except Book ID need to be filled, the content in Book ID will be ignored.";
            label2.Click += label2_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            label3.ForeColor = Color.Crimson;
            label3.Location = new Point(39, 943);
            label3.Name = "label3";
            label3.Size = new Size(900, 30);
            label3.TabIndex = 24;
            label3.Text = "For Update, Book ID is required and must already exist, other fields: fill to update, leave empty to keep original.";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            label4.ForeColor = Color.Crimson;
            label4.Location = new Point(39, 996);
            label4.Name = "label4";
            label4.Size = new Size(668, 30);
            label4.TabIndex = 25;
            label4.Text = "For Delete, only Book ID needs to be filled, all other fields will be ignored.";
            // 
            // BookManagementForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1057, 1210);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(buttonBatchImport);
            Controls.Add(buttonDelete);
            Controls.Add(buttonUpdate);
            Controls.Add(buttonAdd);
            Controls.Add(dataGridViewBooks);
            Controls.Add(label1);
            Controls.Add(txtBookId);
            Controls.Add(txtCategory);
            Controls.Add(txtTitle);
            Controls.Add(txtPress);
            Controls.Add(txtPublishYear);
            Controls.Add(txtAuthor);
            Controls.Add(txtPrice);
            Controls.Add(txtStock);
            Controls.Add(lblBookId);
            Controls.Add(lblCategory);
            Controls.Add(lblTitle);
            Controls.Add(lblPress);
            Controls.Add(lblPublishYear);
            Controls.Add(lblAuthor);
            Controls.Add(lblPrice);
            Controls.Add(lblStock);
            Margin = new Padding(5, 6, 5, 6);
            Name = "BookManagementForm";
            Text = "Book Management";
          /*   Load += BookManagementForm_Load_1; */
            ((System.ComponentModel.ISupportInitialize)dataGridViewBooks).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridViewBooks;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonBatchImport;
        private System.Windows.Forms.ListView lstBooks;
        private System.Windows.Forms.TextBox txtBookId;
        private System.Windows.Forms.TextBox txtCategory;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.TextBox txtPress;
        private System.Windows.Forms.TextBox txtPublishYear;
        private System.Windows.Forms.TextBox txtAuthor;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.TextBox txtStock;
        private System.Windows.Forms.Label lblBookId;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblPress;
        private System.Windows.Forms.Label lblPublishYear;
        private System.Windows.Forms.Label lblAuthor;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.Label lblStock;
        private Label label2;
        private Label label3;
        private Label label4;
    }
}