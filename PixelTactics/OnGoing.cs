using System;
using System.Collections.Generic;

namespace PixelTactics
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
	}
}

