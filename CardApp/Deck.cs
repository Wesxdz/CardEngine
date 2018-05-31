using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardApp {
    public class Deck {

        public List<Card> cards = new List<Card>();
        public List<Card> usedCards = new List<Card>();

        public Deck() {
            
        }

        /// <summary>
        /// Creates a standard deck of 52 cards ranging from 1 - 13 with suits of 0 - 3
        /// </summary>
        /// <returns></returns>
        public static Deck CreateStandardDeck() {
            Deck deck = new Deck();

            for(int i = 0; i <= 13; i++) {
                deck.AddCard(new Card(i, 0));
                deck.AddCard(new Card(i, 1));
                deck.AddCard(new Card(i, 2));
                deck.AddCard(new Card(i, 3));
            }

            return deck;
        }

        public void Shuffle() {
            Random rand = new Random();

            for(int i = 0; i < cards.Count; i++) {
                int k = rand.Next(0, cards.Count);
                Card temp = cards[i];
                cards[i] = cards[k];
                cards[k] = temp;
            }
        }

        /// <summary>
        /// Gets a random card
        /// </summary>
        /// <returns></returns>
        public Card GetCard() {
            if (cards.Count < 1) {
                cards.AddRange(usedCards);
                usedCards.Clear();
            }

            Card c = cards[cards.Count - 1];

            cards.RemoveAt(cards.Count - 1);

            usedCards.Add(c);

            return c;
        }
        /// <summary>
        /// Gets a card specified Index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Card GetCard(int index) {
            return cards[index];
        }

        /// <summary>
        /// Removes the specified cards and adds it into UsedCards.
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public bool RemoveCard(Card card) {
            bool success = cards.Remove(card);
            if (success) {
                usedCards.Add(card);
            }
            return success;
        }
        /// <summary>
        /// Removes the card at index and adds it to UsedCards.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool RemoveCard(int index) {
            Card card = cards[index];
            cards.RemoveAt(index);
            usedCards.Add(card);
            return true;
        }

        /// <summary>
        /// Adds the card to the current Card Pool.
        /// </summary>
        /// <param name="card"></param>
        public void AddCard(Card card) {
            cards.Add(card);
        }
        /// <summary>
        /// Adds the card to the current Card Pool at the specified index.
        /// </summary>
        /// <param name="card"></param>
        /// <param name="index"></param>
        public void AddCard(Card card, int index) {
            cards.Insert(index, card);
        }

        public bool HasCard(Card card) {
            return cards.Exists((x) => x.Rank == card.Rank && x.Suit == card.Suit);
        }

    }
}
