using System.Linq;
using Godot;
using martialvengeance.Contracts;
using martialvengeance.Enumerations;

namespace martialvengeance.Components
{
	public class PlayerMotion : Node2D, IMotion
	{
		public int Speed { get; set; }
		public Vector2 Get()
		{
			var direction = GetDirectionFromInput();
			if (direction == null) return Vector2.Zero;
			return direction.Movement * Speed;
		}

		private static Direction GetDirectionFromInput()
			=> Enumeration.GetAll<Direction>().FirstOrDefault(d => d.IsPressed);
	}
}
