using System;
using System.Collections.Generic;

namespace PixelTactics
{
	public class StringTable
	{
		private Dictionary<string, string> MASTERTABLE;

		private String language;
		public String LANGUAGE {
			get {
				return language;
			}
		}

		public StringTable(String L, Player P){
			this.language = L;
			Initialize (L, P);
		}

		public void Initialize(String L, Player P){
			if (MASTERTABLE == null) {
				MASTERTABLE = new Dictionary<string, string> ();

				//Add dictionaries
				MASTERTABLE = Load(L, P);
			}
		}

		private Dictionary<string, string> Load(String LanguageCode, Player P){
			Dictionary<string, string> MASTER = new Dictionary<string, string> ();
			Dictionary<string, string> LOOKUP = GetDictionary (LanguageCode);

			for (int j = 0; j < 2; j++) {
				foreach (Character c in P.DECK.Contents()) {
					if (!MASTER.ContainsKey (c.NameCode))
						MASTER.Add (c.NameCode, LOOKUP [c.NameCode]);
					for (int i = 0; i < TableTop.ROWS; i++) {
						foreach (Passive p in c.passives[i]) {
							if (!MASTER.ContainsKey (p.DescriptionCode))
								MASTER.Add (p.DescriptionCode, LOOKUP [p.DescriptionCode]);
							foreach (Ability a in p.CreatableAbilities()) {
								if (!MASTER.ContainsKey (a.DescriptionCode))
									MASTER.Add (a.DescriptionCode, LOOKUP [a.DescriptionCode]);
							}
							foreach (Character a in p.CreatableCharacters()) {
								if (!MASTER.ContainsKey (a.NameCode))
									MASTER.Add (a.NameCode, LOOKUP [a.NameCode]);
							}
						}
					}
					for (int i = 0; i < TableTop.ROWS; i++) {
						foreach (Trigger t in c.triggers[i]) {
							if (!MASTER.ContainsKey (t.DescriptionCode))
								MASTER.Add (t.DescriptionCode, LOOKUP [t.DescriptionCode]);
							foreach (Ability a in t.CreatableAbilities()) {
								if (!MASTER.ContainsKey (a.DescriptionCode))
									MASTER.Add (a.DescriptionCode, LOOKUP [a.DescriptionCode]);
							}
							foreach (Character a in t.CreatableCharacters()) {
								if (!MASTER.ContainsKey (a.NameCode))
									MASTER.Add (a.NameCode, LOOKUP [a.NameCode]);
							}
						}
					}
					if (c.order != null && !MASTER.ContainsKey (c.order.DescriptionCode))
						MASTER.Add (c.order.DescriptionCode, LOOKUP [c.order.DescriptionCode]);
				}
				P = P.ENEMY;
			}

			return MASTER;
		}

		private static Dictionary<string, string> GetDictionary(String LanguageCode){
			//Dictionary
			Dictionary<string, string> D = null;

			if (LanguageCode == "EN") D = EN ();
			if (LanguageCode == "ES") D = ES ();

			if (D == null)
				D = EN ();

			return D;
		}

		private static Dictionary<string, string> EN(){
			//Dictionary
			Dictionary<string, string> D = new Dictionary<string, string> ();

			//null value
			D.Add("FOOBAR", "(none)");

			//Passive abilities' descriptions
			D.Add ("PA000001", "<b>Intercept</b>");

			//Trigger abilities' descriptions

			//Ongoing abilities' descriptions

			//Unit names
			D.Add ("UN000001", "Adventurer");
			D.Add ("UN000002", "Warrior");
			D.Add ("UN000003", "Beefcake");
			D.Add ("UN000004", "Citizen");

			return D;
		}

		private static Dictionary<string, string> ES(){
			//Dictionary
			Dictionary<string, string> D = new Dictionary<string, string> ();

			//null value
			D.Add("FOOBAR", "(ninguna)");

			//Passive abilities' descriptions
			D.Add ("PA000001", "<b>Intercepción</b>");

			//Trigger abilities' descriptions

			//Ongoing abilities' descriptions

			//Unit names
			D.Add ("UN000001", "Aventurero");
			D.Add ("UN000002", "Guerrero");
			D.Add ("UN000003", "Machote");
			D.Add ("UN000004", "Ciudadano");

			return D;
		}

		public String Get(String C){
			return MASTERTABLE [C];
		}
	}
}

