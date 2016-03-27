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
* Command.cs
* Author: Nic Wilson
* Last updated: 3/27/2016
*/

using System;
using System.Collections.Generic;

namespace Tactics_CoreGameEngine
{
	public class Utility
	{

		public static Random R = new Random();

		public static List<Character> GetDebugStarterDeck(Player P){
			List<Character> deck = new List<Character> ();
			deck.Add (new UC1 (P));
			deck.Add (new UC1 (P));
			deck.Add (new UC1 (P));
			deck.Add (new UC1 (P));
			deck.Add (new UC2 (P));
			deck.Add (new UC2 (P));
			deck.Add (new UC2 (P));
			deck.Add (new UC3 (P));
			deck.Add (new UC3 (P));
			deck.Add (new UC3 (P));
			return deck;
		}


	} // End Utility class

	public class Point{
		public int x,y;
		public Point(int x, int y){
			this.x = x;
			this.y = y;
		}
	} // End Point class

} // End namespace

