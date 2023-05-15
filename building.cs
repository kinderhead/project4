using Godot;
using System;

public partial class building : Sprite2D
{
    [Export]
    public int MinimumCategory;

    public Area2D Area;

    public override void _Ready()
	{
        GetParent<Level>().RemainingBuildings++;
        Area = GetChild<Area2D>(0);
        Area.AreaEntered += i =>
        {
            if (i.GetParent() is Player player)
            {
                if (player.GetCategory() >= MinimumCategory)
                {
                    GetParent<Level>().RemainingBuildings--;
                    QueueFree();
                }
            }
        };
    }

	public override void _Process(double delta)
	{
        
	}
}
