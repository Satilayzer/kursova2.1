using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

public partial class CardMatchingForm : Form
{
    private Game game;
    LevelSelectionForm parentLevelSelector;

    public CardMatchingForm(Game game, LevelSelectionForm selector)
    {
        this.game = game;
        this.parentLevelSelector = selector;
        InitializeCards();
        int height = 160 * game.gameSize/2 + 30;
        this.Size = new System.Drawing.Size(447, height);
        this.FormClosing += CloseForm;
    }

    private void InitializeCards()
    {
        int cardWidth = 100;
        int cardHeight = 150;
        int spacing = 10;

        for (int i = 0; i < game.Deck.Count; i++)
        {
            Button cardButton = new Button();
            cardButton.Size = new System.Drawing.Size(cardWidth, cardHeight);
            cardButton.Location = new System.Drawing.Point((cardWidth + spacing) * (i % 4), (cardHeight + spacing) * (i / 4));
            cardButton.Tag = i;
            cardButton.Click += CardButtonClick;
            Controls.Add(cardButton);
        }
    }

    static SemaphoreSlim reactSemaphore = new SemaphoreSlim(1);

    private async void CardButtonClick(object sender, EventArgs e)
    {
        Button clickedButton = (Button)sender;
        int cardId = (int)clickedButton.Tag;

        bool acquiredSemaphore = await reactSemaphore.WaitAsync(0);
        if (!acquiredSemaphore)
        {
            return;
        }

        try
        {
            game.FlipCard(cardId);
            UpdateCardDisplay();

            if (game.FlippedCardCount == 2)
            {
                await Task.Delay(500);
                game.CheckForMatch();
                UpdateCardDisplay();
            }
        } finally
        {
            reactSemaphore.Release();
        }
    }

    private void CloseForm(object  sender, FormClosingEventArgs e)
    {
        parentLevelSelector.Show();
    }

    private void UpdateCardDisplay()
    {
        for (int i = 0; i < game.Deck.Count; i++)
        {
            Button cardButton = (Button)Controls[i];
            Card card = game.Deck[i];

            if (card.IsFlipped || card.IsMatched)
            {
                if (card.IsMatched)
                {
                    cardButton.Visible = false;
                }
                else
                {
                    cardButton.BackColor = System.Drawing.Color.FromName(card.Color);
                }
            }
            else
            {
                cardButton.BackColor = DefaultBackColor;
            }
        }

        if (game.IsGameFinished())
        {
            MessageBox.Show("Усi пари знайдено");
            this.Close();
        }
    }
}
