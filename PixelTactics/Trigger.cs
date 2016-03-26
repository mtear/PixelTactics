using System;
using System.Collections.Generic;

namespace PixelTactics
{
	public abstract class Trigger : Ability
	{
		public enum TYPE
		{
			DRAW, RECRUIT, NULL, ENDTURN, UNITDIED, MOVE, STARTTURN, UNITDAMAGE, UNITHEAL, PLAYERDAMAGE, PLAYERHEAL,
			TRAPACTIVATE, PLAYTRAP, PLAYONGOING, PLAYORDER, ACTIVE, CREATECORPSE
		}

		public bool COUNTER = false;

		public Trigger (string name, string description) : base(name, description)
		{
		}

		public bool Check(TriggerPacket TP){
			bool b = Triggered (TP);
			return b;
		}

		public bool CheckAndExecute(TriggerPacket TP, List<TriggerPair> PIPELINE){
			bool b = Triggered (TP);
			if (b)
				PIPELINE.Add (new TriggerPair (TP, this));
			return b;
		}

		public void ExportEffectToPipeline(TriggerPacket TP, List<TriggerPair> PIPELINE){
			PIPELINE.Add (new TriggerPair (TP, this));
		}

		public abstract bool Triggered(TriggerPacket TP);
		public void Execute(TriggerPacket TP){
			if (TP.TYPE == Trigger.TYPE.TRAPACTIVATE) {
				if (TP.TRIGGERTARGET.VALID) {
					mExecute (TP);
					return;
				} else
					return;
			}
			mExecute (TP);
		}
		protected abstract void mExecute(TriggerPacket TP);

	}

	public class TriggerPacket{
		public Trigger.TYPE TYPE;
		public Player PLAYER, INITIATOR;
		public Character USER, TARGET;
		public Trigger TRIGGERTARGET = null;
		public Active ACTIVETARGET = null;
		public TriggerPacket TPTARGET = null;
		public TriggerPacket(Trigger.TYPE TYPE, Player PLAYER,
			Character USER, Player INITIATOR){
			this.TYPE = TYPE;
			this.PLAYER = PLAYER;
			this.USER = USER;
			this.TARGET = USER;
			this.INITIATOR = INITIATOR;
		}
		public TriggerPacket(Trigger.TYPE TYPE, Player PLAYER,
			Character USER, Player INITIATOR, Character TARGET){
			this.TYPE = TYPE;
			this.PLAYER = PLAYER;
			this.USER = USER;
			this.TARGET = TARGET;
			this.INITIATOR = INITIATOR;
		}
	}

	public class TriggerPair{
		public TriggerPacket TP;
		public Trigger TRIGGER;
		public TriggerPair (TriggerPacket TP, Trigger TRIGGER)
		{
			this.TP = TP;
			this.TRIGGER = TRIGGER;
		}
	}
		
}

