using Godot;
using System;

public partial class PlayerCircle : CharacterBody2D
{
    [Export] public int JumpImpulse { get; set; } = -200;

    public Vector2 ScreenSize;

    private int _floorObjectId = -1;
    
    private int _jumpCount = 2;

    private KinematicCollision2D _playerCollision;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        ScreenSize = GetViewportRect().Size;
    }

    public override void _PhysicsProcess(double delta)
    {
        var velocity = Velocity;
        velocity.Y += (float)delta * 900.0f;
        Velocity = velocity;

        var motion = velocity * (float)delta;
        _playerCollision = MoveAndCollide(motion);

        if (IsOnFloor())
        {
            var zeroFall = Velocity;
            zeroFall.Y = 0;
            Velocity = zeroFall;
            _jumpCount = 2;
        }
        HandleInputs(delta);
    }
    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    public bool IsOnFloor()
    {
        if (_playerCollision != null && (int)_playerCollision.GetColliderId() == _floorObjectId)
        {
            //GD.Print("ON THE FLOOR");
            return true;
        }

        return false;
    }
    public void HandleInputs(double delta)
    {
        var velocity = Vector2.Zero;
        if (Input.IsActionPressed("move_left"))
        {
            //GD.Print("left");
            velocity.X -= 1;
        }

        if (Input.IsActionPressed("move_right"))
        {
            //GD.Print("RIGHT");
            velocity.X += 1;
        }

        if (Input.IsActionPressed("down"))
        {
            //GD.Print("down");
            velocity.Y += 1;
        }

        if (Input.IsActionJustPressed("jump"))
        {
            if (_jumpCount > 0)
            {
                velocity.Y -= (float)delta * 30000.0f;
                Velocity = velocity;
            }
            _jumpCount--;
            GD.Print(_jumpCount);
        }

        if (Input.IsActionJustReleased("jump"))
        {

        }
        if (velocity.Length() > 0)
        {
            velocity = velocity.Normalized() * 400;
        }
        Position += velocity * (float)delta;
        Position = new Vector2(
            x: Mathf.Clamp(Position.X, 0, ScreenSize.X),
            y: Mathf.Clamp(Position.Y, 0, ScreenSize.Y));
    }

    public void Start(Vector2 position)
    {
        Position = position;
    }

    public void PerformJump(ref Vector2 velocity)
    {
        var jump = new Vector2();

        MoveAndCollide(jump);
    }

    public void SetFloorObject(int floorObjectId)
    {
        _floorObjectId = floorObjectId;
    }
}
