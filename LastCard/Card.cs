using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastCard {
    public class Card {
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

        public Card(int rank, string suit) {
            this.rank = rank;
            this.suit = suit;

            image = (Image)Properties.Resources.ResourceManager.GetObject("_" + rank + suit);
        }

        public bool matches(Card card) {
            if (card.Rank == rank) { return true; }
            else if (card.Suit == suit) { return true; }
            else { return false; }
        }

        public void flip() {
            faceUp = !faceUp;
        }

        public Image getImage() {
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
}
