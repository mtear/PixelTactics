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
 * ActiveAbilities.cs
 * Author: Nic Wilson
 * Last updated: 3/26/2016
 */

using System;

namespace Tactics_CoreGameEngine
{

/**********************************************************************************
**********************************************************************************
* 
* 										NOTE
* 
* 		THIS FILE IS A COLLECTION OF ACTIVE ABILITIES FOR CHARACTER HAND EFFECTS
* 
**********************************************************************************
**********************************************************************************/

	/// <summary>
	/// Template class.
	/// </summary>
	public class TPA : Active{
		public TPA () : base ("TA1", ""){}

		public override void Execute(Character target, Player PLAYER){

		}
	}

	public class TestActive1 : Active{ //Do 1 to all enemies
		public TestActive1 () : base ("TA1", ""){}

		public override void Execute(Character target, Player PLAYER){
			for (int i = 0; i < 3; i++) {
				for (int a = 0; a < 3; a++) {
					if(PLAYER.ENEMY.GAMEBOARD.BOARD[a,i] != null)
						PLAYER.ENEMY.GAMEBOARD.BOARD [a, i].AddDamage (new Damage(Damage.TYPE.SPELL, 1, null));
				}
			}
		}
	}

	public class TestActive2 : Active{ //Enemy discards 2 cards
		public TestActive2 () : base ("TA2", ""){}

		public override void Execute(Character target, Player PLAYER){
			PLAYER.ENEMY.HAND.DiscardRandom (PLAYER.ENEMY.GRAVEYARD);
			PLAYER.ENEMY.HAND.DiscardRandom (PLAYER.ENEMY.GRAVEYARD);
		}
	}

	public class TestActive3 : Active{ //lightning bolt
		public TestActive3 () : base ("TA3", ""){this.RequestsTarget = true;}

		public override void Execute(Character target, Player PLAYER){
			if (TARGET.TARGETPLAYER) {
				TARGET.PTARGET.AddDamage (new Damage (Damage.TYPE.SPELL, 3, target));
			} else {
				TARGET.CTARGET.AddDamage (new Damage (Damage.TYPE.SPELL, 3, target));
			}
		}

		protected override bool ValidTarget(Target TARGET){
			return !TARGET.TARGETPLAYER;
		}
	}

} //End namespace

