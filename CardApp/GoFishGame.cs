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
        public Deck deck;

        [Serializable]
        public class Player {
			public string name;
            public Deck Hand = new Deck();
            public Deck Pairs = new Deck();
        }

        Player[] players;
        int currPlayer;
        public bool hasAsked;
        public bool hasFished;

        List<Label> playerNamesLabels;
        List<Label> playerScoresLabels;
        List<StackPanel> nonActivePlayerHands;

        private Card selectedCard;

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
            playerNamesLabels = new List<Label>() { page.lblPlayer1Name, page.lblPlayer2Name, page.lblPlayer3Name };
            playerScoresLabels = new List<Label>() { page.lblPlayer1Score, page.lblPlayer2Score, page.lblPlayer3Score };
            nonActivePlayerHands = new List<StackPanel>() { page.Player1Hand, page.Player2Hand, page.Player3Hand };
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

                    Card possiblePair = players[y].Hand.cards.Find((x) => x.Rank == playerCard.Rank);
                    if (possiblePair != null) {
                        players[y].Hand.RemoveCard(possiblePair);
                        players[y].Pairs.AddCard(possiblePair);
                        players[y].Pairs.AddCard(playerCard);
                    } else {
                        players[y].Hand.AddCard(playerCard);
                    }
                }
            }
            VisualizePlayers();
            type = GameType.GoFish;
        }

        public void NextPlayerTakeTurn()
        {
            if (hasAsked && (hasFished || deck.cards.Count == 0))
            {
                hasFished = false;
                hasAsked = false;
                int cardsHeld = 0;
                foreach (Player player in players)
                {
                    cardsHeld += player.Hand.cards.Count;
                }
                if (cardsHeld == 0)
                {
                    Exit();
                } else
                {
                    do
                    {
                        if (++currPlayer >= players.Length)
                        {
                            currPlayer = 0;
                        }
                    } while (players[currPlayer].Hand.cards.Count == 0);
                    VisualizePlayers();
                }
                page.lblInstructions.Content = "Select a card rank to ask for";
            }
        }

        private void DrawCardsIfHandEmpty(Player player)
        {
            if (player.Hand.cards.Count == 0)
            {
                for (int i = 0; i < CARDS_RECIEVED_WHEN_EMPTY; i++)
                {
                    GrabCardFromDeck(player);
                }
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
            Card found = playerTaken.Hand.cards.Find(x => x.Rank == card.Rank);
            if (found != null) {
                playerTaken.Hand.RemoveCard(found);
                playerTaking.Hand.AddCard(found);
                CheckForPairs(playerTaking, found);
                DrawCardsIfHandEmpty(playerTaking);
                DrawCardsIfHandEmpty(playerTaken);
                return true;
            }
            return false;
        }

        public void CurrentPlayerGoFish()
        {
            if (!hasFished)
            {
                GrabCardFromDeck(players[currPlayer]);
                hasFished = true;
            }
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
        public bool GrabCardFromDeck(Player player)
        {
            if (deck.cards.Count > 0)
            {
                Card c = deck.GetCard(deck.cards.Count-1);
                deck.RemoveCard(deck.cards.Count - 1);
                player.Hand.AddCard(c);
                CheckForPairs(player, c);
                return true;
            }
            return false;
        }

        public void HideActivePlayerCards()
        {
            foreach (Card card in players[currPlayer].Hand.cards)
            {
                VisualizeActivePlayer(!card.IsFlipped);
                break;
            }
        }

        /// <summary>
        /// Checks if the player has a pair using the Given Cards Rank
        /// <para></para>
        /// If so, removes from player hand and adds to their pairs...
        /// </summary>
        /// <param name="player"></param>
        /// <param name="cardGained"></param>
        public void CheckForPairs(Player player, Card cardGained) {
            var cards = player.Hand.cards.Where((x) => x.Rank == cardGained.Rank).ToList();
            if (cards.Count >= 2)
            {
                foreach (Card pair in cards)
                {
                    player.Hand.RemoveCard(pair);
                    player.Pairs.AddCard(pair);
                }
            }
            VisualizePlayers();
        }

        public void VisualizePlayers()
        {
            page.lblDeckSize.Content = deck.cards.Count;
            VisualizeActivePlayer();
            int loopIndex = 0;
            int activeLane = 0;
            foreach (Player player in players)
            {
                if (currPlayer != loopIndex)
                {
                    VisualizePlayer(player, playerNamesLabels[activeLane], playerScoresLabels[activeLane], nonActivePlayerHands[activeLane]);
                    activeLane++;
                }
                loopIndex++;
            }
        }

        public void VisualizeActivePlayer(bool cardsFlipped = false)
        {
            page.ActiveHand.Children.Clear();
            page.lblActivePlayerName.Content = players[currPlayer].name;
            page.lblActivePlayerScore.Content = players[currPlayer].Pairs.cards.Count/2 + " pairs";
            Deck hand = players[currPlayer].Hand;
            foreach (Card card in hand.cards)
            {
                card.IsFlipped = cardsFlipped;
                Image image = CardImageCreator.VisualizeCard(card, page.ActiveHand);
                image.Margin = new Thickness(-hand.cards.Count * 4, 0, 0, 0);
                image.MouseDown += IndicateCardSelection;
            }
        }

        public void VisualizePlayer(Player player, Label name, Label score, Panel hold)
        {
            hold.Children.Clear();
            name.Content = player.name;
            score.Content = player.Pairs.cards.Count/2 + " pairs";
            foreach (Card card in player.Hand.cards)
            {
                card.IsFlipped = true;
                Image image = CardImageCreator.VisualizeCard(card, hold);
                image.Margin = new Thickness(-20, 0, 0, 0);
                image.MouseDown += CardAsk;
            }
        }

        public void IndicateCardSelection(object sender, RoutedEventArgs e)
        {
            if (!hasAsked)
            {
                Card card = (Card)((Image)sender).DataContext;
                Thickness margin = ((Image)sender).Margin;
                if (selectedCard != null)
                {
                    margin.Top = 0;
                    selectedCard.image.Margin = margin;
                }
                if (card != selectedCard)
                {
                    margin.Top = -20;
                    ((Image)sender).Margin = margin;
                    selectedCard = card;
                } else
                {
                    selectedCard = null;
                }
            }
        }

        public void CardAsk(object sender, RoutedEventArgs e)
        {
            if (!hasAsked)
            {
                hasAsked = true;
                Card askCard = (Card)((Image)sender).DataContext;
                if (selectedCard != null)
                {
                    foreach (Player player in players)
                    {
                        if (player != players[currPlayer] && CheckPlayerCard(player, askCard))
                        {
                            bool tookCard = TakePlayerCard(players[currPlayer], player, selectedCard);
                            if (tookCard)
                            {
                                page.lblInstructions.Content = "You took a card, go again!";
                                hasAsked = false;
                            } else
                            {
                                page.lblInstructions.Content = "Go fish";
                                selectedCard = null;
                                VisualizePlayers();
                            }
                            break;
                        }
                    }
                }
            }
        }

    }
}

// Requirements - 
// End Game
// Win Detection
// Names
