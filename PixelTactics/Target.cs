using System;

namespace PixelTactics
{
	public class Target
	{
		public Player PTARGET;
		public Character CTARGET;
		public bool TARGETPLAYER = true;
		public Target (Player PTARGET, Character CTARGET)
		{
			if (PTARGET == null)
				TARGETPLAYER = false;
			this.PTARGET = PTARGET;
			this.CTARGET = CTARGET;
		}

		public static Target Parse(String t, Player P){
			try{
				string[] chunks = t.Split(' ');
				if(chunks.Length == 1){
					if(chunks[0] == "0") return new Target(P, null);
					else if(chunks[0] == "1") return new Target(P.ENEMY, null);
				}else{
					int x = int.Parse(chunks[1]);
					int y = int.Parse(chunks[2]);
					GameBoard gb = (chunks[0] == "0") ? P.BOARD : P.ENEMY.BOARD;
					if(gb.BOARD[x,y] == null) return null;
					return new Target(null, gb.BOARD[x,y]);
				}
			}catch{}
			return null;
		}
	}
}

