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
* CommandInterface.cs
* Author: Nic Wilson
* Last updated: 3/27/2016
*/

using System;

namespace Tactics_CoreGameEngine
{
	public class CommandInterface
	{
		public Player P;

		public CommandInterface ()
		{
		}

		public virtual Command GetTurnCommand(){
			Console.Write ("*CMD: ");
			String command = Console.ReadLine ();
			Command c = Command.Parse (command, P);
			return c;
		}

		public virtual Target GetTarget(){
			Console.Write("*TARGET: ");
			String t = Console.ReadLine ();
			Target TARGET = Target.Parse (t, P);
			return TARGET;
		}
	} // End CommandInterface class

	public class EnemyAI_Pass : CommandInterface{
		public EnemyAI_Pass(){}

		public override Command GetTurnCommand(){
			return new Command (Command.TYPE.PASS, null, P);
		}
	} // End EnemyAI_Pass class

} // End namespace

