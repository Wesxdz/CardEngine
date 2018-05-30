using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CardApp {
    public class GoFishGame : Game {

        public static int STARTING_CARDS = 5;
        Deck deck;

        public struct Player {
            public Deck Hand;
            public Deck Pairs;
        }

        Player[] players;
        int currPlayer;

        //Players 2 - 4
        // Current Player

        // Should we have an abstract method to make a turn?
        // CardGame needs to be public

        public override void Exit() {
            
        }

        public override void Load(string path) {
            throw new NotImplementedException();
        }

        public override void Save(string path) {
            throw new NotImplementedException();
        }

        public override void Start() {
            deck = Deck.CreateStandardDeck();
            deck.Shuffle();

            // How do we get the amount of Players????
            players = new Player[2];
            currPlayer = 0;

            for(int i = 0; i < players.Length; i++) {
                players[i] = new Player();
            }

            for (int i = 0; i < STARTING_CARDS; i++) {
                for(int y = 0; y < players.Length; y++) {
                    Card playerCard = deck.GetCard(deck.cards.Count - 1);
                    deck.RemoveCard(deck.cards.Count - 1);
                    players[y].Hand.AddCard(playerCard);
                }
            }

            type = GameType.GoFish;
        }

        public void TakePlayerTurn(int playerTurn, int playerToTake, Card chosenCard) {
            // Invliad Player Turn
            if(playerTurn != currPlayer) {
                MessageBox.Show("The wrong player is taking their turn!");
                return;
            }

            // cAn NOT TAKE FROM yourself.
            if(playerTurn == playerToTake) {
                MessageBox.Show("You can not take a card from yourself!");
                return;
            }

            // Validate Chosen Card is in player's Deck / Hand
            if (!players[playerTurn].Hand.HasCard(chosenCard)) {
                MessageBox.Show("Player can not Choose a card not part of their handw!");
                return;
            }

            // Try to Take Card
            if(TakePlayerCard(players[playerTurn], players[playerToTake], chosenCard)) {

            } else { // If it didn't work...
                GrabCardFromDeck(players[playerTurn]);

                // Check for End of Game...
                if (deck.cards.Count == 0) { }
            }

            // Increment Player
            if (++currPlayer >= players.Length) {
                currPlayer = 0;
            }
        }

        /// <summary>
        /// Checks if the PlayerTaken has the given Card.
        /// <para></para>
        /// If so, it is removed from PlayerTaken and added to PlayerTaking. 
        /// <para></para> Returns true if PlayerTaken has that card, false otherwise.
        /// </summary>
        /// <param name="playerTaking"></param>
        /// <param name="playerTaken"></param>
        /// <param name="card"></param>
        /// <returns></returns>
        public bool TakePlayerCard(Player playerTaking, Player playerTaken, Card card) {
            if(CheckPlayerCard(playerTaken, card)) {
                playerTaken.Hand.RemoveCard(card);
                playerTaking.Hand.AddCard(card);
                CheckForPairs(playerTaking, card);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if the Player has that card in their "Deck"
        /// </summary>
        /// <param name="isPlayer1"></param>
        /// <param name="card"></param>
        /// <returns></returns>
        public bool CheckPlayerCard(Player player, Card card) {
            return player.Hand.HasCard(card);
        }

        /// <summary>
        /// Takes a card from the "Deck" pile and adds it to the player's Hand
        /// </summary>
        /// <param name="player"></param>
        public void GrabCardFromDeck(Player player) {
            Card c = deck.GetCard(deck.cards.Count-1);
            player.Hand.AddCard(c);
            CheckForPairs(player, c);
        }

        /// <summary>
        /// Checks if the player has a pair using the Given Cards Rank
        /// <para></para>
        /// If so, removes from player hand and adds to their pairs...
        /// </summary>
        /// <param name="player"></param>
        /// <param name="cardGained"></param>
        public void CheckForPairs(Player player, Card cardGained) {
            var cards = player.Hand.cards.Where((x) => x.Rank == cardGained.Rank);
            if (cards.Count() >= 2) {
                for (int i = 0; i < 2; i++) {
                    Card pair = cards.ElementAt(i);
                    player.Hand.RemoveCard(pair);
                    player.Pairs.AddCard(pair);
                }
            }
        }
    }
}

// Requirements - 
// Starting Amount of Players
// End Game
// Win Detection
