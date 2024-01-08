using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Threading;

public class Game
{
    private List<Card> deck;
    private List<int> flippedCardIds;
    public int gameSize { get; private set; }
    private int currentLevel;

    public Game(int gameSize)
    {
        this.gameSize = gameSize;
        InitializeDeck();
        ShuffleDeck();
        flippedCardIds = new List<int>();
    }

    public int FlippedCardCount => flippedCardIds.Count;

    private void InitializeDeck()
    {
        deck = new List<Card>();
        for (int i = 0; i < gameSize; i++)
        {
            deck.Add(new Card(i, gameSize));
            deck.Add(new Card(i + gameSize, gameSize));
        }
    }

    private void ShuffleDeck()
    {
        Random rand = new Random();
        int n = deck.Count;
        while (n > 1)
        {
            n--;
            int k = rand.Next(n + 1);
            Card value = deck[k];
            deck[k] = deck[n];
            deck[n] = value;
        }
    }

    public void FlipCard(int cardId)
    {
        if (!flippedCardIds.Contains(cardId))
        {
            flippedCardIds.Add(cardId);
            deck[cardId].IsFlipped = true;
        }
    }

    private void ResetFlippedCards()
    {
        flippedCardIds.Clear();
        foreach (var card in deck)
        {
            card.IsFlipped = false;
        }
    }

    public async void CheckForMatch()
    {
        int card1Id = flippedCardIds[0];
        int card2Id = flippedCardIds[1];

        if (deck[card1Id].Color == deck[card2Id].Color)
        {
            deck[card1Id].IsMatched = true;
            deck[card2Id].IsMatched = true;
        }
        else
        {
            //await Task.Delay(500);
            foreach (var cardId in flippedCardIds)
            {
                deck[cardId].IsFlipped = false;
            }
        }

        ResetFlippedCards();
    }

    public bool IsGameFinished()
    {
        foreach (var card in deck)
        {
            if (!card.IsMatched)
            {
                return false;
            }
        }
        return true;
    }

    public List<Card> Deck => deck;
}
