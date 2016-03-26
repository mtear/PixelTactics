using System;
using System.Collections.Generic;

namespace PixelTactics
{
	public class Hand
	{

		private List<Character> hand;

		public Hand ()
		{
			hand = new List<Character> ();
		}

		public bool Draw(Player PLAYER){
			Character c = PLAYER.DECK.Draw ();
			if (c == null) {
				PLAYER.LoseFromDrawOut ();
			}
			if (PLAYER.HAND.GetHand().Count >= PLAYER.MaxHandSize) {
				PLAYER.GRAVEYARD.AddCard (c);
			}else hand.Add(c);
			return true;
		}

		public Character Get(int index){
			return hand [index];
		}

		public List<Character> GetHand(){
			return hand;
		}

		public void Remove(Character c){
			hand.Remove (c);
		}

		public void Discard(Character c, Hand d){
			c.ResetBaseStats ();
			hand.Remove (c);
			d.AddCard (c);
		}

		public void Discard(Character c){
			c.ResetBaseStats ();
			hand.Remove (c);
		}

		public int HandSize(){
			return hand.Count;
		}

		public bool AddCard(Character c){
			hand.Add (c);
			return true;
		}

		public Character RandomCard(){
			if (hand.Count == 0)
				return null;
			return hand [Utility.R.Next (hand.Count)];
		}

		public void DiscardRandom(Hand graveyard){
			Character c = RandomCard ();
			if (c != null)
				Discard (c, graveyard);
		}
	}
}

