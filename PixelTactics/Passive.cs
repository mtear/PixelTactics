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
* Passive.cs
* Author: Nic Wilson
* Last updated: 3/28/2016
*/

using System;

namespace Tactics_CoreGameEngine
{
	public abstract class Passive : Ability
	{

		public int Priority = 0;

		public Passive (string name, string description, int priority)
			: base(name, description)
		{
			this.Priority = priority;
		}

		public abstract int ModifyDamage (int damage, Character target, Character user, Player PLAYER);

		public abstract int ModifyLife (int life, Character target, Character user, Player PLAYER);

		public abstract bool ModifyAttackType (bool melee, Character target, Character user, Player PLAYER);

		public abstract bool RevealYourHand (bool reveal, Character user, Player PLAYER);

		public abstract bool RevealEnemyHand (bool reveal, Character user, Player PLAYER);

		public abstract bool ModifyIntercept (bool intercept, Character target, Character user, Player PLAYER);

		public abstract bool ModifyRooted (bool rooted, Character target, Character user, Player PLAYER);

		public abstract bool ModifyOverkill (bool overkill, Character target, Character user, Player PLAYER);

		public abstract int ModifyArmor (int armor, Character target, Character user, Player PLAYER);

		public abstract bool ModifyZombie (bool zombie, Character target, Character user, Player PLAYER);

		public abstract int ModifyDamageToX (int damage, Character user, Character attacker, Character defender);

		public abstract int ModifyMaxHandSize (int handsize, Character user, Player PLAYER);

		public abstract bool ModifyTargetable (bool targetable, Character target, Character user, Player PLAYER);

		public abstract bool ModifyPlayerTargetable (bool targetable, Character user, Player PLAYER);

	} // End Passive class

	public class PassivePair{

		public Passive p;
		public Character c;

		public PassivePair(Passive p, Character c){
			this.p = p;
			this.c = c;
		}

	} // End PassivePair class

} // End namespace

