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


	class UC_CITIZEN : Character{
		public UC_CITIZEN(Player CONTROLLER) : base(CONTROLLER, "UN000004",
			1, 1){}
	}

	class UC1 : Character{
		public UC1(Player CONTROLLER) : base(CONTROLLER, "UN000001", 1, 3){}
	}

	class UC2 : Character{
		public UC2(Player CONTROLLER) : base(CONTROLLER, "UN000002", 1, 3){}
	}

	class UC3 : Character{
		public UC3(Player CONTROLLER) : base(CONTROLLER, "UN000003", 15, 1){
			//Gravedigger = true; GravediggerPlus = 2;
		}
	}


} //End namespace

