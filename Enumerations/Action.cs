using Godot;

namespace martialvengeance.Enumerations
{
	public class Action : Enumeration
	{
		private Action(int value, string displayName) : base(value, displayName.ToLower())
		{
		}

		public static Action Jump = new Action(0, nameof(Jump));

		public bool IsPressed => Input.IsActionJustPressed(ToString());
	}
}