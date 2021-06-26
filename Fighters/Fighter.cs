using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using martialvengeance.Contracts;
using martialvengeance.Enumerations;
using Animation = martialvengeance.Enumerations.Animation;

public class Fighter : KinematicBody2D
{
	private const int Gravity = 10;
	private Vector2 _movement = Vector2.Zero;

	[Export] public int Speed { get; set; }
	[Export] public int JumpSpeed { get; set; }

	private AnimatedSprite AnimatedSprite => GetNode<AnimatedSprite>(nameof(AnimatedSprite));
	private CollisionShape2D CollisionShape2D => GetNode<CollisionShape2D>(nameof(CollisionShape2D));
	private AnimationPlayer ActionAnimationPlayer => GetNode<AnimationPlayer>(nameof(ActionAnimationPlayer));
	private AnimationPlayer DirectionAnimationPlayer => GetNode<AnimationPlayer>(nameof(DirectionAnimationPlayer));
	private Direction CurrentDirection { get; set; } = Direction.Right;
	private IMotion Motion => GetNode<IMotion>(nameof(Motion));
	private IEnumerable<KinematicCollision2D> Collisions => 
		Enumerable.Range(0, this.GetSlideCount()).Select(this.GetSlideCollision);
	private Animation CurrentAnimation => Enumeration.FromDisplayName<Animation>(AnimatedSprite.Animation);

	public bool IsJumping => _movement.y < 0;
	public bool IsLanding => _movement.y > 0 && !IsOnFloor();
	public bool IsWalking => _movement.x != 0;
	public bool IsLanded = false;

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
		CurrentDirection = Direction.FromMovement(_movement) ?? CurrentDirection;
		Animate();
		var isLanding = IsLanding;
		_movement = MoveAndSlide(_movement, Vector2.Up);
		if (Collisions.Any(c => c.Collider is StaticBody2D) && isLanding) IsLanded = true;
	}

	public void OnAnimationFinished()
	{
		if(CurrentAnimation.Equals(Animation.Landed)) IsLanded = false;
		Animate();
	}

	private void ApplyGravity()
	{
		var isFalling = _movement.y > 0;
		if (IsOnFloor() && isFalling) _movement.y = 0;
		_movement.y += Gravity;
	}

	private void Animate()
	{
		AnimatedSprite.Animation = Animation.FromFighter(this).ToString();
		AnimatedSprite.FlipH = Direction.Left.Equals(CurrentDirection);
		var animations = ActionAnimationPlayer.GetAnimationList().ToList();
		var animation = animations.First(a =>
			a.Equals($"{CurrentAnimation}-{CurrentDirection}") || a.Equals(CurrentAnimation.ToString()));
		ActionAnimationPlayer.CurrentAnimation = animation;
		ActionAnimationPlayer.Play();
		DirectionAnimationPlayer.CurrentAnimation = CurrentDirection.ToString();
		DirectionAnimationPlayer.Play();
	}
}