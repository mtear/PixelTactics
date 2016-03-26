using System;
using System.Collections.Generic;

namespace PixelTactics
{
	public class TableTop
	{
		public static readonly int ROWS = 2, COLUMNS = 3, MAXLIFE = 15;

		private bool firstturn = true;

		public bool VALID = true;

		public List<TriggerPacket> PIPELINE = new List<TriggerPacket>();
		public List<TriggerPair> EFFECTPIPELINE = new List<TriggerPair>();

		public Player WINNER = null;

		public StringTable STRINGTABLE;

		public TableTop (String LCODE)
		{
			Player P1 = new Player ("P1", ROWS, COLUMNS, MAXLIFE, null, this, new CommandInterface());
			Player P2 = new Player ("P2", ROWS, COLUMNS, MAXLIFE, P1, this, new CommandInterface());
			P1.SetEnemy (P2);

			//Setup string table
			STRINGTABLE = new StringTable(LCODE, P1);

			Play (P1);
		}

		public void Play(Player PLAYER){
			Turn (PLAYER); //first turn
			SettleState (PLAYER);
			while (VALID) {
				Turn (PLAYER.ENEMY);
				if (!VALID)break;
				SettleState (PLAYER);
				if (!VALID)break;
				FullAttack (PLAYER.ENEMY);
				if (!VALID)break;
				SettleState (PLAYER);
				if (!VALID)break;
				Turn (PLAYER.ENEMY);
				if (!VALID)break;
				SettleState (PLAYER);
				if (!VALID)break;
				Turn (PLAYER);
				if (!VALID)break;
				SettleState (PLAYER);
				if (!VALID)break;
				FullAttack (PLAYER);
				if (!VALID)break;
				SettleState (PLAYER);
				if (!VALID)break;
				Turn (PLAYER);
				if (!VALID)break;
				SettleState (PLAYER);
				if (!VALID)break;
			}
			Print (PLAYER);
			if (WINNER == null)
				Console.WriteLine ("\n\n\nTHE GAME IS A DRAW");
			else
				Console.WriteLine ("\n\n\n" + WINNER.Name + " WINS!");
		}

		public void Turn(Player P){
			//Add Start turn trigger event
			PIPELINE.Add (new TriggerPacket (Trigger.TYPE.STARTTURN,
				P, null, P));
			SettleState (P);

			P.StartTurn ();
			if (!firstturn) P.DrawCard (); //Draw if not the first turn
			else firstturn = false;
			do {
				SettleState(P);
				Print (P);
			} while(TakeTurn (P));

			P.EndTurn ();
		}

		public void Print(Player P){
			Console.Clear ();
			P.ENEMY.PrintTraps ();
			P.ENEMY.BOARD.PrintBoard (true);
			P.ENEMY.BOARD.PrintUnits ();
			Console.WriteLine ("---------------");
			P.BOARD.PrintBoard (false);
			P.PrintTraps ();
			P.BOARD.PrintUnits ();
			Console.WriteLine ();

			Console.WriteLine (P.Name + "(" + P.Life + ") HAND:");
			P.PrintHand (false);
			Console.WriteLine ();
			Console.WriteLine (P.ENEMY.Name + "(" + P.ENEMY.Life + ") HAND:");
			P.ENEMY.PrintHand (true);
			Console.WriteLine ();
		}

		public void SettleState(Player P){
			if (!VALID)
				return;

			P.BOARD.CheckAllDead ();
			P.ENEMY.BOARD.CheckAllDead ();
			CalculateUnitStats (P);
			CalculateUnitStats (P.ENEMY);
			PopTrigger (P);
			bool haseffects = EFFECTPIPELINE.Count > 0;
			while (EFFECTPIPELINE.Count > 0) {
				PopEffect ();
			}
			if (haseffects || PIPELINE.Count > 0)
				SettleState (P);

			//Check dead players
			if (P.Life <= 0 || P.ENEMY.Life <= 0) {
				VALID = false;
				if (P.Life == 0 && P.ENEMY.Life == 0)
					WINNER = null;
				else if (P.Life == 0)
					WINNER = P.ENEMY;
				else
					WINNER = P;
					
			}
		}

		void PopEffect(){
			if (EFFECTPIPELINE.Count == 0) return;
			TriggerPair T = EFFECTPIPELINE [0];
			EFFECTPIPELINE.RemoveAt (0);
			if (T.TP.TYPE == Trigger.TYPE.PLAYORDER) {
				if(T.TP.ACTIVETARGET.VALID)
					T.TP.ACTIVETARGET.Execute (T.TP.TARGET, T.TP.PLAYER);
			} else {
				T.TRIGGER.Execute (T.TP);
			}
		}

		TriggerPacket PopTrigger(Player P){
			if (PIPELINE.Count == 0) return null;
			TriggerPacket TP = PIPELINE [0];
			PIPELINE.RemoveAt (0);
			ProcessTriggerPacket (TP, P);
			BroadcastEvent (TP, EFFECTPIPELINE);
			return TP;
		}

