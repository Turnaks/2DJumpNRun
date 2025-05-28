using Godot;
using System;

public partial class MainCamera2D : Camera2D
{
	[Export]
	public Node2D Target;

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);

		GlobalPosition = Target.GlobalPosition;
	}
}
