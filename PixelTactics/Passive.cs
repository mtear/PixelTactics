using System;

namespace PixelTactics
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
	}

	public class PassivePair{

		public Passive p;
		public Character c;

		public PassivePair(Passive p, Character c){
			this.p = p;
			this.c = c;
		}

	}
}

