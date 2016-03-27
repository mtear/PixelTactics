using System;
using System.Collections.Generic;

namespace PixelTactics
{
	public class GameBoard
	{

		public Character[,] BOARD;
		private int COLUMNS, ROWS;

		public Player OWNER;

		public GameBoard (int COLUMNS, int ROWS, Player OWNER)
		{
			this.OWNER = OWNER;
			this.COLUMNS = COLUMNS; this.ROWS = ROWS;
			BOARD = new Character[COLUMNS, ROWS];
		}

		public int NumberOfCorpses{
			get{
				int corpses = 0;
				for (int i = 0; i < ROWS; i++) {
					for (int a = 0; a < COLUMNS; a++) {
						if (BOARD [a, i] != null
						    && BOARD [a, i].Dead)
							corpses++;
					}
				}
				return corpses;
			}
		}

		public int NumberOfUnits{
			get{
				int units = 0;
				for (int i = 0; i < ROWS; i++) {
					for (int a = 0; a < COLUMNS; a++) {
						if (BOARD [a, i] != null
							&& !BOARD [a, i].Dead)
							units++;
					}
				}
				return units;
			}
		}

		public bool ValidMove(int x1, int y1, int x2, int y2, bool forced){
			//Check valid openings
			if (BOARD [x1, y1] == null ||
				(BOARD [x2, y2] != null && !BOARD[x2,y2].Dead))
				return false;

			if (BOARD [x1, y1].Rooted && !forced
				|| BOARD[x1, y1].Stunned && !forced)
				return false;

			//Check valid action
			Character c1 = BOARD [x1, y1];
			if (c1.Moved || c1.Dead)
				return false;

			//Add to Pipeline
			c1.CONTROLLER.TABLE.PIPELINE.Add (new TriggerPacket (Trigger.TYPE.MOVE,
				null, c1, null));
			return true;
		}

		public bool Move(int x1, int y1, int x2, int y2){
			//Check valid openings
			if (BOARD [x1, y1] == null ||
				(BOARD [x2, y2] != null && !BOARD[x2,y2].Dead))
				return false;
			
			//Check valid action
			Character c1 = BOARD [x1, y1];
			if (c1.Moved || c1.Dead)
				return false;

			//Clear corpse if needed
			if(BOARD[x2,y2] != null && !BOARD[x2,y2].Dead){
				Command.Execute (new Command (
					Command.TYPE.CLEARCORPSE,
					new string[] { x1.ToString (), y1.ToString () },
					OWNER));
			}
			
			//Move
			BOARD[x2,y2] = BOARD[x1,y1];
			BOARD [x1, y1] = null;

			c1.Moved = true;
			return true;
		}

		public void PrintBoard(bool flip){
			for (int i = 0; i < ROWS; i++) {
				for (int a = 0; a < COLUMNS; a++) {
					Console.Write ("[ ");
					int row = (flip) ? ROWS - i - 1 : i;
					if (BOARD [a, row] == null) {
						Console.Write (" ");
					} else {
						if (BOARD [a, row].Dead)
							Console.Write ("C");
						else
							Console.Write ("X");
					}
					Console.Write (" ]");
				}
				Console.WriteLine ();
			}
		}

		public void PrintUnits(){
			for (int i = 0; i < ROWS; i++) {
				for (int a = 0; a < COLUMNS; a++) {
					if (BOARD [a, i] != null) {
						Console.Write (a + ", " + i + ": ");
						Console.WriteLine (BOARD [a, i]);
					}
				}
			}
			Console.WriteLine ();
		}

		public void Print(bool flip){
			PrintBoard (flip);
			PrintUnits ();
		}

		public Character FindCharacterInMelee(int col){
			Character ret = null;
			for (int i = 0; i < ROWS; i++) {
				//Look for intercept target in the row
				Character InterceptTarget = FindInterceptTarget (i);
				if (InterceptTarget != null) {
					return InterceptTarget;
				}

				if (BOARD [col, i] != null && !BOARD[col,i].Dead) {
					return BOARD [col, i];
				}
			}
			return ret;
		}

		public Character FindInterceptTarget(int row){
			List<Character> interceptors = new List<Character> ();
			//Gather alive characters with intercept in the row
			for (int i = 0; i < COLUMNS; i++) {
				if (BOARD [i, row] == null || BOARD[i,row].Dead)
					continue;
				if (BOARD [i, row].Intercept)
					interceptors.Add (BOARD [i, row]);
			}
			//Pick one at random or return null if none
			if (interceptors.Count == 0)
				return null;
			else
				return interceptors [Utility.R.Next (interceptors.Count)];
		}

