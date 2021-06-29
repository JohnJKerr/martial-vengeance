using Godot;

namespace martialvengeance.Enumerations
{
	public class Action : Enumeration
	{
		private Action(int value, string displayName) : base(value, displayName.ToLower())
		{
		}

		public static Action Jump = new Action(0, nameof(Jump));
		public static Action Punch = new Action(0, nameof(Punch));

		public bool IsPressed => Input.IsActionJustPressed(ToString());
	}
}