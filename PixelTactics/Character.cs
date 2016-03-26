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
 * Character.cs
 * Author: Nic Wilson
 * Last updated: 3/26/2016
 */

using System;
using System.Collections.Generic;

namespace PixelTactics
{
	public class Character
	{

		protected int maxhealth, damage, attack;
		private int _attack, _maxhealth;
		protected int tattack, tmaxhealth;
		private bool isdead = false;
		protected string name;
		protected bool melee, tmelee;
		protected bool stunned = false;
		public bool Stunned{
			get{
				return stunned;
			}
			set{
				if (!IMMUNITIES.Contains (PixelTactics.Damage.TYPE.STUN))
					stunned = value;
			}
		}
		protected bool intercept = false, tintercept = false, rooted = false, trooted = false,
			overkill = false, toverkill = false, zombie = false, tzombie = false;

		List<Damage.TYPE> IMMUNITIES = new List<PixelTactics.Damage.TYPE> ();

		public Player CONTROLLER;

		public List<Passive>[] passives = new List<Passive>[2];
		protected List<Passive>[] _passives = new List<Passive>[2];
		public Ability order;
		public List<Trigger>[] triggers = new List<Trigger>[2];
		protected List<Trigger>[] _triggers = new List<Trigger>[2];

		public bool CanTrap = false;

		protected List<String> types, _types;

		public bool BaseIntercept{
			get{
				return intercept;
			}
		}
		public bool BaseRooted {
			get {
				return rooted;
			}
		}
		public bool BaseOverkill {
			get {
				return overkill;
			}
		}
		public bool Dead{
			get{
				return isdead;
			}
		}

		public bool Rooted{
			get{
				return trooted;
			}
		}

		public bool Overkill{
			get{
				return toverkill;
			}
		}

		public bool Zombie {
			get {
				return tzombie;
			}
		}

		public bool Upgrade = false;
		public int UpgradePlus = 0;
		public bool Gravedigger = false;
		public int GravediggerPlus = 0;
		public int armor = 0, tarmor = 0;

		public string Name{
			get{
				return CONTROLLER.TABLE.STRINGTABLE.Get (name);
			}
		}

		public string NameCode{
			get{
				return name;
			}
		}

		public void ResetBaseStats(){
			attack = _attack;
			maxhealth = _maxhealth;
			armor = 0;
			zombie = false;
			intercept = false;
			rooted = false;
			overkill = false;
			IMMUNITIES.Clear ();
			types = _types;
			passives = _passives;
			triggers = _triggers;
		}

		public void AddImmunity(Damage.TYPE type){
			if(!IMMUNITIES.Contains(type))
				IMMUNITIES.Add (type);
		}

		public void RemoveImmunity(Damage.TYPE type){
			IMMUNITIES.Remove (type);
		}

		public int Armor {
			get {
				return (tarmor>0)?tarmor:0;
			}
		}
		public int BaseArmor {
			get {
				return armor;
			}
		}

		public int Attack{
			get{
				return tattack;
			}
		}
		public int BaseAttack{
			get{
				return attack;
			}
		}
		public int Life{
			get{
				return tmaxhealth;
			}
		}
		public int RemainingLife {
			get {
				int i = tmaxhealth - damage;
				return (i < 0) ? 0 : i;
			}
		}
		public int BaseLife{
			get{
				return maxhealth;
			}
		}
		public bool BaseZombie{
			get{
				return zombie;
			}
		}
		public void SetAttack(int a){
			this.tattack = a;
		}
		public void SetLife(int l){
			this.tmaxhealth = l;
		}
		public void SetAttackType(bool b){
			this.tmelee = b;
		}
		public void SetIntercept(bool b){
			this.tintercept = b;
		}
		public void SetRooted(bool b){
			this.trooted = b;
		}
		public void SetOverkill(bool b){
			this.toverkill = b;
		}
		public void SetArmor(int a){
			this.tarmor = a;
		}
		public void SetZombie(bool b){
			this.tzombie = b;
		}

