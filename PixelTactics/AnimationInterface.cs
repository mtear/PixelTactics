using System;

namespace Tactics_CoreGameEngine
{
	public class AnimationInterface
	{

		private TableTop TABLE;

		public AnimationInterface ()
		{
		}

		public void SetTable(TableTop TABLE){
			this.TABLE = TABLE;
		}

		public virtual void SwitchTurnBreak(){
			TABLE.SwitchTurnContinue ();
		}

	}
}

