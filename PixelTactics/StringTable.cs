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
* StringTable.cs
* Author: Nic Wilson
* Last updated: 3/27/2016
*/

using System;
using System.Collections.Generic;

namespace Tactics_CoreGameEngine
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
				foreach (Character c in P.DECK.Contents) {
					if (!MASTER.ContainsKey (c.NameCode))
						MASTER.Add (c.NameCode, LOOKUP [c.NameCode]);
					for (int i = 0; i < TableTop.ROWS; i++) {
						foreach (Passive p in c.Passives[i]) {
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
						foreach (Trigger t in c.Triggers[i]) {
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
					if (c.HandAbility != null && !MASTER.ContainsKey (c.HandAbility.DescriptionCode))
						MASTER.Add (c.HandAbility.DescriptionCode, LOOKUP [c.HandAbility.DescriptionCode]);
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

			//Keywords
			D.Add("Upgrade", "Upgrade");
			D.Add("Gravedigger", "Gravedigger");

			//Passive abilities' descriptions
			D.Add ("PAD000001", "<b>Intercept</b>");
			D.Add ("PAD000002", "<b>Intercept</b><br>Life+1");
			D.Add ("PAD000003", "Attack+2");
			D.Add ("PAD000004", "Life+1\nAttack+1");
			D.Add ("PAD000005", "<b>Overkill</b>");
			D.Add ("PAD000006", "<b>Rooted</b>");
			D.Add ("PAD000007", "Attack+1");

			//Trigger abilities' descriptions
			D.Add("TA000001", "<b>Recruit:</b> Draw a card. Take 1 Damage");
			D.Add ("TA000002", "<b>Trap:</b> Do 3 Damage to a Recruited Enemy Unit");
			D.Add ("TA000003", "Attack+1 when an Enemy is Recruited");

			//Ongoing abilities' descriptions

			//Active abilities' descriptions
			D.Add("AA000001", "Draw a card");

			//Unit names
			D.Add ("UN000001", "Basic Adventurer");
			D.Add ("UN000002", "Basic Warrior");
			D.Add ("UN000003", "Basic Slime");
			D.Add ("UN000004", "Basic Ogre");
			D.Add ("UN000005", "Basic Wall");
			D.Add ("UN000010", "Commando");
			D.Add ("UN000014", "Sneaky Trapper");

			//Types
			D.Add("TYP000001", "Basic");
			D.Add("TYP000002", "Human");
			D.Add ("TYP000003", "Slime");
			D.Add ("TYP000004", "Ogre");
			D.Add ("TYP000005", "Wall");

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
	} // End StringTable class

} // End namespace

