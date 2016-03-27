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
* Deck.cs
* Author: Nic Wilson
* Last updated: 3/27/2016
*/

using System;
using System.Collections.Generic;

namespace Tactics_CoreGameEngine
{
	public class Deck
	{

		private List<Character> deck;

		public int Count {
			get {
				return deck.Count;
			}
		}

		public Deck (List<Character> deck)
		{
			this.deck = deck;
			Shuffle ();
		}

		public List<Character> Contents(){
			return deck;
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
			
	} // End Deck class

} // End namespace