		private Character FindOverkillTarget(Character target){
			Point P = LocateInBoard (target);
			int x = P.x; int y = P.y + 1;
			if (y == ROWS)
				return null;
			return BOARD [x, y];
		}

		public bool Melee(int x1, int y1, int col){
			Character attacker = BOARD [x1, y1];
			Player ENEMY = OWNER.ENEMY;
			if (attacker == null || attacker.Dead || attacker.Stunned)
				return false;
			Character target = ENEMY.BOARD.FindCharacterInMelee (col);

			//Damage type
			Damage.TYPE DT = Damage.TYPE.MELEE;
			if (!attacker.IsMelee)
				DT = Damage.TYPE.RANGE;

			if (target != null) {
				target.AddDamage (new Damage(DT, attacker.Attack, attacker));
				//Overkill
				for (int r = 0; r < 2; r++) {
					if (attacker.Overkill && target.Damage >= target.Life) {
						int overkilldamage = target.Damage - target.Life;
						target = ENEMY.BOARD.FindOverkillTarget (target);
						if (target != null) {
							target.AddDamage (new Damage (DT, overkilldamage, attacker));
						} else {
							ENEMY.AddDamage (new Damage (DT, overkilldamage, attacker));
						}
					}
				}
			} else {
				ENEMY.AddDamage (new Damage(DT, attacker.Attack, attacker));
			}
				
			return true;
		}

		public bool IsLaneOpen(int attackcolumn){
			bool open = true;
			for(int i = 0; i < ROWS; i++){
				open = (BOARD[attackcolumn, i] == null) && open;
			}
			return open;
		}

		public bool Recruit(int x1, int y1, int handslot, Player PLAYER){
			Character c = PLAYER.HAND.Get (handslot);
			if (c == null)
				return false;

			if (!ValidRecruit (c, x1, y1)) //Check for legal recruitment
				return false;

			Character c2 = BOARD [x1, y1];
			if (c2 != null) {
				if (c2.Dead) {
					Command.Execute (new Command (
						Command.TYPE.CLEARCORPSE,
						new string[] { x1.ToString (), y1.ToString () },
						PLAYER));
				}
			}
			BOARD [x1, y1] = c;

			PostRecruitEffects (c, x1, y1);

			PLAYER.HAND.Discard (c, PLAYER.GRAVEYARD);
			return true;
		}

		private void PostRecruitEffects(Character c, int x, int y){
			if (c.Gravedigger) {
				int p = c.GravediggerPlus;
				while (p > 0) {
					Point P = RandomCorpse();
					Command.Execute (new Command (
						Command.TYPE.CLEARCORPSE,
						new string[] { P.x.ToString (), P.y.ToString () },
						OWNER));
					p--;
				}
			}
			if (c.Upgrade) {
				int p = c.UpgradePlus;
				while (p > 0) {
					Point P = RandomUnit();
					if (P.x == x && P.y == y)
						continue;
					//remove unit
					OWNER.GRAVEYARD.AddCard(BOARD[P.x,P.y]);
					BOARD [P.x, P.y] = null;
					p--;
				}
			}
		}

		private Point RandomCorpse(){
			List<Point> corpses = new List<Point> ();
			for (int i = 0; i < ROWS; i++) {
				for (int a = 0; a < COLUMNS; a++) {
					if (BOARD [a, i] != null && BOARD [a, i].Dead)
						corpses.Add (new Point(a, i));
				}
			}
			if (corpses.Count == 0)
				return null;
			return corpses [Utility.R.Next (corpses.Count)];
		}

		private Point RandomUnit(){
			List<Point> units = new List<Point> ();
			for (int i = 0; i < ROWS; i++) {
				for (int a = 0; a < COLUMNS; a++) {
					if (BOARD [a, i] != null && !BOARD [a, i].Dead)
						units.Add (new Point(a, i));
				}
			}
			if (units.Count == 0)
				return null;
			return units [Utility.R.Next (units.Count)];
		}

		public void CheckAllDead(){
			for (int i = 0; i < ROWS; i++) {
				for (int a = 0; a < COLUMNS; a++) {
					if(BOARD[a,i] != null) BOARD [a, i].CheckDead ();
				}
			}
		}

		public bool ClearCorpse(Hand GRAVEYARD, int x1, int y1){
			if (BOARD [x1, y1].Dead) {
				GRAVEYARD.AddCard (BOARD [x1, y1]);
				BOARD [x1, y1] = null;
				return true;
			} else
				return false;
		}

