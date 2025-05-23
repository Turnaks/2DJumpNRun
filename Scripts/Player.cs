using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export]
	public float MaxSpeed = 100.0f;
	[Export]
	public float Acceleration = 10.0f;
	[Export]
	public float JumpVelocity = -250.0f;

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
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
}
