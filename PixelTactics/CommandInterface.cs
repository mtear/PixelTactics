using System;

namespace PixelTactics
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
	}

	public class EnemyAI : CommandInterface{
		public EnemyAI(){}

		public override Command GetTurnCommand(){
			return new Command (Command.TYPE.PASS, null, P);
		}
	}
}

