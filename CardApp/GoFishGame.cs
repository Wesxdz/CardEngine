using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardEngine {
    /*public */class GoFishGame : CardGame {

        Deck deck;
        // Players 2 - 4
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
        }
    }
}
