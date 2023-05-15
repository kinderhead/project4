using Godot;
using System;

public partial class Player : Node2D
{
	[Export]
	public Vector2I GridPos { get {
		return (Vector2I)(Position / 256).Floor();
	} set {
		value = value.Clamp((Vector2I)(-Level.Size), (Vector2I)(Level.Size - new Vector2(1, 1)));
		var PositionTween = GetTree().CreateTween();
		DoneMoving = false;
		PositionTween.TweenProperty(this, "position", (value * 256) + new Vector2(128, 128), .15);
		PositionTween.TweenCallback(Callable.From(() => {
			DoneMoving = true;
			PositionTween.Kill();
			ProcessTile();
		}));
	}}

	[Export]
	public Level Level;

	[Export]
	public TileMap Map;

	[Export]
	public Control UI;

	[Export]
	public Label CategoryLabel;

	[Export]
	public double Power;

	protected bool DoneMoving = true;

	public override void _Ready()
	{
		Position = ((Position / 256).Floor() * 256) + new Vector2(128, 128);
		CategoryLabel = UI.GetNode<Label>("categorycontainer/category");
    }

	public override void _Process(double delta)
	{
		if (Power < 10) Reset();

		Power = Math.Clamp(Power, 0, 100);
		Scale = new Vector2((float)(Power / 100), (float)(Power / 100)) * 7.5f;

    	CategoryLabel.Text = $"Category {GetCategory()} Hurricane";

		Rotate((float)(.5 * delta));
	}

    public override void _Input(InputEvent @event)
    {
        if (!Level.Complete && @event is InputEventKey ev)
		{
			if (DoneMoving)
			{
				if (ev.IsActionPressed("left")) GridPos += new Vector2I(-1, 0);
				if (ev.IsActionPressed("right")) GridPos += new Vector2I(1, 0);
				if (ev.IsActionPressed("up")) GridPos += new Vector2I(0, -1);
				if (ev.IsActionPressed("down")) GridPos += new Vector2I(0, 1);
			}

			if (ev.IsActionPressed("reset")) Reset();
		}
    }

	public void Reset() 
	{
		GetTree().ReloadCurrentScene();
	}

	public void ProcessTile()
	{
        if (Map.GetCellAtlasCoords(0, GridPos) != new Vector2I(-1, -1)) Power -= 15;
		else if (Map.GetCellAtlasCoords(1, GridPos) == new Vector2I(0, 0)) Power -= 5;

		if (Map.GetCellAtlasCoords(1, GridPos) == new Vector2I(1, 0))
		{
			Power += 10;
			Map.SetCell(1, GridPos, 1, new(0, 0));
		}
        
    }

	public int GetCategory()
	{
		if (Power <= 10) return 0;
		if (Power <= 20) return 1;
		if (Power <= 35) return 2;
		if (Power <= 55) return 3;
		if (Power <= 75) return 4;
		if (Power <= 90) return 5;
		return -1;
	}
}
