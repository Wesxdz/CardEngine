using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace CardApp {
    [Serializable]
    public class GoFishGame : Game {

        public GoFishPage page;
		public static int PLAYER_AMO = 2;
        public static int STARTING_CARDS = 5;
        public static int CARDS_RECIEVED_WHEN_EMPTY = 5;
        Deck deck;

        [Serializable]
        public class Player {
			public string name;
            public Deck Hand = new Deck();
            public Deck Pairs = new Deck();
        }

        Player[] players;
        int currPlayer;

        //IEnumerable<int> placements;
        bool gameHasEnded = false;

        public override void InitializePlayers(int playerAmo, string[] names, bool[] areHuman) {
            PLAYER_AMO = playerAmo;
            players = new Player[PLAYER_AMO];

            for (int i = 0; i < players.Length; i++) {
                players[i] = new Player();
                players[i].name = names[i];
                // Computers aren't allowed in Go Fish
            }
        }

        public override void Exit() {
            KeyValuePair<int, int>[] pairs = new KeyValuePair<int, int>[PLAYER_AMO];

            for (int i = 0; i < PLAYER_AMO; i++) {
                KeyValuePair<int, int> pair = new KeyValuePair<int, int>(
                    i,
                    players[i].Pairs.cards.Count / 2
                    );
                pairs[i] = pair;
            }

            Array.Sort(pairs, (x, y) => x.Value.CompareTo(y.Value));

            string winText = "Game has ended!\n";
            for (int i = 0; i < PLAYER_AMO; i++) {
                var pair = pairs[i];
                winText += (i + 1) + ".) " + players[pair.Key].name + ": " + pair.Value;
            }
            MessageBox.Show(winText);

            gameHasEnded = true;
        }

        public override void Load(string path)
        {
            BinaryFormatter format = new BinaryFormatter();
            GoFishGame state = (GoFishGame)format.Deserialize(File.Open(path, FileMode.Open));
            page = state.page;
            deck = state.deck;
            players = state.players;
            currPlayer = state.currPlayer;
            gameHasEnded = state.gameHasEnded;
        }

        public override void Save(string path)
        {
            BinaryFormatter format = new BinaryFormatter();
            format.Serialize(File.Open(Path.GetFullPath(path), FileMode.OpenOrCreate), this);
        }

        public override void Start() {
            deck = Deck.CreateStandardDeck();
            deck.Shuffle();

            currPlayer = 0;
            gameHasEnded = false;

            STARTING_CARDS = PLAYER_AMO == 2 ? 7 : 5;
            for (int i = 0; i < STARTING_CARDS; i++) {
                for(int y = 0; y < players.Length; y++) {
                    Card playerCard = deck.GetCard(deck.cards.Count - 1);
                    deck.RemoveCard(deck.cards.Count - 1);

                    //Card possiblPair = players[y].Hand.cards.Find((x) => x.Rank == playerCard.Rank);
                    //if (possiblPair != null) {
                    //    players[y].Hand.RemoveCard(possiblPair);
                    //    players[y].Pairs.AddCard(possiblPair);
                    //    players[y].Pairs.AddCard(playerCard);
                    //} else {
                    //}
                    players[y].Hand.AddCard(playerCard);
                }
            }
            VisualizeActivePlayer();
            type = GameType.GoFish;
        }

        public void TakePlayerTurn(int playerTurn, int playerToTake, Card chosenCard) {
            if (gameHasEnded) {
                MessageBox.Show("Game has ended!");
                return;
            }

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
                if(players[playerTurn].Hand.cards.Count == 0) {
                    for (int i = 0; i < CARDS_RECIEVED_WHEN_EMPTY; i++) {
                        GrabCardFromDeck(players[playerTurn]);
                        if (deck.cards.Count == 0) { Exit(); return; }
                    }
                }
            } else { // If it didn't work...
                GrabCardFromDeck(players[playerTurn]);

                // Check for End of Game...
                if (deck.cards.Count == 0) { Exit(); return; }
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
            deck.RemoveCard(deck.cards.Count - 1);
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

        public void VisualizeActivePlayer()
        {
            Deck hand = players[currPlayer].Hand;
            foreach (Card card in hand.cards)
            {
                Image image = CardImageCreator.VisualizeCard(card, page.ActiveHand);
                image.Margin = new Thickness(-hand.cards.Count * 4, 0, 0, 0);
                image.MouseDown += FlipCard;
            }
        }

        public void FlipCard(object sender, RoutedEventArgs e)
        {
            Card card = (Card)((Image)sender).DataContext;
            card.IsFlipped = !card.IsFlipped;
        }
    }
}

// Requirements - 
// End Game
// Win Detection
// Names
