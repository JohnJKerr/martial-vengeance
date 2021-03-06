using System;
using Godot;

namespace martialvengeance.Enumerations
{
	public class State : Enumeration
	{
		public State()
		{
		}
		
		private State(int value, string name, State resetState = default) : base(value, name.ToLower())
		{
			ResetState = resetState;
		}
		
		public State ResetState { get; }
		public bool IsResettable => ResetState != null;

		public static State Idle = new State(0, nameof(Idle));
		public static State Walk = new State(1, nameof(Walk));
		public static State Jump = new State(2, nameof(Jump));
		public static State Landing = new State(3, nameof(Landing));
		public static State Landed = new State(4, nameof(Landed), Idle);
		public static State Punch = new State(5, nameof(Punch), Idle);
	}
}