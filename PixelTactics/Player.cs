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
* Player.cs
* Author: Nic Wilson
* Last updated: 3/27/2016
*/

using System;
using System.Collections.Generic;

namespace Tactics_CoreGameEngine
{
	public class Player
	{
		public String Name;
		public GameBoard GAMEBOARD;
		public Character[] TRAPS;
		public Hand HAND;
		public Hand GRAVEYARD;
		public Deck DECK;
		public Player ENEMY;
		public int Life;
		private int MAXLIFE;
		public CommandInterface COMMANDINTERFACE;
		public bool RevealHandEffect = false;

		private int ROWS, COLUMNS;

		public TableTop TABLE;

		//public static List<TriggerPacket> PIPELINE = new List<TriggerPacket>();

		private int BaseMaxHandSize = 5;
		private int HandSizeModifier = 0;
		public int MaxHandSize {
			get {
				if (BaseMaxHandSize + HandSizeModifier < 0)
					return 0;
				return BaseMaxHandSize + HandSizeModifier;
			}
		}

		public Player (string Name, int ROWS, int COLUMNS, int MAXLIFE,
			Player ENEMY, TableTop TABLE, CommandInterface COMMANDINTERFACE)
		{
			this.Name = Name;
			this.ROWS = ROWS; this.COLUMNS = COLUMNS;
			this.GAMEBOARD = new GameBoard (COLUMNS, ROWS, this);
			this.TRAPS = new Character[3];
			this.Life = MAXLIFE;
			this.MAXLIFE = MAXLIFE;
			this.HAND = new Hand ();
			this.DECK = new Deck (this);
			this.GRAVEYARD = new Hand ();
			this.ENEMY = ENEMY;
			this.TABLE = TABLE;
			COMMANDINTERFACE.P = this;
			this.COMMANDINTERFACE = COMMANDINTERFACE;

			if (ENEMY == null)
				return;
			for (int i = 0; i < 4; i++) {
				DrawCard ();
			}
		}

		public void SetEnemy(Player ENEMY){
			this.ENEMY = ENEMY;
			for (int i = 0; i < 4; i++) {
				DrawCard ();
			}
		}

		public Command GetTurnCommand(){
			return COMMANDINTERFACE.GetTurnCommand ();
		}

		public void DrawCard(){
			Command c = new Command (Command.TYPE.DRAW,
				            null, this);
			Command.Execute (c);
		}

		public void ClearBoard(){
			for (int i = 0; i < ROWS; i++) {
				for (int a = 0; a < COLUMNS; a++) {
					GAMEBOARD.BOARD [a, i] = null;
				}
			}
		}

		public bool AddTrap(Character c){
			bool g = false;
			for (int i = 0; i < 3; i++) {
				if (TRAPS [i] == null) {
					TRAPS [i] = c;
					g = true;
					break;
				}
			}
			return g;
		}

		public void LoseFromDrawOut(){
			if (DECK.Count == 0) {
				Life = 0;
				TABLE.VALID = false;
			}
		}

		public bool Active(int handindex){
			Character c = HAND.Get(handindex);
			if (c == null)
				return false;
			Active a = (Active)c.HandAbility;
			if (a == null || a.GetType().IsSubclassOf(typeof(Trigger)))
				return false;
			//Get the target if there is any and return false if it fails
			bool t = a.GetTarget(this);
			if (!t)
				return false;
			
			//Add pipeline event for the active
			TriggerPacket TP = new TriggerPacket (Trigger.TYPE.PLAYORDER,
				                   this, c, this);
			TP.ACTIVETARGET = a;
			TABLE.PIPELINE.Add (TP);

			HAND.Discard (c, GRAVEYARD);
			return true;
		}

		public bool Trap(int handindex){
			Character c = HAND.Get (handindex);
			if (c == null)
				return false;
			Ability a = (Ability)c.HandAbility;
			if (a == null || !a.GetType().IsSubclassOf(typeof(Trigger)))
				return false;
			bool g = AddTrap (c);
			if (g) {
				HAND.Discard (c);
				TABLE.PIPELINE.Add (new TriggerPacket (Trigger.TYPE.PLAYTRAP, this, c, this));
			}

			return g;
		}

