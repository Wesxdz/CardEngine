using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace CardApp {

    public class Card {
        /// <summary>
        /// This is the Number of Card, i.e 1, 2, 3, 4... 
        /// <para></para>
        /// Jack = 11
        /// <para></para>
        /// Queen = 12
        /// <para></para>
        /// King = 13
        /// <para></para>
        /// A = 1
        /// </summary>
        public int Rank { get; private set; }
        /// <summary>
        /// This is the symbol of the card.
        /// <para></para>
        /// A standard card has Heart (0), Diamond (1), Club (2), and Spade (3)
        /// </summary>
        public int Suit { get; private set; }

        public bool IsFlipped { get; set; }

        public Card(int rank, int suite) {
            Rank = rank;
            Suit = suite;
        }
    }

}
