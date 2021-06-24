using System;
using Godot;

namespace martialvengeance.Enumerations
{
	public class Animation : Enumeration
	{
		public Animation()
		{
		}
		
		private Animation(int value, string name) : base(value, name.ToLower())
		{
		}
		
		public static Animation Idle = new Animation(0, nameof(Idle));
		public static Animation Walk = new Animation(1, nameof(Walk));
		public static Animation Jump = new Animation(2, nameof(Jump));
		public static Animation Landing = new Animation(3, nameof(Landing));
		public static Animation Landed = new Animation(4, nameof(Landed));

		public static Animation FromFighter(Fighter fighter)
		{
			if (fighter.IsJumping) return Jump;
			if (fighter.IsLanding) return Landing;
			if (fighter.IsLanded) return Landed;
			return fighter.IsWalking ? Walk : Idle;
		}
	}
}