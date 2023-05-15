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
		
	}

	public override void _Process(double delta)
	{
        if (RemainingBuildings <= 0)
        {
            GD.Print("Done");
            Complete = true;
            GetNode<Control>("CanvasLayer/ui/complete").Visible = true;
            GetNode<Button>("CanvasLayer/ui/complete/MarginContainer/Level done/Button").ButtonDown += () =>
            {
                GetTree().ChangeSceneToPacked(NextLevel);
            };
        }
	}
}
