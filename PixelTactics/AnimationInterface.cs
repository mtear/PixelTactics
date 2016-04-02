using System;
using System.Collections.Generic;

namespace Tactics_CoreGameEngine
{
	public class AnimationInterface
	{

		public TableTop TABLE;

		public AnimationInterface ()
		{
		}

		public void SetTable(TableTop TABLE){
			this.TABLE = TABLE;
		}

		public virtual void SwitchTurnBreak(){
			TABLE.SwitchTurnContinue ();
		}

		public virtual void AttackBreak(Character a, Character t, List<Character> Units){
			TABLE.AttackContinue (Units);
		}

	}
}

