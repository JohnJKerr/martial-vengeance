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

		public static Animation FromMovement(Vector2 movement)
			=> movement.x == 0 ? Idle : Walk;
	}
}