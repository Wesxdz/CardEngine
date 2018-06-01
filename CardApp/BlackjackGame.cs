using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CardApp {
    class BlackjackGame : Game {

        public class Player {
            public string name;
            public List<Card> cards = new List<Card>();
            public int bank = 20;

            public int currBet = 0;
            public bool isHittting = true;
        }

        public static int PLAYER_AMO = 2;

        Deck deck;

        // House...
        public List<Card> houseCards;

        List<Player> players = new List<Player>();
        int currPlayer;

        // Black Jack should be split into rounds
        // Each Round, 

        public override void Start() {
            StartRound();
        }

        public override void InitializePlayers(int playerAmo, string[] names, bool[] areHuman) {
            PLAYER_AMO = playerAmo;

            for (int i = 0; i < playerAmo; i++) {
                Player p = new Player();
                p.name = names[i];
                players.Add(p);
                // Computers aren't allowed in BlackJack...
            }
        }

        public override void Exit() {
            // Determine Winner based on amount they have...
        }

        public void GetAnte(int[] ante) {
            for (int i = 0; i < PLAYER_AMO; i++) {
                int bet = ante[i];
                if (!IsValidBet(bet)) {
                    MessageBox.Show("Ante must be $1, $5, or $10");
                }
                players[i].currBet = bet;
            }

            StartRound();
        }
        public bool IsValidBet(int bet) {
            return bet == 1 || bet == 5 || bet == 10;
        }

        public void StartRound() {

            // 1st card
            for (int i = 0; i < PLAYER_AMO; i++) {
                GrabCardFromDeck(players[i]);
                players[i].isHittting = true;
            }
            // House Card
            {
                Card c = deck.GetCard(deck.cards.Count - 1);
                deck.RemoveCard(deck.cards.Count - 1);
                houseCards.Add(c);
            }


            // 2nd card
            for (int i = 0; i < PLAYER_AMO; i++) {
                GrabCardFromDeck(players[i]);
            }
            // House Card
            {
                Card c = deck.GetCard(deck.cards.Count - 1);
                deck.RemoveCard(deck.cards.Count - 1);
                houseCards.Add(c);
            }

            currPlayer = 0;
        }
        public void EndRound() {

        }

        public void TakeTurn(int player, bool isHitting) {
            if(player != currPlayer) {
                MessageBox.Show("Invalid Player playing!");
                return;
            }

            if (isHitting) {
                GrabCardFromDeck(players[player]);

                var possibleSum = GetSum(players[player].cards);
            } else {
                players[player].isHittting = false;
            }

            // Find Next Player who is Hitting...
            if(players.Exists((x) => x.isHittting)) {
                while (true) {
                    currPlayer++;
                    if (currPlayer >= players.Count) {
                        currPlayer = 0;
                    }

                    if (players[currPlayer].isHittting) {
                        break;
                    }
                }
            } else {
                // If no one is hitting, End Round...
                EndRound();
            } 
        }

        /// <summary>
        /// Takes a card from the "Deck" pile and adds it to the player's Hand
        /// </summary>
        /// <param name="player"></param>
        public void GrabCardFromDeck(Player player) {
            Card c = deck.GetCard(deck.cards.Count - 1);
            deck.RemoveCard(deck.cards.Count - 1);
            player.cards.Add(c);
        }

        public List<int> GetSum(List<Card> cards) {
            List<int> possibleSums = new List<int>();
            possibleSums.Add(0);
            for (int i = 0; i < cards.Count; i++){
                Card c = cards[i];
                if(c.Rank == 11 || c.Rank == 12 || c.Rank == 13) {
                    possibleSums.ForEach((x) => x += 10);
                }
                if(c.Rank == 1) {
                    int count = possibleSums.Count;
                    for(int y = 0; y < count; y++) {
                        possibleSums[y] += 1;

                        possibleSums.Add(possibleSums[y] + 10);
                    }
                }
            }
            return possibleSums;
        }

        public override void Load(string path) {
            throw new NotImplementedException();
        }
        public override void Save(string path) {
            throw new NotImplementedException();
        }
    }
}
