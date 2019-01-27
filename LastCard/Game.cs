using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace LastCard
{
    public class Game
    {
        Deck mainDeck;
        Deck discardPile;
        Hand computerHand;
        Hand playerHand;
        Rectangle discardRect;
        Form1 form;

        public Game(Form1 form)
        {
            // Create card piles
            mainDeck = new Deck(50, 220, true);
            discardPile = new Deck(150, 220, false);
            playerHand = new Hand(true);
            computerHand = new Hand(false);
            discardRect = new Rectangle(DiscardPile.X, DiscardPile.Y, 70, 90);

            this.form = form;

            // Add Cards
            for (int rank = 1; rank <= 13; rank++)
            {
                foreach (string suit in new string[] { "hearts", "diamonds", "spades", "clubs" })
                {
                    mainDeck.add(new Card(rank, suit));
                }
            }
        }
        public void start()
        {
            // Suffle cards
            mainDeck.shuffle();

            // Add cards to Player and Computer pile
            for (int i = 0; i < 7; i++)
            {
                playerHand.add(mainDeck.serve());
                computerHand.add(mainDeck.serve());
            }
        }

        public void moveCard(Card selectedCard, MouseEventArgs e)
        {
            if (selectedCard != null)
            {
                Console.WriteLine("Mouse Move");
                form.coverCard(selectedCard);
                selectedCard.X = e.X - (selectedCard.Width / 2);
                selectedCard.Y = e.Y - (selectedCard.Height / 2);
                form.refresh();
                form.display(selectedCard);
            }
        }

        public void checkMatch(MouseEventArgs e, Card selectedCard)
        {
            try
            {
                if (selectedCard != null)
                {
                    if (discardRect.Contains(e.Location))
                    {
                        if (selectedCard.Rank == discardPile.Cards[0].Rank || selectedCard.Suit == discardPile.Cards[0].Suit)
                        {
                            int cardCycle = 0;
                            foreach (Card card in playerHand.getCards())
                            {
                                if (card.Rank == playerHand.getCards()[cardCycle].Rank && card.Suit == playerHand.getCards()[cardCycle].Suit)
                                {
                                    form.coverCard(selectedCard);
                                    
                                    discardPile.add(selectedCard);
                                    selectedCard.OldX = selectedCard.X;
                                    selectedCard.OldY = selectedCard.Y;
                                    form.refresh();
                                    playerHand.remove(selectedCard);
                                    discardPile.switchCards();
                                    discardPile.switchCards();
                                    break;
                                }
                                cardCycle++;
                            }
                        }
                    }
                    form.coverCard(selectedCard);
                    selectedCard.X = selectedCard.OldX;
                    selectedCard.Y = selectedCard.OldY;
                }
                form.refresh();
            }
            catch (ArgumentException) { Console.WriteLine("No discard pile"); }
        }
            
        public Hand PlayerHand{ get { return playerHand; } }
        public Hand ComputerHand { get { return computerHand; } }
        public Deck MainDeck { get { return mainDeck; } }
        public Deck DiscardPile { get { return discardPile; } }
    }

    public class Hand
    {
        List<Slot> slots;
        bool human;

        public Hand(bool human)
        {
            this.human = human;

            slots = new List<Slot>();
            int cardSpaceing = 120;

            // Human Player
            if (human)
            {
                // Add slot to slots list
                int x = 60;
                int y = 400;
                for (int i = 0; i < 7; i++)
                {
                    slots.Add(new Slot(x, y));
                    x += cardSpaceing;
                }
            }

            // Computer Player
            else if (!human)
            {
                // Add slot to slots list
                int x = 60;
                int y = 20;
                for (int i = 0; i < 7; i++)
                {
                    slots.Add(new Slot(x, y));
                    x += cardSpaceing;
                }
            }
        }

        public void add(Card aCard)
        {
            foreach (Slot slot in slots)
            {
                if (slot.Available)
                {
                    if (human) { aCard.flip(); }
                    aCard.X = slot.X;
                    aCard.Y = slot.Y;
                    slot.Card = aCard;
                    break;
                }
            }
        }

        public void remove(Card card)
        {
            foreach (Slot slot in slots)
            {
                if (slot.Card == card)
                {
                    slot.Card = null;
                    break;
                }
            }
        }

        public List<Card> getCards()
        {
            List<Card> cards = new List<Card>();
            foreach (Slot slot in slots)
            {
                if (slot != null)
                {
                    cards.Add(slot.Card);
                }
            }
            return cards;
        }      
    }

    public class Deck
    {
        List<Card> cards;
        int max;
        int x;
        int y;
        bool mainDeck;

        public Deck(int x, int y, bool mainDeck)
        {
            this.x = x;
            this.y = y;
            this.mainDeck = mainDeck;
            cards = new List<Card>();
            max = 52;
        }

        public void add(Card card)
        {
            if (cards.Count <= max)
            {
                card.X = x;
                card.Y = y;
                cards.Add(card);
            }
        }

        public Card serve()
        {
            Card temp = cards[0];
            cards.RemoveAt(0);
            return temp;
        }

        public void shuffle()
        {
            if (cards != null)
            {
                Random random = new Random();
                for (int i = 0; i < cards.Count; i++)
                {
                    int randomInt = random.Next(cards.Count);
                    Card temp = cards[randomInt];
                    cards.RemoveAt(randomInt);
                    cards.Add(temp);
                }
            }
        }

        public void switchCards()
        {
            try
            {
                Card temp = cards[0];
                cards[0] = cards[1];
                cards[1] = temp;
            }
            catch (ArgumentOutOfRangeException) { Console.WriteLine("Cant switch cards: Out of range"); }
        }

        public List<Card> Cards { get { return cards; } }
        public int X { get { return x; } }
        public int Y { get { return y; } }

    }

    public class Card
    {
        int rank;
        string suit;
        int x = 0;
        int y = 0;
        int width = 70;
        int height = 90;
        int oldX = 0;
        int oldY = 0;
        bool faceUp = false;

        Image image;

        public Card(int rank, string suit)
        {
            this.rank = rank;
            this.suit = suit;

            image = (Image)Properties.Resources.ResourceManager.GetObject("_" + rank + suit);
        }

        public bool matches(Card card)
        {
            if (card.Rank == rank) { return true; }
            else if (card.Suit == suit) { return true; }
            else { return false; }
        }

        public void flip()
        {
            faceUp = !faceUp;
        }

        public Image getImage()
        {
            return image;
        }

        public int X { get { return x; } set { x = value; } }
        public int Y { get { return y; } set { y = value; } }
        public int Width { get { return width; } }
        public int Height { get { return height; } }
        public string Suit { get { return suit; } }
        public int Rank { get { return rank; } }
        public Rectangle Area { get { return new Rectangle(x, y, width, height); } }
        public int OldX { get { return oldX; } set { oldX = value; } }
        public int OldY { get { return oldY; } set { oldY = value; } }
        public bool FaceUp { get { return faceUp; } }
    }

    public class Slot
    {
        int x;
        int y;
        Card card = null;

        public Slot(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int X { get { return x; } }
        public int Y { get { return y; } }
        public bool Available { get { return card == null; } }
        public Card Card { set { card = value; if (card != null) { card.X = x; card.Y = y; } } get { return card; } }
    }

    public class Highscores
    {
        List<Score> scores = new List<Score>();

        public void addScore(Score score)
        {
            scores.Add(score);
        }

    }

    public class Score
    {
        string name;
        int score;

        public Score(string name, int score)
        {
            this.name = name;
            this.score = score;
        }

        public string Name { get { return name; } }
        public int Number { get { return score; } }
    }
}
