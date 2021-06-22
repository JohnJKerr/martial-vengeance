using System.Linq;
using Godot;
using martialvengeance.Contracts;
using martialvengeance.Enumerations;

namespace martialvengeance.Components
{
	public class PlayerMotion : Node2D, IMotion
	{
		private Fighter Fighter => GetParent<Fighter>();

		public int Speed { get; set; }
		public int JumpSpeed { get; set; }

		public Vector2 Get()
		{
			var horizontal = GetHorizontalMovement();
			var x = horizontal.x * Speed;
			var vertical = GetVerticalMovement();
			var y = vertical.y * JumpSpeed;
			return new Vector2(x, y);
		}

		private static Direction GetDirectionFromInput()
			=> Enumeration.GetAll<Direction>().FirstOrDefault(d => d.IsPressed);

		private static Vector2 GetHorizontalMovement()
		{
			var direction = GetDirectionFromInput();
			return direction?.Movement ?? Vector2.Zero;
		}

		private Vector2 GetVerticalMovement() => !HasJumped ? Vector2.Zero : Vector2.Up;

		private bool HasJumped => Action.Jump.IsPressed && Fighter.IsOnFloor();
	}
}
