using Laboratorio_8_OOP_201920.Cards;
using System;
using System.Collections.Generic;
using System.Text;

namespace Laboratorio_8_OOP_201920
{
    public class PlayerEventArgs : EventArgs
    {
        Card card;
        Player py;

        public Card Card { get => card; set => card = value; }
        public Player Py { get => py; set => py = value; }
    }
}