		public List<PassivePair> GatherPassives(){
			List<PassivePair> ret = new List<PassivePair> ();
			for (int i = 0; i < ROWS; i++) {
				for (int a = 0; a < COLUMNS; a++) {
					Character uc1 = BOARD[a,i];
					if (uc1 == null)
						continue;
					foreach(Passive p in uc1.Passives[i]){
						ret.Add (new PassivePair (p, uc1));
					}

				}
			}
			ret.Sort((x,y) => x.p.Priority.CompareTo(y.p.Priority));
			return ret;
		}

		public void ResetFlags(){
			for (int i = 0; i < ROWS; i++) {
				for (int a = 0; a < COLUMNS; a++) {
					if (BOARD [a, i] != null) {
						BOARD [a, i].Moved = false;
					}
				}
			}
		}

		public void ResetStunned(){
			for (int i = 0; i < ROWS; i++) {
				for (int a = 0; a < COLUMNS; a++) {
					if (BOARD [a, i] != null) {
						BOARD [a, i].Stunned = false;
					}
				}
			}
		}

		public bool Contains(Character c){
			for (int i = 0; i < ROWS; i++) {
				for (int a = 0; a < COLUMNS; a++) {
					if (BOARD [a, i] == c)
						return true;
				}
			}
			return false;
		}

		public bool RowContains(Character c, int row){
			for (int i = 0; i < ROWS; i++) {
				if (BOARD [i, row] == c)
					return true;
			}
			return false;
		}

		public Point LocateInBoard(Character c){
			for (int i = 0; i < ROWS; i++) {
				for (int a = 0; a < COLUMNS; a++) {
					if (BOARD [a, i] == c)
						return new Point (a, i);
				}
			}
			return null;
		}

		public bool Full{
			get{
				for (int i = 0; i < ROWS; i++) {
					for (int a = 0; a < COLUMNS; a++) {
						if (BOARD [a, i] == null)
							return false;
					}
				}
				return true;
			}
		}

		private bool ValidRecruit(Character c, int x, int y){
			Character c2 = BOARD [x, y];
			if (c2 == null) { //Nothing in the slot
				if (c.Upgrade || c.Gravedigger) //upgrade or graved
					return false;
				return true; //regular unit
			} else { //Something there
				if (c2.Dead) { //Corpse there
					if (c.Upgrade) //upgrade
						return false; //regular unit
					if (c.Gravedigger) { //Gravedigger
						return NumberOfCorpses >= c.GravediggerPlus+1;
					}
					return true; //regular unit
				} else { //Alive unit there
					if (c.Upgrade) //upgrade
						return NumberOfUnits >= c.UpgradePlus+1;
					return false; //regular unit / gravedigger
				}
			}
		}

		private bool CalculateRevealHand(){
			bool reveal = false;

			List<PassivePair> AP = OWNER.GatherPassives ();
			List<PassivePair> EP = OWNER.ENEMY.GatherPassives ();

			foreach (PassivePair pp in AP) {
				reveal = pp.p.RevealYourHand (reveal, pp.c, OWNER);
			}
			foreach (PassivePair pp in EP) {
				reveal = pp.p.RevealEnemyHand (reveal, pp.c, OWNER.ENEMY);
			}

			return reveal;
		}

		private int CalculateDamage(int x1, int y1){
			Character c = OWNER.BOARD.BOARD [x1, y1];
			int damage = c.BaseAttack;
			if (c == null)
				return damage;

			List<PassivePair> AP = OWNER.GatherPassives ();
			List<PassivePair> EP = OWNER.ENEMY.GatherPassives ();

			foreach (PassivePair pp in AP) {
				damage = pp.p.ModifyDamage (damage, c, pp.c, OWNER);
			}
			foreach (PassivePair pp in EP) {
				damage = pp.p.ModifyDamage (damage, c, pp.c, OWNER.ENEMY);
			}

			//clamp and return
			if (damage < 0)
				damage = 0;

			return damage;
		}

		private int CalculateLife(int x1, int y1){
			Character c = OWNER.BOARD.BOARD[x1, y1];
			int life = c.BaseLife;
			if (c == null)
				return life;

			List<PassivePair> AP = OWNER.GatherPassives ();
			List<PassivePair> EP = OWNER.ENEMY.GatherPassives ();

			foreach (PassivePair pp in AP) {
				life = pp.p.ModifyLife (life, c, pp.c, OWNER);
			}
			foreach (PassivePair pp in EP) {
				life = pp.p.ModifyLife (life, c, pp.c, OWNER.ENEMY);
			}

			//clamp and return
			if (life < 0)
				life = 0;

			return life;
		}

