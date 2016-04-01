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
 * Character.cs
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
* 		THIS FILE IS A COLLECTION OF CHARACTERS THAT ARE THE CARDS OF THE GAME
* 
**********************************************************************************
**********************************************************************************/

	class UC1 : Character{
		public UC1(Player CONTROLLER) : base(CONTROLLER, "UN000001", 2, 3, 0,
		"TYP000001", "TYP000002"){
			this.AddPassive (new TestPassive2 (), 0);
			this.AddPassive (new TestPassive1 (), 1);
		}
	}

	class UC2 : Character{
		public UC2(Player CONTROLLER) : base(CONTROLLER, "UN000002", 3, 2, 0,
			"TYP000001", "TYP000002"){
			this.AddPassive (new TestPassive1 (), 0);
			this.AddPassive (new TestPassive2 (), 1);
		}
	}

	class UC3 : Character{
		public UC3(Player CONTROLLER) : base(CONTROLLER, "UN000003", 1, 1, 0,
			"TYP000001", "TYP000003"){
			this.AddPassive (new TestPassive3 (), 0);
			this.AddPassive (new TestPassive4 (), 1);
		}
	}

	class UC4 : Character{
		public UC4(Player CONTROLLER) : base(CONTROLLER, "UN000004", 4, 5, 1,
			"TYP000001", "TYP000004"){
			this.Upgrade = true;
			this.AddPassive (new TestPassive5 (), 0);
		}
	}

	class UC5 : Character{
		public UC5(Player CONTROLLER) : base(CONTROLLER, "UN000005", 0, 6, 0,
			"TYP000001", "TYP000005"){
			this.AddPassive (new TestPassive6 (), 0);
			this.AddPassive (new TestPassive6 (), 1);
		}
	}


} //End namespace

