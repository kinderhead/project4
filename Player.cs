using Godot;
using System;

public partial class Player : Node2D
{
	[Export]
	public Vector2 GridPos { get {
		return (Position / 256).Floor();
	} set {
		var PositionTween = GetTree().CreateTween();
		DoneMoving = false;
		PositionTween.TweenProperty(this, "position", (value * 256) + new Vector2(128, 128), .15);
		PositionTween.TweenCallback(Callable.From(() => {
			DoneMoving = true;
			PositionTween.Kill();
		}));
	}}

	protected bool DoneMoving = true;

	public override void _Ready()
	{
		Position = ((Position / 256).Floor() * 256) + new Vector2(128, 128);
	}

	public override void _Process(double delta)
	{
	}

    public override void _PhysicsProcess(double delta)
    {
        Rotate((float)(.5 * delta));
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventKey ev && DoneMoving)
		{
			if (ev.IsActionPressed("left")) GridPos += new Vector2(-1, 0);
			if (ev.IsActionPressed("right")) GridPos += new Vector2(1, 0);
			if (ev.IsActionPressed("up")) GridPos += new Vector2(0, -1);
			if (ev.IsActionPressed("down")) GridPos += new Vector2(0, 1);
		}
    }
}
