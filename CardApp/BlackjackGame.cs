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
            public List<List<Card>> cards = new List<List<Card>>();
            public int bank = 20;

            public int currBet = 0;
            public List<bool> isHitting = new List<bool>();
            public bool canSplit = false;
        }

        public static int PLAYER_AMO = 2;
        public static int CHARLIE_MULTI = 4;
        public static int WIN_MULTI = 2;
        public static int BLACKJACK_MULTI = 3;
        public static int DRAW_MULTI = 1;
        public static int DROPOUT = -50;

        Deck deck;

        // House...
        public List<Card> houseCards;

        List<Player> players = new List<Player>();
        int currPlayer;

        bool roundOver = true;

        // Black Jack should be split into rounds
        // Each Round, We need to get each player's Ante
        // Then we process their turn...

            // What is splitting???

        public override void Start() {
            deck = Deck.CreateStandardDeck();
            deck.Shuffle();

            houseCards = new List<Card>();

            type = GameType.Blackjack;
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
                players[i].bank -= bet;
            }

            StartRound();
        }
        public bool IsValidBet(int bet) {
            return bet == 1 || bet == 5 || bet == 10;
        }

        public void StartRound() {
            // 1st card - Face Down to others...
            for (int i = 0; i < PLAYER_AMO; i++) {
                players[i].cards.Clear();
                players[i].cards.Add(new List<Card>());
                GrabCardFromDeck(players[i].cards[0]);
                players[i].isHitting[0] = true;
            }
            // House Card
            {
                houseCards.Clear();

                Card c = deck.GetCard(deck.cards.Count - 1);
                deck.RemoveCard(deck.cards.Count - 1);
                houseCards.Add(c);
            }


            // 2nd card - Face up to everyone
            for (int i = 0; i < PLAYER_AMO; i++) {
                GrabCardFromDeck(players[i].cards[0]);

                if(players[i].cards[0][0] == players[i].cards[0][1]) {
                    players[i].canSplit = true;
                }
            }
            // House Card
            {
                Card c = deck.GetCard(deck.cards.Count - 1);
                deck.RemoveCard(deck.cards.Count - 1);
                houseCards.Add(c);
            }

            currPlayer = 0;
            roundOver = false;
        }
        public void EndRound() {
            int houseSum = GetSum(houseCards);
            bool houseIsValid = houseSum <= 21;

            for (int i = 0; i < PLAYER_AMO; i++) {
                Player p = players[i];
                for(int y = 0; y < p.cards.Count; y++) {
                    var hand = p.cards[y];

                    int playerSum = GetSum(hand);
                    bool playerIsValid = playerSum <= 21;

                    if (playerIsValid && players[i].cards.Count >= 5) {
                        // 5 Card Charlie
                        players[i].bank += players[i].currBet * CHARLIE_MULTI;
                    } else if (!playerIsValid && !houseIsValid) {
                        // Draw
                        players[i].bank += players[i].currBet * DRAW_MULTI;
                    } else if (!houseIsValid && playerIsValid) {
                        // Won 
                        players[i].bank += players[i].currBet * WIN_MULTI;
                    } else {
                        if (playerSum == houseSum) {
                            // Draw
                            players[i].bank += players[i].currBet * DRAW_MULTI;
                        } else if (playerSum == 21) {
                            // Black Jack
                            players[i].bank += players[i].currBet * BLACKJACK_MULTI;
                        } else if (playerSum > houseSum) {
                            // Win
                            players[i].bank += players[i].currBet * WIN_MULTI;
                        }
                    }
                }         

                if(players[i].bank <= DROPOUT) {
                    players.RemoveAt(i);
                    i--;
                }
            }

            roundOver = true;
        }

        public void Split(int player) {
            if (player != currPlayer) {
                MessageBox.Show("Invalid Player spliting!");
                return;
            }

            Player p = players[player];
            if (!p.canSplit) {
                MessageBox.Show("Player can not split!");
                return;
            }

            Card c = p.cards[0][0];
            p.cards[0].Remove(c);

            p.cards.Add(new List<Card>());
            p.cards[1].Add(c);

            GrabCardFromDeck(p.cards[0]);
            GrabCardFromDeck(p.cards[1]);

            p.isHitting.Add(true);

            p.bank -= p.currBet;
        }

        public void TakeTurn(int player, bool[] isHitting) {
            if(player != currPlayer) {
                MessageBox.Show("Invalid Player playing!");
                return;
            }
            if (roundOver) {
                MessageBox.Show("Round is over! Ante up to begin!");
                return;
            }

            Player p = players[player];

            p.canSplit = false;

            for(int i = 0; i < p.cards.Count; i++) {
                var hand = p.cards[i];

                if (isHitting[i]) {
                    GrabCardFromDeck(p.cards[i]);

                    var possibleSum = GetSum(p.cards[i]);
                    if (possibleSum > 21) {
                        players[player].isHitting[i] = false;

                    } else if (players[player].cards.Count >= 5) { // 5 card charlie
                        players[player].isHitting[i] = false;
                    }
                } else {
                    players[player].isHitting[i] = false;
                }
            }

            // Find Next Player who is Hitting...
            if(players.Exists((x) => x.isHitting.Exists((y) => y))) {
                while (true) {
                    currPlayer++;
                    if (currPlayer >= players.Count) {
                        TakeHouseTurn();
                        currPlayer = 0;
                    }

                    if (players[currPlayer].isHitting.Exists((y) => y)) {
                        break;
                    }
                }
            } else {
                // If no one is hitting, End Round...
                TakeHouseTurn();
                EndRound();
            } 
        }

        public void TakeHouseTurn() {
            if(GetSum(houseCards) < 17) {
                Card c = deck.GetCard();
                deck.RemoveCard(c);
                houseCards.Add(c);
            }
        }

        /// <summary>
        /// Takes a card from the "Deck" pile and adds it to the player's Hand
        /// </summary>
        /// <param name="player"></param>
        public void GrabCardFromDeck(List<Card> cards) {
            Card c = deck.GetCard();
            deck.RemoveCard(c);
            cards.Add(c);
        }

        public int GetSum(List<Card> cards) {
            List<int> possibleSums = new List<int>();
            possibleSums.Add(0);
            for (int i = 0; i < cards.Count; i++){
                Card c = cards[i];
                if(c.Rank == 11 || c.Rank == 12 || c.Rank == 13) {
                    possibleSums.ForEach((x) => x += 10);
                } else if(c.Rank == 1) {
                    int count = possibleSums.Count;
                    for(int y = 0; y < count; y++) {
                        possibleSums[y] += 1;

                        possibleSums.Add(possibleSums[y] + 10);
                    }
                } else {
                    possibleSums.ForEach((x) => x += c.Rank);
                }
            }

            var validSums = possibleSums.Where((x) => x <= 21);
            if(validSums.Count() > 0) {
                return validSums.Max();
            }
            return validSums.Count() > 0 ? validSums.Max() : possibleSums.Max();
        }

        public override void Load(string path) {
            throw new NotImplementedException();
        }
        public override void Save(string path) {
            throw new NotImplementedException();
        }

        public Player GetPlayer(int index) {
            return players[index];
        }
        public Player GetCurrentPlayer() {
            return players[currPlayer];
        }
        public List<Card> GetHouseCards() {
            return houseCards;
        }
    }
}