		private bool CalculateAttackType(int x1, int y1){
			Character c = OWNER.BOARD.BOARD[x1, y1];
			bool melee = c.BaseIsMelee;
			if (c == null)
				return melee;

			List<PassivePair> AP = OWNER.GatherPassives ();
			List<PassivePair> EP = OWNER.ENEMY.GatherPassives ();

			foreach (PassivePair pp in AP) {
				melee = pp.p.ModifyAttackType (melee, c, pp.c, OWNER);
			}
			foreach (PassivePair pp in EP) {
				melee = pp.p.ModifyAttackType (melee, c, pp.c, OWNER.ENEMY);
			}

			return melee;
		}

		private bool CalculateIntercept(int x1, int y1){
			Character c = OWNER.BOARD.BOARD[x1, y1];
			bool intercept = c.BaseIntercept;
			if (c == null)
				return intercept;

			List<PassivePair> AP = OWNER.GatherPassives ();
			List<PassivePair> EP = OWNER.ENEMY.GatherPassives ();

			foreach (PassivePair pp in AP) {
				intercept = pp.p.ModifyIntercept (intercept, c, pp.c, OWNER);
			}
			foreach (PassivePair pp in EP) {
				intercept = pp.p.ModifyIntercept (intercept, c, pp.c, OWNER.ENEMY);
			}

			return intercept;
		}

		private bool CalculateRooted(int x1, int y1){
			Character c = OWNER.BOARD.BOARD[x1, y1];
			bool intercept = false;
			if (c == null)
				return intercept;

			List<PassivePair> AP = OWNER.GatherPassives ();
			List<PassivePair> EP = OWNER.ENEMY.GatherPassives ();

			foreach (PassivePair pp in AP) {
				intercept = pp.p.ModifyRooted (intercept, c, pp.c, OWNER);
			}
			foreach (PassivePair pp in EP) {
				intercept = pp.p.ModifyRooted (intercept, c, pp.c, OWNER.ENEMY);
			}

			return intercept;
		}

		private bool CalculateOverkill(int x1, int y1){
			Character c = OWNER.BOARD.BOARD[x1, y1];
			bool overkill = false;
			if (c == null)
				return overkill;

			List<PassivePair> AP = OWNER.GatherPassives ();
			List<PassivePair> EP = OWNER.ENEMY.GatherPassives ();

			foreach (PassivePair pp in AP) {
				overkill = pp.p.ModifyOverkill (overkill, c, pp.c, OWNER);
			}
			foreach (PassivePair pp in EP) {
				overkill = pp.p.ModifyOverkill (overkill, c, pp.c, OWNER.ENEMY);
			}

			return overkill;
		}

		private int CalculateArmor(int x1, int y1){
			Character c = OWNER.BOARD.BOARD [x1, y1];
			int armor = c.BaseArmor;
			if (c == null)
				return armor;

			List<PassivePair> AP = OWNER.GatherPassives ();
			List<PassivePair> EP = OWNER.ENEMY.GatherPassives ();

			foreach (PassivePair pp in AP) {
				armor = pp.p.ModifyArmor (armor, c, pp.c, OWNER);
			}
			foreach (PassivePair pp in EP) {
				armor = pp.p.ModifyArmor (armor, c, pp.c, OWNER.ENEMY);
			}

			//clamp and return
			if (armor < 0)
				armor = 0;

			return armor;
		}

		private bool CalculateZombie(int x1, int y1){
			Character c = OWNER.BOARD.BOARD[x1, y1];
			bool zombie = c.BaseZombie;
			if (c == null)
				return zombie;

			List<PassivePair> AP = OWNER.GatherPassives ();
			List<PassivePair> EP = OWNER.ENEMY.GatherPassives ();

			foreach (PassivePair pp in AP) {
				zombie = pp.p.ModifyZombie (zombie, c, pp.c, OWNER);
			}
			foreach (PassivePair pp in EP) {
				zombie = pp.p.ModifyZombie (zombie, c, pp.c, OWNER.ENEMY);
			}

			return zombie;
		}

		public void CalculateUnitStats(){
			OWNER.RevealHandEffect = CalculateRevealHand ();
			for (int i = 0; i < ROWS; i++) {
				for (int a = 0; a < COLUMNS; a++) {
					if (BOARD [a, i] != null) {
						BOARD [a, i].Attack = CalculateDamage (a, i);
						BOARD [a, i].Life = CalculateLife (a, i);
						BOARD [a, i].IsMelee = CalculateAttackType(a,i);
						BOARD [a, i].Intercept = CalculateIntercept(a,i);
						BOARD [a, i].Rooted = CalculateRooted(a,i);
						BOARD [a, i].Overkill = CalculateOverkill(a,i);
						BOARD [a, i].Armor = CalculateArmor(a,i);
						BOARD [a, i].Zombie = CalculateZombie(a,i);
					}
				}
			}
		}



	}
}

