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

        public Game(Form1 form) {

            this.form = form;

            // Create card piles
            mainDeck = new Deck(50, 220, true);
            discardPile = new Deck(150, 220, false);

            // Create hands
            playerHand = new Hand(true);
            computerHand = new Hand(false);

            // Discard visuals
            discardRect = new Rectangle(DiscardPile.X, DiscardPile.Y, 70, 90);

            // Generate cards
            for (int rank = 1; rank <= 13; rank++){
                foreach (string suit in new string[] { "hearts", "diamonds", "spades", "clubs" }){
                    mainDeck.add(new Card(rank, suit));
                }
            }
        }
        public void start() {

            // Suffle cards
            mainDeck.shuffle();

            // Add cards to Player and Computer pile
            for (int i = 0; i < 7; i++) {
                playerHand.add(mainDeck.serve());
                computerHand.add(mainDeck.serve());
            }

        }

        public void moveCard(Card selectedCard, MouseEventArgs e) {

            if (selectedCard != null){
                form.coverCard(selectedCard);
                selectedCard.X = e.X - (selectedCard.Width / 2);
                selectedCard.Y = e.Y - (selectedCard.Height / 2);
                form.refresh();
                form.display(selectedCard);
            }

        }

        public void checkMatch(MouseEventArgs e, Card selectedCard) {
            try {
                if (selectedCard == null) return;

                // Discard pile is being used
                if (discardRect.Contains(e.Location)) {

                    // Match
                    if (selectedCard.matches(discardPile.Cards[0])) {
                        form.coverCard(selectedCard); // hide it by drawing over it until next render
                        discardPile.add(selectedCard);
                        playerHand.remove(selectedCard); // remove card from player
                        form.refresh();
                        return;
                    }

                }

                // Pickup pile is being used
                if (selectedCard == mainDeck.Cards[0]) { // card is from pickup pile
                    foreach (Slot slot in playerHand.Slots) {
                        if (slot.Area.Contains(e.Location)) { // card is being dropped in this slot
                            if (slot.Card != null) return; // slot is already used

                            form.coverCard(selectedCard);
                            slot.Card = mainDeck.serve();
                            form.refresh();
                            return;
                        }
                    }
                }
                

                // No match
                form.coverCard(selectedCard);
                selectedCard.X = selectedCard.OldX;
                selectedCard.Y = selectedCard.OldY;
                form.refresh();
            }
            catch (ArgumentException) { Console.WriteLine("No discard pile"); }
        }
            
        public Hand PlayerHand{ get { return playerHand; } }
        public Hand ComputerHand { get { return computerHand; } }
        public Deck MainDeck { get { return mainDeck; } }
        public Deck DiscardPile { get { return discardPile; } }
    }
}
