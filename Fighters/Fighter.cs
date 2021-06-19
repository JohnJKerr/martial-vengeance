using Godot;
using System;
using martialvengeance.Contracts;

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
        if (motion != Vector2.Zero)
        {
            AnimatedSprite.FlipH = motion < Vector2.Zero;
            CollisionShape2D.Position = new Vector2(-50 * motion.Sign().x, CollisionShape2D.Position.y);
        }
        MoveAndSlide(motion, Vector2.Up);
    }
}
