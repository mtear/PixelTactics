/*************************************************************************
 * 
 * FELICITY CONFIDENTIAL
 * __________________
 * 
 *  [2016] - [2016] Felicity Entertainment
 *  All Rights Reserved.
 * 
 * NOTICE:  All information contained herein is, and remains
 * the property of Felicity Entertainment and its suppliers,
 * if any.  The intellectual and technical concepts contained
 * herein are proprietary to Felicity Entertainment and its 
 * suppliers and may be covered by U.S. and Foreign Patents,
 * patents in process, and are protected by trade secret or 
 * copyright law. Dissemination of this information or reproduction
 * of this material is strictly forbidden unless prior written
 * permission is obtained from Felicity Entertainment.
 */

/*
* Hand.cs
* Author: Nic Wilson
* Last updated: 3/27/2016
*/

using System;
using System.Collections.Generic;

namespace Tactics_CoreGameEngine
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
	} // End Hand class

} // End namespace

