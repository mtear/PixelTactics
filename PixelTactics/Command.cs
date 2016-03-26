using System;
using System.Collections.Generic;

namespace PixelTactics
{
	public class Command
	{
		public enum TYPE{
			MOVE, QUIT, PASS, MELEE, RECRUIT, DRAW, CLEARCORPSE, ACTIVE, TRAP, ONGOING
		}

		public TYPE type;
		public string[] param;
		public Player PLAYER;
		public TYPE CTYPE{
			get{
				return type;
			}
		}

		//public static List<TriggerPacket> PIPELINE = new List<TriggerPacket>();

		public Command (TYPE type, string[] param, Player PLAYER)
		{
			this.type = type;
			this.param = param;
			this.PLAYER = PLAYER;
		}

		public int Cost{
			get{
				switch (type) {
					case TYPE.MOVE:
						return 1;
					case TYPE.QUIT:
						return 0;
					case TYPE.PASS:
						return 1;
					case TYPE.MELEE:
						return 0;
					case TYPE.RECRUIT:
						return 0;
					case TYPE.DRAW:
						return 0;
					case TYPE.CLEARCORPSE:
						return 0;
					case TYPE.ACTIVE:
						return 0;
					case TYPE.TRAP:
						return 0;
					case TYPE.ONGOING:
						return 0;
				}
				return 1;
			}
		}

		public static Command Parse(String s, Player PLAYER){
			s = s.ToLower ();
			try{
				string[] chunks = s.Split(' ');
				string[] param = new List<string>(chunks).GetRange(1, chunks.Length-1).ToArray();
				switch(chunks[0]){
				case "move": return new Command(TYPE.MOVE, param, PLAYER);
				case "quit": return new Command(TYPE.QUIT, param, PLAYER);
				case "pass": return new Command(TYPE.PASS, param, PLAYER);
				case "melee": return new Command(TYPE.MELEE, param, PLAYER);
				case "recruit": return new Command(TYPE.RECRUIT, param, PLAYER);
				case "draw": return new Command(TYPE.DRAW, param, PLAYER);
				case "clear": return new Command(TYPE.CLEARCORPSE, param, PLAYER);
				case "active": return new Command(TYPE.ACTIVE, param, PLAYER);
				case "trap": return new Command(TYPE.TRAP, param, PLAYER);
				case "ongoing": return new Command(TYPE.ONGOING, param, PLAYER);
				}
			}catch{}
			return null;
		}

		public static bool Execute(Command c){
			execTP etp = pExecute (c);
			if(etp.b) c.PLAYER.TABLE.PIPELINE.Add(etp.TP);
			return etp.b;
		}

		private static Trigger.TYPE CtoT(Command.TYPE type){
			if (type == Command.TYPE.DRAW) return Trigger.TYPE.DRAW;
			if (type == Command.TYPE.RECRUIT) return Trigger.TYPE.RECRUIT;
			if (type == Command.TYPE.PASS) return Trigger.TYPE.ENDTURN;
			if (type == Command.TYPE.ACTIVE) return Trigger.TYPE.ACTIVE;
			return Trigger.TYPE.NULL;
		}

		class execTP{
			public TriggerPacket TP;
			public bool b;
			public execTP(TriggerPacket TP, bool b){
				this.TP = TP; this.b = b;
			}
		}

		private static execTP pExecute(Command c){
			execTP etp = new execTP (null, false);
			if (c == null)
				return etp;

			etp.TP = new TriggerPacket (CtoT (c.type), c.PLAYER, null, c.PLAYER);

			try{
			switch (c.type) {
			case TYPE.MOVE:
				{
					etp.b= c.PLAYER.BOARD.ValidMove (int.Parse (c.param [0]),
					int.Parse (c.param [1]),
					int.Parse (c.param [2]),
					int.Parse (c.param [3]), false);
						if(etp.b){
							c.PLAYER.TABLE.SettleState(c.PLAYER);
							etp.b= c.PLAYER.BOARD.Move (int.Parse (c.param [0]),
								int.Parse (c.param [1]),
								int.Parse (c.param [2]),
								int.Parse (c.param [3]));
						}
						break;
				}
			case TYPE.QUIT:
				{
					Environment.Exit (0); break;
				}
			case TYPE.PASS:
				{
						etp.b= true; break;
				}
			case TYPE.MELEE:
				{
					etp.b= c.PLAYER.BOARD.Melee(
						int.Parse(c.param[0]),
						int.Parse(c.param[1]),
							int.Parse(c.param[2]));	 break;
				}
				case TYPE.RECRUIT:
				{
						etp.TP.TARGET = c.PLAYER.HAND.Get(int.Parse(c.param[2]));
						etp.b = c.PLAYER.BOARD.Recruit(
						int.Parse(c.param[0]),
						int.Parse(c.param[1]),
						int.Parse(c.param[2]),
							c.PLAYER); break;
				}
				case TYPE.DRAW:
				{
						etp.b = c.PLAYER.HAND.Draw(c.PLAYER); break;
				}
				case TYPE.CLEARCORPSE:
				{
						etp.TP.TARGET = c.PLAYER.BOARD.BOARD[int.Parse(c.param[0]), int.Parse(c.param[1])];
						etp.b = c.PLAYER.BOARD.ClearCorpse(c.PLAYER.GRAVEYARD,
							int.Parse(c.param[0]),
							int.Parse(c.param[1])); break;
				}
				case TYPE.ACTIVE:{
						etp.b= c.PLAYER.Active(
							int.Parse(c.param[0])); break;
				}
				case TYPE.TRAP:{
						etp.TP.TARGET = c.PLAYER.HAND.Get(int.Parse(c.param[0]));
						etp.b = c.PLAYER.Trap(int.Parse(c.param[0])); break;
				}
				case TYPE.ONGOING:{
						etp.TP.TARGET = c.PLAYER.HAND.Get(int.Parse(c.param[0]));
						etp.b = c.PLAYER.OnGoing(int.Parse(c.param[0])); break;
				}
			}

			}catch{
				etp.b = false;
			}

			return etp;
		}
	
	}
}