		public bool Melee {
			get {
				return tmelee;
			}
		}
		public bool BaseMelee{
			get{
				return melee;
			}
		}
		public bool Intercept{
			get{
				return tintercept;
			}
		}
		public int Damage{
			get{
				return damage;
			}
		}

		public bool moved = true;

		public Character (Player CONTROLLER, string name, int attack, int maxhealth, params string[] typearray)
		{
			this.CONTROLLER = CONTROLLER;
			this.name = name;
			this.attack = attack;
			this._attack = attack;
			this.tattack = attack;
			this.maxhealth = maxhealth;
			this.tmaxhealth = maxhealth;
			this._maxhealth = maxhealth;
			this.melee = true;
			this.tmelee = true;
			this.types = new List<string>(typearray);
			this.types = new List<string> (typearray);

			triggers [0] = new List<Trigger> ();
			triggers [1] = new List<Trigger> ();
			_triggers [0] = new List<Trigger> ();
			_triggers [1] = new List<Trigger> ();
			passives [0] = new List<Passive> ();
			passives [1] = new List<Passive> ();
			_passives [0] = new List<Passive> ();
			_passives [1] = new List<Passive> ();
		}

		public override string ToString ()
		{
			if (Dead)
				return "CORPSE";
			return "Name: " + Name + "\t" +
				"ATK/HP/DMG: " + tattack + (tmelee?"":"R") + (overkill?"O":"") + " " +
				tmaxhealth + (intercept?"I":"") + (Armor>0?" "+Armor+"A":"") + " " + damage;
		}

		public void AddType(string t){
			if(!types.Contains(t))
				types.Add(t);
		}

		public void RemoveType(string t){
			types.Remove (t);
		}

		public void AddPassive(Passive p, int r){
			this.passives [r].Add(p);
		}

		public void AddTrigger(Trigger t, int r){
			this.triggers [r].Add(t);
		}

		public void RemovePassive(Passive p, int r){
			this.passives [r].Remove(p);
		}

		public void RemoveTrigger(Trigger t, int r){
			this.triggers [r].Remove(t);
		}

		protected void _AddHandAbility(Ability a){
			this.order = a;
		}

		protected void _AddPassive(Passive p, int r){
			this.passives [r].Add(p);
			this._passives [r].Add(p);
		}

		protected void _AddTrigger(Trigger t, int r){
			this.triggers [r].Add(t);
			this._triggers [r].Add(t);
		}

		public void AddDamage(Damage D){
			if (IMMUNITIES.Contains (D.type))
				return;

			int DV = D.VALUE - Armor;

			if (D.type == PixelTactics.Damage.TYPE.SPELL) DV = D.VALUE;
			
			if (DV < 0)
				DV = 0;
			damage += DV;
			if(DV > 0)
				CONTROLLER.TABLE.PIPELINE.Add (new TriggerPacket (Trigger.TYPE.UNITDAMAGE, CONTROLLER, D.SOURCE, null, this));
		}

		public void HealDamage(Damage D){
			if (Zombie)
				D.VALUE *= -1;
			if (IMMUNITIES.Contains (D.type))
				return;
			damage -= D.VALUE;
			if (damage < 0)
				damage = 0;
			CONTROLLER.TABLE.PIPELINE.Add (new TriggerPacket (Trigger.TYPE.UNITHEAL, CONTROLLER, D.SOURCE, null, this));
		}

		public void CheckDead(){
			if (damage >= maxhealth) {
				if (!isdead) { //just died
					//Make trigger for dying
					CONTROLLER.TABLE.PIPELINE.Add(new TriggerPacket(Trigger.TYPE.UNITDIED,CONTROLLER,this,CONTROLLER));
					CONTROLLER.TABLE.PIPELINE.Add(new TriggerPacket(Trigger.TYPE.CREATECORPSE,CONTROLLER,this,CONTROLLER));
					ResetBaseStats ();
				}
				isdead = true;
			}
		}

		public void ModifyBaseAttack(int n){
			this.attack = n;
		}


	} // End Character class

} // End namespace

