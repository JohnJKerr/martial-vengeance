using Godot;

namespace martialvengeance.Contracts
{
	public interface IMotion
	{
		int Speed { get; set; }
		int JumpSpeed { get; set; }
		Vector2 Get();
	}
}