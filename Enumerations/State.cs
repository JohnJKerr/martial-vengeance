using System;
using Godot;

namespace martialvengeance.Enumerations
{
	public class State : Enumeration
	{
		public State()
		{
		}
		
		private State(int value, string name) : base(value, name.ToLower())
		{
		}
		
		public static State Idle = new State(0, nameof(Idle));
		public static State Walk = new State(1, nameof(Walk));
		public static State Jump = new State(2, nameof(Jump));
		public static State Landing = new State(3, nameof(Landing));
		public static State Landed = new State(4, nameof(Landed));
	}
}