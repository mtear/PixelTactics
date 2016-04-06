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
using UnityEngine;

namespace Tactics_CoreGameEngine
{

/**********************************************************************************
**********************************************************************************
* 
* 					NOTE
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

	public class AA1 : Active{ //Draw a card
		public AA1 () : base ("TA1", "AA000001"){}

		public override void Execute(Character target, Player PLAYER){
			PLAYER.DrawCard ();
		}
	}

	public class AA2 : Active{ //Each player discards a card. Draw a card
		public AA2 () : base ("TA1", "AA000002"){}

		public override void Execute(Character target, Player PLAYER){
			PLAYER.HAND.DiscardRandomCard (PLAYER.GRAVEYARD);
			PLAYER.ENEMY.HAND.DiscardRandomCard (PLAYER.ENEMY.GRAVEYARD);
			PLAYER.DrawCard ();
		}
	}

	public class AA3 : Active{
		public AA3 () : base ("TA1", "AA000003"){
			this.RequestsTarget = true;
		}

		public override void Execute(Character target, Player PLAYER){
			Debug.Log ("HEY");
			TARGET.CTARGET.AddDamage (new Damage (Damage.TYPE.SPELL, 2, null));
		}
	}

} //End namespace

