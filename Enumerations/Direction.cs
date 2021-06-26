using System;
using System.Linq;
using Godot;

namespace martialvengeance.Enumerations
{
	public class Direction : Enumeration
	{
		public Direction()
		{
		}
		
		private Direction(int value, string name, Vector2 movement) : base(value, name.ToLower())
		{
			Movement = movement;
		}
		
		public Vector2 Movement { get; }
	
		public static Direction Left = new Direction(0, nameof(Left), Vector2.Left);
		public static Direction Right = new Direction(1, nameof(Right), Vector2.Right);

		public bool IsPressed => Input.IsActionPressed(ToString());

		public static Direction FromMovement(Vector2 movement) =>
			GetAll<Direction>().FirstOrDefault(d => Math.Abs(d.Movement.x - movement.Sign().x) < 0.01);
	}
}