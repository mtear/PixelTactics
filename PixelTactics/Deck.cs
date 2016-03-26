using System;
using System.Collections.Generic;

namespace PixelTactics
{
	public class Deck
	{

		private List<Character> deck;

		public int Count {
			get {
				return deck.Count;
			}
		}

		public Deck (Player P)
		{
			deck = new List<Character> ();
			Setup (P);
		}

		public List<Character> Contents(){
			return deck;
		}

		public void Setup(Player P){
			deck.Add(new UC1(P));
			deck.Add(new UC1(P));
			deck.Add(new UC1(P));
			deck.Add(new UC3(P));
			deck.Add(new UC3(P));
			deck.Add(new UC2(P));
			deck.Add(new UC2(P));
			deck.Add(new UC2(P));
			deck.Add(new UC2(P));
			deck.Add(new UC3(P));
			deck.Add(new UC3(P));
			Shuffle ();
		}

		public Character Draw(){
			if (deck.Count == 0)
				return null;
			else {
				Character c = deck [0];
				deck.RemoveAt (0);
				return c;
			}
		}

		public void Shuffle(){
			List<Character> temp = new List<Character> ();
			while (deck.Count > 0) {
				int i = Utility.R.Next (deck.Count);
				temp.Add (deck [i]);
				deck.RemoveAt (i);
			}
			deck = temp;
		}
			
	}
}

