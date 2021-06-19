using Godot;

namespace martialvengeance.Contracts
{
	public interface IMotion
	{
		int Speed { get; set; }
		Vector2 Get();
	}
}