using System;

namespace PixelTactics
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
	}
}

