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
 * TriggerAbilities.cs
 * Author: Nic Wilson
 * Last updated: 3/26/2016
 */

using System;

namespace Tactics_CoreGameEngine
{

/**********************************************************************************
**********************************************************************************
* 
* 					NOTE
* 
* 		THIS FILE IS A COLLECTION OF TRIGGER ABILITIES FOR CHARACTERS AND TRAPS
* 
**********************************************************************************
**********************************************************************************/


	/// <summary>
	/// Template class.
	/// </summary>
	class TTA : Trigger{
		public TTA() : base("TTA1", ""){}

		public override bool Triggered(TriggerPacket TP){
			return false;
		}

		protected override void mExecute(TriggerPacket TP){

		}
	}

	class TA1 : Trigger{ //Recruit: Draw a card
		public TA1() : base("TTA1", "TA000001"){}

		public override bool Triggered(TriggerPacket TP){
			if (TP.TYPE == TYPE.RECRUIT) {
				if (TP.USER == TP.TARGET) {
					return true;
				}
			}
			return false;
		}

		protected override void mExecute(TriggerPacket TP){
			TP.TARGET.CONTROLLER.DrawCard ();
			TP.TARGET.CONTROLLER.AddDamage (new Damage (Damage.TYPE.SPELL, 1, TP.USER));
		}
	}

	class TA2 : Trigger{ //Trap: Do 3 Damage to a Recruited Enemy Unit
		public TA2() : base("TTA1", "TA000002"){}

		public override bool Triggered(TriggerPacket TP){
			if (TP.TYPE == TYPE.RECRUIT) {
				if (TP.USER.CONTROLLER != TP.TARGET.CONTROLLER) {
					return true;
				}
			}
			return false;
		}

		protected override void mExecute(TriggerPacket TP){
			TP.TARGET.AddDamage (new Damage (Damage.TYPE.SPELL, 3, TP.USER));
		}
	}

	class TA3 : Trigger{ //Attack+1 when an Enemy is Recruited
		public TA3() : base("TTA1", "TA000003"){}

		public override bool Triggered(TriggerPacket TP){
			if (TP.TYPE == TYPE.RECRUIT) {
				if (TP.INITIATOR != TP.USER.CONTROLLER) {
					return true;
				}
			}
			return false;
		}

		protected override void mExecute(TriggerPacket TP){
			TP.USER.BaseAttack++;
		}
	}

	class TA4 : Trigger{ //Draw a card at start of turn
		public TA4() : base("TTA1", "TA000004"){}

		public override bool Triggered(TriggerPacket TP){
			if (TP.TYPE == TYPE.STARTTURN) {
				if (TP.INITIATOR == TP.USER.CONTROLLER) {
					return true;
				}
			}
			return false;
		}

		protected override void mExecute(TriggerPacket TP){
			TP.USER.CONTROLLER.DrawCard ();
		}
	}

} //End namespace

