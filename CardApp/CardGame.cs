using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardApp
{

    public enum GameType
    {
        War,
        GoFish,
        Blackjack
    }

    public abstract class Game
    {
        public GameType type;
        public abstract void Start();
        public abstract void Exit();
        public abstract void Save(string path);
        public abstract void Load(string path);
    }
}
