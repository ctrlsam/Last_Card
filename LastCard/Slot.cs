using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastCard {
    public class Slot {
        int x;
        int y;
        Card card = null;
        Rectangle area = Rectangle.Empty;

        public Slot(int x, int y) {
            this.x = x;
            this.y = y;
            this.area = new Rectangle(x, y, 70, 90);
        }

        public int X { get { return x; } }
        public int Y { get { return y; } }
        public bool Available { get { return card == null; } }
        public Card Card { set { card = value; if (card != null) { card.X = x; card.Y = y; } } get { return card; } }
        public Rectangle Area { get { return area; } }
    }

}