		private void ProcessTriggerPacket(TriggerPacket TP, Player P){
			if (TP.TYPE == Trigger.TYPE.UNITDIED
				|| TP.TYPE == Trigger.TYPE.MOVE
				|| TP.TYPE == Trigger.TYPE.STARTTURN) {
				TP.PLAYER = (P.DoesControlUnit (TP.USER)) ? P : P.ENEMY;
				TP.INITIATOR = (P.DoesControlUnit (TP.USER)) ? P : P.ENEMY;
			}
		}

		public bool TakeTurn(Player PLAYER){
			if (!VALID)
				return false;
			
			Console.Write (PLAYER.Name + " ");
			Command c = PLAYER.GetTurnCommand ();
			try{
				Command.Execute (c);
			}catch{return false;}
			return c == null || c.type != Command.TYPE.PASS;
		}

		public void FullAttack(Player P){
			for (int a = 0; a < COLUMNS; a++) {
				if (P.BOARD.BOARD [a,0] != null) {
					P.BOARD.Melee (a, 0, a);
				}
				if (P.ENEMY.BOARD.BOARD [a,0] != null) {
					P.ENEMY.BOARD.Melee (a, 0, a);
				}
			}
			//Ranged
			for (int a = 0; a < COLUMNS; a++) {
				if (P.BOARD.BOARD [a,1] != null
					&& !P.BOARD.BOARD [a,1].Melee) {
					P.BOARD.Melee (a, 1, a);
				}
				if (P.ENEMY.BOARD.BOARD [a,1] != null
					&& !P.ENEMY.BOARD.BOARD [a,1].Melee) {
					P.ENEMY.BOARD.Melee (a, 1, a);
				}
			}
		}

		public void CalculateUnitStats(Player P){
			P.BOARD.CalculateUnitStats ();
		}

		public void BroadcastEvent(TriggerPacket TP, List<TriggerPair> EFFECTPIPELINE){
			bool countered = CheckTraps (TP, EFFECTPIPELINE);
			if (countered) {
				PIPELINE.Add (TP);
				return;
			}

			//Check unit passives
			for (int i = 0; i < ROWS; i++) {
				for (int a = 0; a < COLUMNS; a++) {
					Player A = TP.PLAYER;
					for(int j = 0; j < 2; j++){ //Go twice for both players
						Character c = A.BOARD.BOARD [a, i];
						if (c == null) {
							A = TP.PLAYER.ENEMY;
							continue;
						}

						foreach (Trigger trig in c.triggers[i]) {
							TP.USER = c;
							trig.CheckAndExecute (TP, EFFECTPIPELINE);
						}

						//Setup for next loop
						A = TP.PLAYER.ENEMY;
					}
				}
			}

			//Checked and processed
			//If a trapactivate, put on effect stack (or order activate)
			if (TP.TYPE == Trigger.TYPE.TRAPACTIVATE) {
				TP.TRIGGERTARGET.ExportEffectToPipeline (TP, EFFECTPIPELINE);
			}
			if (TP.TYPE == Trigger.TYPE.PLAYORDER) {
				TP.ACTIVETARGET.ExportEffectToPipeline (TP, EFFECTPIPELINE);
			}

		}

		public bool CheckTraps(TriggerPacket TP, List<TriggerPair> EFFECTPIPELINE){
			bool countered = false;
			//Check traps
			Player P = TP.PLAYER;
			Character[] traps = TP.PLAYER.TRAPS;
			for (int a = 0; a < 2; a++) {
				for (int i = 0; i < traps.Length; i++) {
					if (traps [i] == null)
						continue;
					if (!traps [i].CanTrap)
						continue;
					if (traps [i].order.GetType ().IsSubclassOf (typeof(Trigger))) {
						//Get the trap
						Trigger t = (Trigger)traps [i].order;
						TP.USER = traps [i];
						bool activated = t.Check (TP);
						//Activate trap
						if (activated) {
							TriggerPacket TRAPPACKET = new TriggerPacket (Trigger.TYPE.TRAPACTIVATE,
								traps[i].CONTROLLER, traps[i], traps[i].CONTROLLER);
							TRAPPACKET.TRIGGERTARGET = (Trigger)traps[i].order;
							TRAPPACKET.TPTARGET = TP;
							if (TRAPPACKET.TRIGGERTARGET.COUNTER) {
								PIPELINE.Insert (0, TRAPPACKET);
								countered = true;
							}
							else PIPELINE.Add(TRAPPACKET);

							//discard
							P.GRAVEYARD.AddCard (traps [i]);
							traps [i] = null;
						}
					} else if (traps [i].order.GetType ().IsSubclassOf (typeof(OnGoing))) {
						//Get the ongoing
						OnGoing og = (OnGoing)traps [i].order;
						foreach (Trigger t in og.Triggers) {
							TP.USER = traps [i];
							t.CheckAndExecute (TP, EFFECTPIPELINE);
						}
					}
				}
				traps = TP.PLAYER.ENEMY.TRAPS;
				P = TP.PLAYER.ENEMY;
			}
			return countered;
		}

	}
}

