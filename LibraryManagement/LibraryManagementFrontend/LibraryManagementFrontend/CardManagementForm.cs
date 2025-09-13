using System;
using System.Windows.Forms;
using LibraryManagementBackend;
using System.Collections.Generic;
using System.Xml.Linq;

namespace LibraryManagementFrontend.Forms
{
    public partial class CardManagementForm : Form
    {
        private readonly CardRepository _cardRepository;

        // Updated constructor to accept CardRepository from MainForm
        public CardManagementForm(CardRepository cardRepository)
        {
            InitializeComponent();
            _cardRepository = cardRepository ?? throw new ArgumentNullException(nameof(cardRepository));
        }

        private void btnAddCard_Click(object sender, EventArgs e)
        {
            // Collect card details from user input
            string name = txtCardName.Text;
            string department = txtDepartment.Text;
            string typeText = cmbType.Text;

            if (string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(department) ||
                string.IsNullOrWhiteSpace(typeText))
            {
                MessageBox.Show("All fields except Card ID are required. Please fill in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (typeText.Length != 1 || (typeText[0] != 'T' && typeText[0] != 'S'))
            {
                MessageBox.Show("The Type field must be either 'T' or 'S'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            char type = typeText[0];

            // Create a new card object
            Card newCard = new Card
            {
                Name = name,
                Department = department,
                Type = type
            };

            // Add the card to the database
            try
            {
                _cardRepository.AddCard(newCard);
                MessageBox.Show("Card added successfully.");
                RefreshCardList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdateCard_Click(object sender, EventArgs e)
        {
            // Collect card ID and updated details from user input
            int cardId = int.Parse(txtCardId.Text);
            string name = txtCardName.Text;
            string department = txtDepartment.Text;
            string typeText = cmbType.Text;

            // Get the existing card and update its details
            Card existingCard = _cardRepository.GetCardById(cardId);
            if (existingCard != null)
            {
                if (!string.IsNullOrWhiteSpace(name))
                {
                    existingCard.Name = name;
                }

                if (!string.IsNullOrWhiteSpace(department))
                {
                    existingCard.Department = department;
                }

                if (!string.IsNullOrWhiteSpace(typeText))
                {
                    if (typeText.Length == 1 && (typeText[0] == 'T' || typeText[0] == 'S'))
                    {
                        existingCard.Type = typeText[0];
                    }
                    else
                    {
                        MessageBox.Show("The Type field must be either 'T' or 'S'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                _cardRepository.UpdateCard(existingCard);
                MessageBox.Show("Card updated successfully.");
                // Refresh card list
                RefreshCardList();
            }
            else
            {
                MessageBox.Show("Card not found.");
            }
        }

        private void btnDeleteCard_Click(object sender, EventArgs e)
        {
            // Collect card ID from user input
            string cardIdText = txtCardId.Text;

            if (string.IsNullOrWhiteSpace(cardIdText))
            {
                MessageBox.Show("Please enter a valid card ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(cardIdText, out int cardId))
            {
                MessageBox.Show("Card ID must be a valid number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Check if the card exists
                Card cardToDelete = _cardRepository.GetCardById(cardId);
                if (cardToDelete == null)
                {
                    MessageBox.Show($"Card with ID {cardId} does not exist and cannot be deleted.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Stop delete operation
                }

                // Delete the card from the database
                _cardRepository.DeleteCard(cardId);
                MessageBox.Show("Card successfully deleted.");
                // Refresh card list
                RefreshCardList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnViewCards_Click(object sender, EventArgs e)
        {
            // Fetch all cards from the database
            List<Card> cards = _cardRepository.GetAllCards();

            // Clear existing items and columns
            lstCards.Items.Clear();
            lstCards.Columns.Clear();

            // Set up columns
            lstCards.Columns.Add("Card ID", 100);
            lstCards.Columns.Add("Name", 100);
            lstCards.Columns.Add("Department", 100);
            lstCards.Columns.Add("Type", 100);

            // Display cards in the ListView
            foreach (var card in cards)
            {
                ListViewItem item = new ListViewItem(card.CardId.ToString());
                item.SubItems.Add(card.Name);
                item.SubItems.Add(card.Department);
                item.SubItems.Add(card.Type.ToString());
                lstCards.Items.Add(item);
            }

            // Ensure the ListView is in Details view mode
            lstCards.View = View.Details;
        }

        private void CardManagementForm_Load(object sender, EventArgs e)
        {
            // Load all cards when the form loads
            btnViewCards_Click(sender, e);
        }

        private void CardManagementForm_Load_1(object sender, EventArgs e)
        {
            if (_cardRepository == null)
            {
                MessageBox.Show("Card repository is not initialized.");
                return;
            }

            if (dataGridViewCards == null)
            {
                MessageBox.Show("DataGridView is not initialized.");
                return;
            }

            // Fetch all cards from the repository
            List<Card> cards = _cardRepository.GetAllCards();

            // Bind the cards to the DataGridView
            dataGridViewCards.DataSource = cards;
        }

        private void RefreshCardList()
        {
            // Fetch all cards from the database
            List<Card> cards = _cardRepository.GetAllCards();

            // Bind the cards to the DataGridView
            dataGridViewCards.DataSource = null;
            dataGridViewCards.DataSource = cards;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
