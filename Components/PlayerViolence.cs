using System.Linq;
using Godot;
using martialvengeance.Contracts;
using martialvengeance.Enumerations;

namespace martialvengeance.Components
{
	public class PlayerViolence : Node2D, IViolence
	{
		public Action Get()
		{
			var attacks = new[] {Action.Punch};
			return attacks.FirstOrDefault(a => a.IsPressed);
		}
	}
}
