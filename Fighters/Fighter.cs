using Godot;
using System;
using martialvengeance.Contracts;
using Animation = martialvengeance.Enumerations.Animation;

public class Fighter : KinematicBody2D
{
	private const int Gravity = 10;
	private Vector2 _movement = Vector2.Zero;

	[Export] public int Speed { get; set; }
	[Export] public int JumpSpeed { get; set; }

	private AnimatedSprite AnimatedSprite => GetNode<AnimatedSprite>(nameof(AnimatedSprite));
	private CollisionShape2D CollisionShape2D => GetNode<CollisionShape2D>(nameof(CollisionShape2D));
	private IMotion Motion => GetNode<IMotion>(nameof(Motion));

	public override void _Ready()
	{
		base._Ready();
		Motion.Speed = Speed;
		Motion.JumpSpeed = JumpSpeed;
	}

	public override void _PhysicsProcess(float delta)
	{
		base._PhysicsProcess(delta);
		var motion = Motion.Get();
		_movement.x = motion.x;
		_movement.y += motion.y;
		ApplyGravity();
		Animate();
		_movement = MoveAndSlide(_movement, Vector2.Up);
	}

	private void ApplyGravity()
	{
		var isFalling = _movement.y > 0;
		if (IsOnFloor() && isFalling) _movement.y = 0;
		_movement.y += Gravity;
	}

	private void Animate()
	{
		AnimatedSprite.Animation = Animation.FromMovement(_movement).ToString();
		if (_movement.x == 0) return;
		AnimatedSprite.FlipH = _movement.x < 0;
		CollisionShape2D.Position = new Vector2(-50 * _movement.Sign().x, CollisionShape2D.Position.y);
	}
}