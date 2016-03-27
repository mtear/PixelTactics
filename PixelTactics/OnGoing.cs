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
* OnGoing.cs
* Author: Nic Wilson
* Last updated: 3/27/2016
*/

using System;
using System.Collections.Generic;

namespace Tactics_CoreGameEngine
{
	public abstract class OnGoing : Ability
	{

		public int Timer = 0;
		public List<Trigger> Triggers;
		public List<Passive> Passives;

		public bool Expired {
			get {
				return Timer <= 0;
			}
		}

		public OnGoing (string name, string description, int Timer) : base (name, description)
		{
			this.Timer = Timer;
			this.Triggers = new List<Trigger> ();
			this.Passives = new List<Passive> ();
		}

		public void Decrement(){
			Timer--;
		}

		public abstract void Discard (Player P);
	} // End OnGoing class

} // End namespace

