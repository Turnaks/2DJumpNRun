using Godot;

public partial class Player : CharacterBody2D
{
	[Export]
	public int Health = 6;
	[Export]
	public int MaxHealth = 6;
    [Export]
    public Node2D HealthDisplay;
	[Export]
	public float MaxSpeed = 100.0f;
	[Export]
	public float Acceleration = 10.0f;
	[Export]
	public float JumpVelocity = -250.0f;

	public override void _PhysicsProcess(double delta)
	{
        move(delta);
	}

	private void move(double delta)
	{
        Vector2 velocity = Velocity;

        velocity += handleGravity(delta);

        velocity += handleJump();
        

        // Get the input direction and handle the movement/deceleration.
        Vector2 direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");
        if (direction != Vector2.Zero)
        {
            velocity.X += direction.X * Acceleration;
            velocity.X = Mathf.Clamp(velocity.X, -MaxSpeed, MaxSpeed);
        }
        else
        {
            velocity.X = Mathf.MoveToward(Velocity.X, 0, Acceleration);
        }

        Velocity = velocity;
        MoveAndSlide();
    }

    private Vector2 handleGravity(double delta)
    {
        Vector2 gravity = Vector2.Zero;
        if (!IsOnFloor())
        {
            gravity = GetGravity() * (float)delta;
        }
        return gravity;
    }

    private Vector2 handleJump()
    {
        Vector2 jump = Vector2.Zero;
        if (Input.IsActionJustPressed("jump") && IsOnFloor())
        {
            jump.Y = JumpVelocity;
        }
        return jump;
    }
}
