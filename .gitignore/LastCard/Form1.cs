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
    public partial class Form1 : Form
    {
        Graphics cardTable;
        Game game;
        Card selectedCard;
        bool mouseDown;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cardTable = tablePictureBox.CreateGraphics();
            game = new Game(this);
            
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            game.start();
            game.MainDeck.Cards[0].flip();
            game.DiscardPile.add(game.MainDeck.serve());
            refresh();
        }

        private Card mouseOnCard(List<Card> pile, MouseEventArgs mousePos)
        {
            foreach (Card card in pile)
            {
                if (card.Area.Contains(mousePos.Location))
                {
                    return card;
                }
            }
            return null;
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            Card tempPlayerSelectedCard = mouseOnCard(game.PlayerHand.getCards(), e);
            Card tempDeckSelectedCard = mouseOnCard(game.MainDeck.Cards, e);

            if (tempPlayerSelectedCard != null)
            {
                selectedCard = tempPlayerSelectedCard;
                selectedCard.OldX = selectedCard.X;
                selectedCard.OldY = selectedCard.Y;
            }
            else if (tempDeckSelectedCard != null)
            {
                selectedCard = tempDeckSelectedCard;
                selectedCard.OldX = selectedCard.X;
                selectedCard.OldY = selectedCard.Y;
            }


        }

        private void tablePictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown) { game.moveCard(selectedCard, e); }
        }

        private void tablePictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
            game.checkMatch(e, selectedCard);
            selectedCard = null;
        }

        public void display(Card card)
        {
            cardTable.DrawImage(card.getImage(), card.X, card.Y, card.Width, card.Height);
        }

        public void coverCard(Card card)
        {
            cardTable.FillRectangle(new SolidBrush(Color.Green), new Rectangle(card.X, card.Y, card.Width, card.Height));
        }

        public void refresh()
        {
            drawCards(game.ComputerHand.getCards());
            drawCards(game.PlayerHand.getCards());
            drawCards(game.DiscardPile.Cards);
            drawCards(new List<Card> { game.MainDeck.Cards[0] });
        }

        private void drawCards(List<Card> cards)
        {
            foreach (Card card in cards)
            {
                display(card);
            }
        }



    }

    
}
