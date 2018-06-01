using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace CardApp
{
	[Serializable]
	class WarGame : Game
	{
		[Serializable]
		public class Player
		{
			public string name;
			public bool isComputer = false;
			public Queue<Card> cards = new Queue<Card>();
			public List<Card> faceUp = new List<Card>();
			public List<Card> faceDown = new List<Card>();
		}

		public Player p1 = new Player();
		public Player p2 = new Player();

		/// <summary>
		/// Moves a card from the players hand to the faceUp position
		/// </summary>
		/// <param name="playerIndex">Index of the player, always 1 or 2</param>
		public void PlayCard(int playerIndex)
		{
			Player p = GetPlayer(playerIndex);
			Player op = GetPlayer(playerIndex % 2 + 1);
			if (p == null || op == null) return;
			if (p.faceUp.Count != 0 && op.faceUp.Count != 0)
			{
				if (p.faceUp.Last().Rank < op.faceUp.Last().Rank)
				{
					op.faceUp.ForEach(c => op.cards.Enqueue(c));
					op.faceUp.Clear();
					p.faceUp.ForEach(c => op.cards.Enqueue(c));
					p.faceUp.Clear();
					op.faceDown.ForEach(c => op.cards.Enqueue(c));
					op.faceDown.Clear();
					p.faceDown.ForEach(c => op.cards.Enqueue(c));
					p.faceDown.Clear();
				}
				else if (p.faceUp.Last().Rank > op.faceUp.Last().Rank)
				{
					op.faceUp.ForEach(c => p.cards.Enqueue(c));
					op.faceUp.Clear();
					p.faceUp.ForEach(c => p.cards.Enqueue(c));
					p.faceUp.Clear();
					op.faceDown.ForEach(c => p.cards.Enqueue(c));
					op.faceDown.Clear();
					p.faceDown.ForEach(c => p.cards.Enqueue(c));
					p.faceDown.Clear();
				}
				else
				{
					if (p.cards.Count() > 0)
					{
						p.faceDown.Add(p.cards.Dequeue());
						if (p.cards.Count() > 0)
						{
							p.faceDown.Add(p.cards.Dequeue());
							if (p.cards.Count() > 0) p.faceDown.Add(p.cards.Dequeue());
						}
					}
					if (op.cards.Count() > 0)
					{
						op.faceDown.Add(op.cards.Dequeue());
						if (op.cards.Count() > 0)
						{
							op.faceDown.Add(op.cards.Dequeue());
							if (op.cards.Count() > 0) op.faceDown.Add(op.cards.Dequeue());
						}
					}
				}
			}

			p.faceUp.Add(p.cards.Dequeue());
			if (op.isComputer) op.faceUp.Add(op.cards.Dequeue());
		}

		public string GetAnnounceText()
		{
			if (p1.faceUp.Count == 0 || p2.faceUp.Count == 0) return "";
			if (p1.faceUp.Last().Rank < p2.faceUp.Last().Rank)
			{
				if (p1.cards.Count == 0) return p2.name + " won the game!";
				return p2.name + " won the round";
			}
			else if (p1.faceUp.Last().Rank > p2.faceUp.Last().Rank)
			{
				if (p2.cards.Count == 0) return p1.name + " won the game!";
				return p1.name + " won the round";
			}
			else return "WAR!";
		}
		public Player GetPlayer(int playerIndex)
		{
			switch (playerIndex)
			{
				case 1:
					return p1;
				case 2:
					return p2;
				default:
					return null;
			}
		}
		public override void Start()
		{
			p1.cards.Clear();
			p2.cards.Clear();
			p1.faceUp.Clear();
			p2.faceUp.Clear();
			p1.faceDown.Clear();
			p2.faceDown.Clear();
			Deck stdDeck = Deck.CreateStandardDeck();
			stdDeck.Shuffle();
			for (int i = 0; i < 26; ++i) p1.cards.Enqueue(stdDeck.GetCard(i));
			for (int i = 26; i < 52; ++i) p2.cards.Enqueue(stdDeck.GetCard(i));
		}

		public override void Exit()	{ }
		public override void Save(string path)
		{
			BinaryFormatter format = new BinaryFormatter();
			format.Serialize(File.Open(Path.GetFullPath(path), FileMode.OpenOrCreate), this);
		}
		public override void Load(string path)
		{
			BinaryFormatter format = new BinaryFormatter();
			WarGame state = (WarGame)format.Deserialize(File.Open(path, FileMode.Open));
			p1 = state.p1;
			p2 = state.p2;
		}

		public override void InitializePlayers(int playerAmo, string[] names, bool[] areHuman)
		{
			if (playerAmo != 2) throw new ArgumentOutOfRangeException("playerAmo", "playerAmo must be 2");
			p1.name = names[0];
			p2.name = names[1];
			p1.isComputer = areHuman[0];
			p2.isComputer = areHuman[1];
		}
	}
}
