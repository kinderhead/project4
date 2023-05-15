using Godot;
using System;

public partial class Level : Node2D
{
	[Export]
	public Camera2D Camera;

	[Export]
	public Vector2 Size;

	public override void _Ready()
	{
		
	}

	public override void _Process(double delta)
	{
	}
}
