using Godot;
using System;

public partial class MainScript : Node
{
    private StaticBody2D _floorObject;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        var player = GetNode<PlayerCircle>("PlayerCircle");
        var startPosition = GetNode<Marker2D>("StartPosition");
        _floorObject = GetNode<StaticBody2D>("Floor");

        player.SetFloorObject((int)_floorObject.GetInstanceId());
        player.Start(startPosition.Position);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}