		public void AddDamage(Damage D){
			this.Life -= D.VALUE;
			TABLE.PIPELINE.Add (new TriggerPacket (Trigger.TYPE.PLAYERDAMAGE, this, D.SOURCE, D.SOURCE.CONTROLLER));
		}

		public void HealDamage(Damage D){
			this.Life += D.VALUE;
			if (this.Life > MAXLIFE)
				this.Life = MAXLIFE;
			TABLE.PIPELINE.Add (new TriggerPacket (Trigger.TYPE.PLAYERHEAL, this, D.SOURCE, D.SOURCE.CONTROLLER));
		}

		public bool OnGoing(int handindex){
			Character c = HAND.Get (handindex);
			if (c == null)
				return false;
			Ability a = (Ability)c.HandAbility;
			if (a == null || !a.GetType().IsSubclassOf(typeof(OnGoing)))
				return false;
			bool g = AddTrap (c);
			if (g) {
				HAND.Discard (c);
				TABLE.PIPELINE.Add (new TriggerPacket (Trigger.TYPE.PLAYONGOING, this, c, this));
			}
			return g;
		}

		public void PrintHand(bool hide){
			foreach (Character c in HAND.GetHand()) {
				if (!hide || RevealHandEffect)
					Console.WriteLine (c);
				else
					Console.WriteLine ("****** HIDDEN CARD ******");
			}
		}

		public void PrintTraps(){
			foreach (Character c in TRAPS) {
				Console.Write ("< ");
				if (c == null)
					Console.Write (" ");
				else if (c.HandAbility.GetType ().IsSubclassOf (typeof(Trigger)))
					Console.Write ("T");
				else
					Console.Write (((OnGoing)(c.HandAbility)).Timer);
				Console.Write (" >");
			}
			Console.WriteLine ();
		}

		public bool DoesControlUnit(Character c){
			for (int i = 0; i < ROWS; i++) {
				for (int a = 0; a < COLUMNS; a++) {
					if (GAMEBOARD.BOARD [a, i] == c)
						return true;
				}
			}
			return false;
		}

		public List<PassivePair> GatherPassives(){
			List<PassivePair> ret = new List<PassivePair> ();
			//Grab ongoing passives
			for (int i = 0; i < TRAPS.Length; i++) {
				if (TRAPS [i] == null)
					continue;
				if ((TRAPS [i]).HandAbility.GetType ().IsSubclassOf (typeof(OnGoing))) {
					//If this trap slot is an ongoing
					OnGoing og = (OnGoing)TRAPS [i].HandAbility;
					foreach (Passive p in og.Passives) {
						ret.Add (new PassivePair(p, TRAPS[i]));
					}
				}
			}
			//Board passives
			ret.AddRange(GAMEBOARD.GatherPassives());
			ret.Sort((x,y) => x.p.Priority.CompareTo(y.p.Priority));
			return ret;
		}

		public void EndTurn(){
			//Check expired ongoings
			for (int i = 0; i < TRAPS.Length; i++) {
				if (TRAPS [i] == null)
					continue;
				TRAPS [i].TrapArmed = true;
				if (TRAPS [i].HandAbility.GetType ().IsSubclassOf (typeof(OnGoing))) {
					//If this trap slot is an ongoing
					OnGoing og = (OnGoing)TRAPS [i].HandAbility;
					og.Decrement ();
					if (og.Expired) {
						og.Discard (this);
						GRAVEYARD.AddCard (TRAPS [i]);
						TRAPS [i] = null;
					}
				}
			}
			TABLE.SettleState (this);

			GAMEBOARD.ResetFlags ();
			GAMEBOARD.ResetStunned ();
		}

		public void StartTurn(){
			GAMEBOARD.ResetFlags ();
		}

	} // End Player class

} // End namespace

