using System.Windows.Forms;

namespace LibraryManagementFrontend.Forms
{
    partial class CardManagementForm
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
            btnAddCard = new Button();
            btnUpdateCard = new Button();
            btnDeleteCard = new Button();
            dataGridViewCards = new DataGridView();
            txtCardName = new TextBox();
            txtCardId = new TextBox();
            txtName = new TextBox();
            txtDepartment = new TextBox();
            cmbType = new ComboBox();
            lblCardId = new Label();
            lblCardName = new Label();
            lblName = new Label();
            lblDepartment = new Label();
            lblType = new Label();
            lstCards = new ListView();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridViewCards).BeginInit();
            SuspendLayout();
            // 
            // btnAddCard
            // 
            btnAddCard.Location = new Point(20, 135);
            btnAddCard.Margin = new Padding(5, 6, 5, 6);
            btnAddCard.Name = "btnAddCard";
            btnAddCard.Size = new Size(125, 44);
            btnAddCard.TabIndex = 0;
            btnAddCard.Text = "Add Card";
            btnAddCard.UseVisualStyleBackColor = true;
            btnAddCard.Click += btnAddCard_Click;
            // 
            // btnUpdateCard
            // 
            btnUpdateCard.Location = new Point(155, 135);
            btnUpdateCard.Margin = new Padding(5, 6, 5, 6);
            btnUpdateCard.Name = "btnUpdateCard";
            btnUpdateCard.Size = new Size(125, 44);
            btnUpdateCard.TabIndex = 1;
            btnUpdateCard.Text = "Update Card";
            btnUpdateCard.UseVisualStyleBackColor = true;
            btnUpdateCard.Click += btnUpdateCard_Click;
            // 
            // btnDeleteCard
            // 
            btnDeleteCard.Location = new Point(290, 135);
            btnDeleteCard.Margin = new Padding(5, 6, 5, 6);
            btnDeleteCard.Name = "btnDeleteCard";
            btnDeleteCard.Size = new Size(125, 44);
            btnDeleteCard.TabIndex = 2;
            btnDeleteCard.Text = "Delete Card";
            btnDeleteCard.UseVisualStyleBackColor = true;
            btnDeleteCard.Click += btnDeleteCard_Click;
            // 
            // dataGridViewCards
            // 
            dataGridViewCards.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCards.Location = new Point(20, 190);
            dataGridViewCards.Margin = new Padding(5, 6, 5, 6);
            dataGridViewCards.Name = "dataGridViewCards";
            dataGridViewCards.RowHeadersWidth = 62;
            dataGridViewCards.Size = new Size(1293, 652);
            dataGridViewCards.TabIndex = 3;
            // 
            // txtCardName
            // 
            txtCardName.Location = new Point(197, 56);
            txtCardName.Margin = new Padding(5, 6, 5, 6);
            txtCardName.Name = "txtCardName";
            txtCardName.Size = new Size(164, 31);
            txtCardName.TabIndex = 5;
            // 
            // txtCardId
            // 
            txtCardId.Location = new Point(20, 56);
            txtCardId.Margin = new Padding(5, 6, 5, 6);
            txtCardId.Name = "txtCardId";
            txtCardId.Size = new Size(164, 31);
            txtCardId.TabIndex = 4;
            // 
            // txtName
            // 
            txtName.Location = new Point(118, 29);
            txtName.Name = "txtName";
            txtName.Size = new Size(100, 31);
            txtName.TabIndex = 5;
            // 
            // txtDepartment
            // 
            txtDepartment.Location = new Point(374, 56);
            txtDepartment.Name = "txtDepartment";
            txtDepartment.Size = new Size(164, 31);
            txtDepartment.TabIndex = 6;
            // 
            // cmbType
            // 
            cmbType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbType.Items.AddRange(new object[] { "", "S", "T" });
            cmbType.Location = new Point(551, 56);
            cmbType.Name = "cmbType";
            cmbType.Size = new Size(164, 33);
            cmbType.TabIndex = 7;
            // 
            // lblCardId
            // 
            lblCardId.AutoSize = true;
            lblCardId.Location = new Point(20, 25);
            lblCardId.Margin = new Padding(5, 0, 5, 0);
            lblCardId.Name = "lblCardId";
            lblCardId.Size = new Size(76, 25);
            lblCardId.TabIndex = 6;
            lblCardId.Text = "Card ID:";
            // 
            // lblCardName
            // 
            lblCardName.AutoSize = true;
            lblCardName.Location = new Point(192, 25);
            lblCardName.Margin = new Padding(5, 0, 5, 0);
            lblCardName.Name = "lblCardName";
            lblCardName.Size = new Size(105, 25);
            lblCardName.TabIndex = 7;
            lblCardName.Text = "Card Name:";
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(115, 13);
            lblName.Name = "lblName";
            lblName.Size = new Size(38, 13);
            lblName.TabIndex = 9;
            lblName.Text = "Name:";
            // 
            // lblDepartment
            // 
            lblDepartment.AutoSize = true;
            lblDepartment.Location = new Point(374, 25);
            lblDepartment.Name = "lblDepartment";
            lblDepartment.Size = new Size(111, 25);
            lblDepartment.TabIndex = 10;
            lblDepartment.Text = "Department:";
            // 
            // lblType
            // 
            lblType.AutoSize = true;
            lblType.Location = new Point(551, 25);
            lblType.Name = "lblType";
            lblType.Size = new Size(53, 25);
            lblType.TabIndex = 11;
            lblType.Text = "Type:";
            // 
            // lstCards
            // 
            lstCards.Location = new Point(12, 444);
            lstCards.Name = "lstCards";
            lstCards.Size = new Size(776, 200);
            lstCards.TabIndex = 12;
            lstCards.UseCompatibleStateImageBehavior = false;
            lstCards.View = View.Details;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 11F);
            label1.ForeColor = Color.Crimson;
            label1.Location = new Point(727, 20);
            label1.Name = "label1";
            label1.Size = new Size(462, 30);
            label1.TabIndex = 12;
            label1.Text = "When adding a card, all fields except Card ID are required.";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 11F);
            label2.ForeColor = Color.Crimson;
            label2.Location = new Point(611, 125);
            label2.Name = "label2";
            label2.Size = new Size(710, 30);
            label2.TabIndex = 13;
            label2.Text = "When updating a card, Card ID is required, and other fields will be updated if filled, empty fields will not be modified.";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 11F);
            label3.ForeColor = Color.Crimson;
            label3.Location = new Point(727, 73);
            label3.Name = "label3";
            label3.Size = new Size(529, 30);
            label3.TabIndex = 14;
            label3.Text = "When deleting a card, Card ID is required, other fields will be ignored even if filled.";
            // 
            // CardManagementForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1333, 865);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(lblType);
            Controls.Add(lblDepartment);
            Controls.Add(lblCardName);
            Controls.Add(lblCardId);
            Controls.Add(cmbType);
            Controls.Add(txtDepartment);
            Controls.Add(txtCardName);
            Controls.Add(txtCardId);
            Controls.Add(dataGridViewCards);
            Controls.Add(btnDeleteCard);
            Controls.Add(btnUpdateCard);
            Controls.Add(btnAddCard);
            Margin = new Padding(5, 6, 5, 6);
            Name = "CardManagementForm";
            Text = "Card Management";
            Load += CardManagementForm_Load_1;
            ((System.ComponentModel.ISupportInitialize)dataGridViewCards).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.Button btnAddCard;
        private System.Windows.Forms.Button btnUpdateCard;
        private System.Windows.Forms.Button btnDeleteCard;
        private System.Windows.Forms.DataGridView dataGridViewCards;
        private System.Windows.Forms.TextBox txtCardId;
        private System.Windows.Forms.TextBox txtCardName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtDepartment;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.Label lblCardId;
        private System.Windows.Forms.ListView lstCards;
        private System.Windows.Forms.Label lblDepartment;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblCardName;
        private Label label1;
        private Label label2;
        private Label label3;
    }
}