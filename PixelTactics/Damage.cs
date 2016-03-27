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
* Damage.cs
* Author: Nic Wilson
* Last updated: 3/27/2016
*/

using System;

namespace Tactics_CoreGameEngine
{
	public class Damage
	{

		public enum TYPE{
			MELEE, RANGE, SPELL, HEAL, STUN
		}

		public TYPE type;
		public Character SOURCE;
		public int VALUE;

		public Damage (Damage.TYPE type, int VALUE, Character SOURCE)
		{
			this.type = type;
			this.VALUE = VALUE;
			this.SOURCE = SOURCE;
		}
	} // End Damage class

} // End namespace

