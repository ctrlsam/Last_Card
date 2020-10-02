using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastCard {
    public class Hand {
        List<Slot> slots;
        bool human;

        public Hand(bool human) {
            this.human = human;

            slots = new List<Slot>();
            int cardSpaceing = 120;

            // Human Player
            if (human) {
                // Add slot to slots list
                int x = 60;
                int y = 400;
                for (int i = 0; i < 7; i++) {
                    slots.Add(new Slot(x, y));
                    x += cardSpaceing;
                }
            }

            // Computer Player
            else if (!human) {
                // Add slot to slots list
                int x = 60;
                int y = 20;
                for (int i = 0; i < 7; i++) {
                    slots.Add(new Slot(x, y));
                    x += cardSpaceing;
                }
            }
        }

        public void add(Card aCard) {
            foreach (Slot slot in slots) {
                if (slot.Available) {
                    if (human) { aCard.flip(); }
                    aCard.X = slot.X;
                    aCard.Y = slot.Y;
                    slot.Card = aCard;
                    break;
                }
            }
        }

        public void remove(Card card) {
            foreach (Slot slot in slots) {
                if (slot.Card == card) {
                    slot.Card = null;
                    break;
                }
            }
        }

        public List<Card> getCards() {
            List<Card> cards = new List<Card>();
            foreach (Slot slot in slots) {
                if (slot.Card != null) {
                    cards.Add(slot.Card);
                }
            }
            return cards;
        }
        public List<Slot> Slots {get { return slots; }}
    }
}
