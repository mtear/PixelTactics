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
 * PassiveAbilities.cs
 * Author: Nic Wilson
 * Last updated: 3/26/2016
 */

using System;

namespace PixelTactics
{

/**********************************************************************************
**********************************************************************************
* 
* 										NOTE
* 
* 		THIS FILE IS A COLLECTION OF PASSIVE ABILITIES FOR CHARACTER EFFECTS
* 
**********************************************************************************
**********************************************************************************/

	/// <summary>
	/// Template class.
	/// </summary>
	public class TFP : Passive{
		public TFP() : base("TFP", "", 0){}

		public override int ModifyDamage (int damage, Character target, Character user, Player PLAYER){
			return damage;
		}

		public override int ModifyLife (int life, Character target, Character user, Player PLAYER){
			return life;
		}

		public override bool ModifyAttackType (bool melee, Character target, Character user, Player PLAYER){
			return melee;
		}

		public override bool RevealYourHand (bool reveal, Character user, Player PLAYER){
			return reveal;
		}

		public override bool RevealEnemyHand (bool reveal, Character user, Player PLAYER){
			return reveal;
		}

		public override bool ModifyIntercept (bool intercept, Character target, Character user, Player PLAYER){
			return intercept;
		}

		public override bool ModifyRooted (bool rooted, Character target, Character user, Player PLAYER){
			return rooted;
		}

		public override bool ModifyOverkill (bool overkill, Character target, Character user, Player PLAYER){
			return overkill;
		}

		public override int ModifyArmor (int armor, Character target, Character user, Player PLAYER){
			return armor;
		}

		public override bool ModifyZombie (bool zombie, Character target, Character user, Player PLAYER){
			return zombie;
		}
	}

	public class TestPassive1 : Passive //Give overkill
	{
		public TestPassive1 () : base("PA000001", "", 0)
		{
		}

		public override int ModifyDamage (int damage, Character target, Character user, Player PLAYER){
			return damage;
		}

		public override int ModifyLife (int life, Character target, Character user, Player PLAYER){
			return life;
		}

		public override bool ModifyAttackType (bool melee, Character target, Character user, Player PLAYER){
			return melee;
		}

		public override bool RevealYourHand (bool reveal, Character user, Player PLAYER){
			return reveal;
		}

		public override bool RevealEnemyHand (bool reveal, Character user, Player PLAYER){
			return reveal;
		}

		public override bool ModifyIntercept (bool intercept, Character target, Character user, Player PLAYER){
			return intercept;
		}

		public override bool ModifyRooted (bool rooted, Character target, Character user, Player PLAYER){
			return rooted;
		}

		public override bool ModifyOverkill (bool overkill, Character target, Character user, Player PLAYER){
			return overkill;
		}

		public override int ModifyArmor (int armor, Character target, Character user, Player PLAYER){
			if(target == user) return armor+2;
			return armor;
		}

		public override bool ModifyZombie (bool zombie, Character target, Character user, Player PLAYER){
			return zombie;
		}
	}

} //End namespace