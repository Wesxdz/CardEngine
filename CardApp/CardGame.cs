using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardEngine
{
    abstract class CardGame
    {
        public abstract void Start();
        public abstract void Exit();
        public abstract void Save(string path);
        public abstract void Load(string path);
    }
}
