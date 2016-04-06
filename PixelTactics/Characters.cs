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

	class UC1 : Character{ //Basic Adventurer
		public UC1(Player CONTROLLER) : base(CONTROLLER, "UN000001", 2, 3, 1,
		"TYP000001", "TYP000002"){
			this._AddPassive (new TestPassive2 (), 0);
			this._AddPassive (new TP1 (), 1);
		}
	}

	class UC2 : Character{ //Basic Warrior
		public UC2(Player CONTROLLER) : base(CONTROLLER, "UN000002", 3, 2, 0,
			"TYP000001", "TYP000002"){
			this._AddPassive (new TP1 (), 0);
			this._AddPassive (new TestPassive2 (), 1);
		}
	}

	class UC3 : Character{ //Basic Slime
		public UC3(Player CONTROLLER) : base(CONTROLLER, "UN000003", 1, 1, 0,
			"TYP000001", "TYP000003"){
			this._AddPassive (new TestPassive3 (), 0);
			this._AddPassive (new TestPassive4 (), 1);
		}
	}

	class UC4 : Character{ //Basic Ogre
		public UC4(Player CONTROLLER) : base(CONTROLLER, "UN000004", 4, 5, 1,
			"TYP000001", "TYP000004"){
			this.Upgrade = true;
			this._AddPassive (new TestPassive5 (), 0);
		}
	}

	class UC5 : Character{ //Basic wall
		public UC5(Player CONTROLLER) : base(CONTROLLER, "UN000005", 0, 6, 0,
			"TYP000001", "TYP000005"){
			this._AddPassive (new TestPassive6 (), 0);
			this._AddPassive (new TestPassive6 (), 1);
		}
	}

	class UC9 : Character{ //Mystic Elf
		public UC9(Player CONTROLLER) : base(CONTROLLER, "UN000009", 0, 5, 1,
			"TYP000006"){
			this._AddTrigger (new TA5 (), 0);
			this._AddPassive (new TP1 (), 1);
			this._AddHandAbility (new TO1 ());
		}
	}

	class UC10 : Character{ //Commando
		public UC10(Player CONTROLLER) : base(CONTROLLER, "UN000010", 2, 2, 0,
			"TYP000002"){
			this._AddPassive (new TestPassive7 (), 0);
			this._AddTrigger (new TA1 (), 1);
			this._AddHandAbility (new AA1 ());
		}
	}

	class UC11 : Character{ //Bubba
		public UC11(Player CONTROLLER) : base(CONTROLLER, "UN000011", 2, 4, 2,
			"TYP000002"){
			this.Gravedigger = true;
			this._AddPassive (new TestPassive7 (), 0);
			this._AddTrigger (new TA1 (), 0);
			this._AddTrigger (new TA4 (), 1);
			this._AddHandAbility (new AA2 ());
		}
	}

	class UC14 : Character{ //Sneaky Trapper
		public UC14(Player CONTROLLER) : base(CONTROLLER, "UN000014", 1, 1, 1,
			"TYP000002"){
			this._AddTrigger (new TA3 (), 0);
			this._AddHandAbility (new TA2 ());
		}
	}

	class UC19 : Character{ //Paper Boy
		public UC19(Player CONTROLLER) : base(CONTROLLER, "UN000019", 0, 2, 0,
			"TYP000002"){
			this._AddPassive (new TestPassive2 (), 0);
			this._AddPassive (new TP9 (), 1);
			this._AddHandAbility (new AA3 ());
		}
	}


} //End namespace

