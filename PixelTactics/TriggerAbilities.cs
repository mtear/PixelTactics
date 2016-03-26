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

namespace PixelTactics
{

/**********************************************************************************
**********************************************************************************
* 
* 										NOTE
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

} //End namespace

