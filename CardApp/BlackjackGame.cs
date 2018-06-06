using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CardApp
{

    [Serializable]
    class BlackjackGame : Game
    {

        [Serializable]
        public class Player
        {
            public string name { get; set; }
            public List<List<Card>> cards { get; set; } = new List<List<Card>>();
            public int sum { get; set; } = 0;
            public int bank { get; set; } = 20;

            public int currBet { get; set; } = 0;
            public List<bool> isHitting { get; set; } = new List<bool>();
            public bool canSplit { get; set; } = false;
        }

        public static int PLAYER_AMO = 2;
        public static int CHARLIE_MULTI = 4;
        public static int WIN_MULTI = 2;
        public static int BLACKJACK_MULTI = 3;
        public static int DRAW_MULTI = 1;
        public static int DROPOUT = -50;

        public Deck deck { get; set; }

        // House...
        public List<Card> houseCards { get; set; } = new List<Card>();

        List<Player> players = new List<Player>();
        int currPlayer;

        public bool gameOver { get; private set; } = false;
        public bool roundOver { get; private set; } = true;

        // Black Jack should be split into rounds
        // Each Round, We need to get each player's Ante
        // Then we process their turn...

        // What is splitting???

        public override void Start()
        {
            deck = Deck.CreateStandardDeck();
            deck.Shuffle();

            type = GameType.Blackjack;
        }

        public override void InitializePlayers(int playerAmo, string[] names, bool[] areHuman)
        {
            PLAYER_AMO = playerAmo;

            for (int i = 0; i < playerAmo; i++)
            {
                Player p = new Player();
                p.name = names[i];
                players.Add(p);
                // Computers aren't allowed in BlackJack...
            }
        }

        public override void Exit()
        {
            // Determine Winner based on amount they have...
            MessageBox.Show("No players available. Game is ending.");
            gameOver = true;
        }

        public void GetAnte(int[] ante)
        {
            for (int i = 0; i < players.Count; i++)
            {
                int bet = ante[i];
                if (!IsValidBet(bet))
                {
                    MessageBox.Show("Ante must be $1, $5, or $10");
                }
                players[i].currBet = bet;
                players[i].bank -= bet;
            }

            StartRound();
        }

        public void TakeAnte(int player, int ante) {
            if (player != currPlayer) {
                MessageBox.Show("Invalid Player playing!");
                return;
            }
            if (!IsValidBet(ante)) {
                MessageBox.Show("Ante must be $1, $5, or $10");
            }

            Player p = players[player];

            p.currBet = ante;

            currPlayer++;
            if (currPlayer >= players.Count) {
                currPlayer = 0;
            }
        }

        public bool IsValidBet(int bet)
        {
            return bet == 1 || bet == 5 || bet == 10;
        }

        public void StartRound()
        {

            // 1st card - Face Down to others...
            for (int i = 0; i < players.Count; i++)
            {
                players[i].cards.Clear();
                players[i].cards.Add(new List<Card>());
                GrabCardFromDeck(players[i].cards[0], true);

                players[i].isHitting.Clear();
                players[i].isHitting.Add(true);

                players[i].sum = 0;
                players[i].bank -= players[i].currBet;
            }
            // House Card
            {
                houseCards.Clear();
                GrabCardFromDeck(houseCards, true);
            }


            // 2nd card - Face up to everyone
            for (int i = 0; i < players.Count; i++)
            {
                GrabCardFromDeck(players[i].cards[0], false);
                players[i].sum = GetSum(players[i].cards[0]);

                if (players[i].cards[0][0].Rank == players[i].cards[0][1].Rank)
                {
                    players[i].canSplit = true;
                }
            }
            // House Card
            {
                GrabCardFromDeck(houseCards, false);
            }

            currPlayer = 0;
            roundOver = false;
            players[currPlayer].cards[0][0].IsFlipped = false;
        }
        public void EndRound()
        {
            for (int e = 0; e < houseCards.Count; e++) {
                houseCards[e].IsFlipped = false;
                deck.AddCard(houseCards[e]);
            }
            int houseSum = GetSum(houseCards);
            bool houseIsValid = houseSum <= 21;

            for (int i = 0; i < players.Count; i++)
            {
                Player p = players[i];
                for (int y = 0; y < p.cards.Count; y++)
                {
                    var hand = p.cards[y];
                    for(int e = 0; e < hand.Count; e++) {
                        hand[e].IsFlipped = false;
                        deck.AddCard(hand[e]);
                    }

                    p.sum = GetSum(hand);
                    int playerSum = p.sum;
                    bool playerIsValid = playerSum <= 21;

                    if (playerIsValid && hand.Count >= 5)
                    {
                        // 5 Card Charlie
                        players[i].bank += players[i].currBet * CHARLIE_MULTI;
                    }
                    else if (!playerIsValid && !houseIsValid)
                    {
                        // Draw
                        players[i].bank += players[i].currBet * DRAW_MULTI;
                    }
                    else if (!houseIsValid && playerIsValid)
                    {
                        // Won 
                        players[i].bank += players[i].currBet * WIN_MULTI;
                    }
                    else if(houseIsValid && playerIsValid)
                    {
                        if (playerSum == houseSum)
                        {
                            // Draw
                            players[i].bank += players[i].currBet * DRAW_MULTI;
                        }
                        else if (playerSum == 21)
                        {
                            // Black Jack
                            players[i].bank += players[i].currBet * BLACKJACK_MULTI;
                        }
                        else if (playerSum > houseSum)
                        {
                            // Win
                            players[i].bank += players[i].currBet * WIN_MULTI;
                        }
                    }
                }
                if (players[i].bank <= DROPOUT) {
                    players.RemoveAt(i);
                    i--;
                }
            }
            PLAYER_AMO = players.Count;
            if(players.Count == 0) {
                Exit();
            }

            deck.Shuffle();
            roundOver = true;
            currPlayer = 0;
        }

        public void Split(int player)
        {
            if (player != currPlayer)
            {
                MessageBox.Show("Invalid Player spliting!");
                return;
            }

            Player p = players[player];
            if (!p.canSplit)
            {
                MessageBox.Show("Player can not split!");
                return;
            }

            Card c = p.cards[0][0];
            p.cards[0].Remove(c);

            p.cards.Add(new List<Card>());
            p.cards[1].Add(c);

            GrabCardFromDeck(p.cards[0], false);
            GrabCardFromDeck(p.cards[1], false);

            p.isHitting.Add(true);

            p.bank -= p.currBet;
        }

        public void TakeTurn(int player, bool[] isHitting)
        {
            if (player != currPlayer)
            {
                MessageBox.Show("Invalid Player playing!");
                return;
            }
            if (roundOver)
            {
                MessageBox.Show("Round is over! Ante up to begin!");
                return;
            }

            Player p = players[player];

            p.canSplit = false;

            for (int i = 0; i < p.cards.Count; i++)
            {
                var hand = p.cards[i];

                if (isHitting[i])
                {
                    GrabCardFromDeck(hand, false);

                    p.sum = GetSum(hand);
                    var possibleSum = p.sum;
                    if (possibleSum > 21)
                    {
                        p.isHitting[i] = false;

                    }
                    if (hand.Count >= 5)
                    { // 5 card charlie
                        p.isHitting[i] = false;
                    }
                }
                else
                {
                    p.isHitting[i] = false;
                }
            }

            players[currPlayer].cards[0][0].IsFlipped = true;
            // Find Next Player who is Hitting...
            if (players.Exists((x) => x.isHitting.Exists((y) => y)))
            {
                while (true)
                {
                    currPlayer++;
                    if (currPlayer >= players.Count)
                    {
                        TakeHouseTurn();
                        currPlayer = 0;
                    }

                    if (players[currPlayer].isHitting.Exists((y) => y))
                    {
                        players[currPlayer].cards[0][0].IsFlipped = false;
                        break;
                    }
                }
            }
            else
            {
                // If no one is hitting, End Round...
                while (TakeHouseTurn()) ;
                EndRound();
                return;
            }
        }

        public bool TakeHouseTurn()
        {
            if (GetSum(houseCards) < 17)
            {
                GrabCardFromDeck(houseCards, false);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Takes a card from the "Deck" pile and adds it to the player's Hand
        /// </summary>
        /// <param name="player"></param>
        public void GrabCardFromDeck(List<Card> cards, bool isHidden)
        {
            Card c = deck.GetCard();
            c.IsFlipped = isHidden;
            deck.RemoveCard(c);
            cards.Add(c);
        }

        public int GetSum(List<Card> cards)
        {
            List<int> possibleSums = new List<int>();
            possibleSums.Add(0);
            for (int i = 0; i < cards.Count; i++)
            {
                Card c = cards[i];
                if (c.Rank == 11 || c.Rank == 12 || c.Rank == 13)
                {
                    for (int y = 0; y < possibleSums.Count; y++) {
                        possibleSums[y] += 10;
                    }
                }
                else if (c.Rank == 1)
                {
                    int count = possibleSums.Count;
                    for (int y = 0; y < count; y++)
                    {
                        possibleSums[y] += 1;

                        possibleSums.Add(possibleSums[y] + 10);
                    }
                }
                else
                {
                    for (int y = 0; y < possibleSums.Count; y++) {
                        possibleSums[y] += c.Rank;
                    }
                }
            }

            var validSums = possibleSums.Where((x) => x <= 21);
            return validSums.Count() > 0 ? validSums.Max() : possibleSums.Max();
        }

        public override void Load(string path)
        {
            BinaryFormatter format = new BinaryFormatter();
            BlackjackGame state = (BlackjackGame)format.Deserialize(File.Open(path, FileMode.Open));

            deck = state.deck;
            houseCards = state.houseCards;
            players = state.players;
            currPlayer = state.currPlayer;
            roundOver = state.roundOver;
        }
        public override void Save(string path)
        {
            BinaryFormatter format = new BinaryFormatter();
            format.Serialize(File.Open(Path.GetFullPath(path), FileMode.OpenOrCreate), this);
        }

        public Player GetPlayer(int index)
        {
            return players[index];
        }
        public int GetCurrentPlayer()
        {
            return currPlayer;
        }
        public List<Card> GetHouseCards()
        {
            return houseCards;
        }
    }
}
