using Godot;
using System;
using martialvengeance.Contracts;
using Animation = martialvengeance.Enumerations.Animation;

public class Fighter : KinematicBody2D
{
    [Export] public int Speed { get; set; }

    private AnimatedSprite AnimatedSprite => GetNode<AnimatedSprite>(nameof(AnimatedSprite));
    private CollisionShape2D CollisionShape2D => GetNode<CollisionShape2D>(nameof(CollisionShape2D));
    private IMotion Motion => GetNode<IMotion>(nameof(Motion));

    public override void _Ready()
    {
        base._Ready();
        Motion.Speed = Speed;
    }

    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);
        var motion = Motion.Get();
        Animate(motion);
        MoveAndSlide(motion, Vector2.Up);
    }

    private void Animate(Vector2 movement)
    {
        AnimatedSprite.Animation = Animation.FromMovement(movement).ToString();
        if (movement.Equals(Vector2.Zero)) return;
        AnimatedSprite.FlipH = movement < Vector2.Zero;
        CollisionShape2D.Position = new Vector2(-50 * movement.Sign().x, CollisionShape2D.Position.y);
    }
}
