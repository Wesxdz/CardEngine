using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardApp {
    class BlackjackGame : Game {

        public class Player {
            public string name;
            public List<Card> cards = new List<Card>();
            public int bank = 20;
        }

        //public static int PLAYER_AMO = 2;

        //Deck deck;

        //// House...
        //public List<Card> houseCards;

        //Player[] players;
        //List<int> playersHitting;
        //int currPlayer;

        //// Black Jack should be split into rounds
        //// Each Round, 

        //bool gameHasEnded = false;

        public override void Start() {

        }

        public override void InitializePlayers(int playerAmo, string[] names, bool[] areHuman) {
            //PLAYER_AMO = playerAmo;
            //players = new Player[PLAYER_AMO];

            //for (int i = 0; i < players.Length; i++) {
            //    players[i] = new Player();
            //    players[i].name = names[i];
            //    // Computers aren't allowed in BlackJack...
            //}
        }

        public override void Exit() {

        }

        public void StartRound() {

        }

        public void TakeTurn() {

        }

        public override void Load(string path) {
            throw new NotImplementedException();
        }
        public override void Save(string path) {
            throw new NotImplementedException();
        }
    }
}
