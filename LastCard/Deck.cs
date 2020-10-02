using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastCard {
    public class Deck {
        List<Card> cards;
        int max;
        int x;
        int y;
        bool mainDeck;

        public Deck(int x, int y, bool mainDeck) {
            this.x = x;
            this.y = y;
            this.mainDeck = mainDeck;
            cards = new List<Card>();
            max = 52;
        }

        public void add(Card card) {
            if (cards.Count <= max) {
                card.OldX = x;
                card.OldY = y;
                card.X = x;
                card.Y = y;

                if (!mainDeck) {
                    cards.Insert(0, card);
                }
                else {
                    cards.Add(card);
                }
            }
        }

        public Card serve() {
            Card temp = cards[0];
            cards.RemoveAt(0);
            return temp;
        }

        public void shuffle() {
            if (cards != null) {
                Random random = new Random();
                for (int i = 0; i < cards.Count; i++) {
                    int randomInt = random.Next(cards.Count);
                    Card temp = cards[randomInt];
                    cards.RemoveAt(randomInt);
                    cards.Add(temp);
                }
            }
        }

        public void switchCards() {
            try {
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
}
