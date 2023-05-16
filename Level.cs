using Godot;
using System;

public partial class Level : Node2D
{
	[Export]
	public Camera2D Camera;

	[Export]
	public Vector2 Size;

    [Export]
    public bool Complete = false;

    [Export]
    public int RemainingBuildings = 0;

    [Export]
    public PackedScene NextLevel;

    public override void _Ready()
	{
		GetNode<Button>("CanvasLayer/ui/complete/MarginContainer/LevelDone/Button").ButtonDown += () =>
        {
            GetTree().ChangeSceneToPacked(NextLevel);
        };
	}

	public override void _Process(double delta)
	{
        if (RemainingBuildings <= 0)
        {
            Complete = true;
            if (NextLevel == null)
            {
                GetNode<Control>("CanvasLayer/ui/gamedone").Visible = true;
            }
            else
            {
                GetNode<Control>("CanvasLayer/ui/complete").Visible = true;
            }
        }
	}
}
