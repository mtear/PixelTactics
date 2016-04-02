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
 * Last updated: 3/28/2016
 */

using System;

namespace Tactics_CoreGameEngine
{

/**********************************************************************************
**********************************************************************************
* 
* 					NOTE
* 
* 		THIS FILE IS A COLLECTION OF PASSIVE ABILITIES FOR CHARACTER EFFECTS
* 
**********************************************************************************
**********************************************************************************/

	/// <summary>
	/// Template class.
	/// </summary>
	public class TFP : Passive{
		public TFP() : base("", 0){}

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

		public override int ModifyDamageToX (int damage, Character user, Character attacker, Character defender){
			return damage;
		}

		public override int ModifyMaxHandSize (int handsize, Character user, Player PLAYER){
			return handsize;
		}

		public override bool ModifyTargetable (bool targetable, Character target, Character user, Player PLAYER){
			return targetable;
		}

		public override bool ModifyPlayerTargetable (bool targetable, Character user, Player PLAYER){
			return targetable;
		}
	}

	public class TestPassive1 : Passive //Give Intercept
	{
		public TestPassive1 () : base("PAD000001", 0)
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
			if (user == target)
				return true;
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

		public override int ModifyDamageToX (int damage, Character user, Character attacker, Character defender){
			return damage;
		}

		public override int ModifyMaxHandSize (int handsize, Character user, Player PLAYER){
			return handsize;
		}

		public override bool ModifyTargetable (bool targetable, Character target, Character user, Player PLAYER){
			return targetable;
		}

		public override bool ModifyPlayerTargetable (bool targetable, Character user, Player PLAYER){
			return targetable;
		}
	}

	public class TestPassive2 : Passive //Give Intercept, Life + 1
	{
		public TestPassive2 () : base("PAD000002", 0)
		{
		}

		public override int ModifyDamage (int damage, Character target, Character user, Player PLAYER){
			return damage;
		}

		public override int ModifyLife (int life, Character target, Character user, Player PLAYER){
			if (target == user)
				return life + 1;
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
			if (target == user)
				return true;
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

		public override int ModifyDamageToX (int damage, Character user, Character attacker, Character defender){
			return damage;
		}

		public override int ModifyMaxHandSize (int handsize, Character user, Player PLAYER){
			return handsize;
		}

		public override bool ModifyTargetable (bool targetable, Character target, Character user, Player PLAYER){
			return targetable;
		}

		public override bool ModifyPlayerTargetable (bool targetable, Character user, Player PLAYER){
			return targetable;
		}
	}

	public class TestPassive3 : Passive //Give attack + 2
	{
		public TestPassive3 () : base("PAD000003", 0)
		{
		}

		public override int ModifyDamage (int damage, Character target, Character user, Player PLAYER){
			if (target == user)
				return damage + 2;
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

		public override int ModifyDamageToX (int damage, Character user, Character attacker, Character defender){
			return damage;
		}

		public override int ModifyMaxHandSize (int handsize, Character user, Player PLAYER){
			return handsize;
		}

		public override bool ModifyTargetable (bool targetable, Character target, Character user, Player PLAYER){
			return targetable;
		}

		public override bool ModifyPlayerTargetable (bool targetable, Character user, Player PLAYER){
			return targetable;
		}
	}

	public class TestPassive4 : Passive //Give life + 2
	{
		public TestPassive4 () : base("PAD000004", 0)
		{
		}

		public override int ModifyDamage (int damage, Character target, Character user, Player PLAYER){
			return damage;
		}

		public override int ModifyLife (int life, Character target, Character user, Player PLAYER){
			if (user == target)
				return life + 2;
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

		public override int ModifyDamageToX (int damage, Character user, Character attacker, Character defender){
			return damage;
		}

		public override int ModifyMaxHandSize (int handsize, Character user, Player PLAYER){
			return handsize;
		}

		public override bool ModifyTargetable (bool targetable, Character target, Character user, Player PLAYER){
			return targetable;
		}

		public override bool ModifyPlayerTargetable (bool targetable, Character user, Player PLAYER){
			return targetable;
		}
	}

	public class TestPassive5 : Passive //Give Overkill
	{
		public TestPassive5 () : base("PAD000005", 0)
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
			if (target == user)
				return true;
			return overkill;
		}

		public override int ModifyArmor (int armor, Character target, Character user, Player PLAYER){
			return armor;
		}

		public override bool ModifyZombie (bool zombie, Character target, Character user, Player PLAYER){
			return zombie;
		}

		public override int ModifyDamageToX (int damage, Character user, Character attacker, Character defender){
			return damage;
		}

		public override int ModifyMaxHandSize (int handsize, Character user, Player PLAYER){
			return handsize;
		}

		public override bool ModifyTargetable (bool targetable, Character target, Character user, Player PLAYER){
			return targetable;
		}

		public override bool ModifyPlayerTargetable (bool targetable, Character user, Player PLAYER){
			return targetable;
		}
	}

	public class TestPassive6 : Passive //Give Rooted
	{
		public TestPassive6 () : base("PAD000006", 0)
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
			if (target == user)
				return true;
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

		public override int ModifyDamageToX (int damage, Character user, Character attacker, Character defender){
			return damage;
		}

		public override int ModifyMaxHandSize (int handsize, Character user, Player PLAYER){
			return handsize;
		}

		public override bool ModifyTargetable (bool targetable, Character target, Character user, Player PLAYER){
			return targetable;
		}

		public override bool ModifyPlayerTargetable (bool targetable, Character user, Player PLAYER){
			return targetable;
		}
	}

} //End namespace