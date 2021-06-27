using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using martialvengeance.Contracts;
using martialvengeance.Enumerations;

public class Fighter : KinematicBody2D
{
	private const int Gravity = 10;
	private Vector2 _movement = Vector2.Zero;
	private Vector2 _motion = Vector2.Zero;

	[Export] public int Speed { get; set; }
	[Export] public int JumpSpeed { get; set; }

	private AnimatedSprite AnimatedSprite => GetNode<AnimatedSprite>(nameof(AnimatedSprite));
	private CollisionShape2D CollisionShape2D => GetNode<CollisionShape2D>(nameof(CollisionShape2D));
	private AnimationPlayer ActionAnimationPlayer => GetNode<AnimationPlayer>(nameof(ActionAnimationPlayer));
	private AnimationPlayer DirectionAnimationPlayer => GetNode<AnimationPlayer>(nameof(DirectionAnimationPlayer));
	private IMotion Motion => GetNode<IMotion>(nameof(Motion));
	private IEnumerable<KinematicCollision2D> Collisions => 
		Enumerable.Range(0, GetSlideCount()).Select(GetSlideCollision);
	private Direction CurrentDirection { get; set; } = Direction.Right;
	private State CurrentState { get; set; } = State.Idle;

	public override void _Ready()
	{
		base._Ready();
		Motion.Speed = Speed;
		Motion.JumpSpeed = JumpSpeed;
	}

	public override void _PhysicsProcess(float delta)
	{
		base._PhysicsProcess(delta);
		_motion = Motion.Get();
		_movement.x = _motion.x;
		_movement.y += _motion.y;
		ApplyGravity();
		_movement = MoveAndSlide(_movement, Vector2.Up);
		CurrentDirection = Direction.FromMovement(_movement) ?? CurrentDirection;
		CurrentState = GetState() ?? CurrentState;
		Animate();
	}

	public void OnAnimationFinished()
	{
		if (CurrentState.Equals(State.Landed)) CurrentState = State.Idle;
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
		AnimatedSprite.Animation = CurrentState.ToString();
		AnimatedSprite.FlipH = Direction.Left.Equals(CurrentDirection);
		var animations = ActionAnimationPlayer.GetAnimationList().ToList();
		var animation = animations.First(a =>
			a.Equals($"{CurrentState}-{CurrentDirection}") || a.Equals(CurrentState.ToString()));
		ActionAnimationPlayer.CurrentAnimation = animation;
		ActionAnimationPlayer.Play();
		DirectionAnimationPlayer.CurrentAnimation = CurrentDirection.ToString();
		DirectionAnimationPlayer.Play();
	}

	private State GetState()
	{
		if (_motion.y < 0) return State.Jump;
		if (_movement.y > 0 && !IsOnFloor()) return State.Landing;
		if (_motion.x != 0 && IsOnFloor()) return State.Walk;
		if (Collisions.Any(c => c.Collider is StaticBody2D) && State.Landing.Equals(CurrentState))
			return State.Landed;
		if(IsOnFloor() && _motion.x == 0 && !State.Idle.Equals(CurrentState)) return State.Idle;
		return default;
	}
}