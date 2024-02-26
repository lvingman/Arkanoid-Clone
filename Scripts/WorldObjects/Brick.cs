using Godot;
using System;
using CommunityToolkit.Mvvm.Messaging;

public partial class Brick : StaticBody2D, IRecipient<BallHitsBrick>
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	//Message listener overrides
	public override void _EnterTree()
	{
		base._EnterTree();
		StrongReferenceMessenger.Default.RegisterAll(this);
	}

	public override void _ExitTree()
	{
		base._ExitTree();
		StrongReferenceMessenger.Default.UnregisterAll(this);
	}
	
	
	//Receives the call when the ball hits a brick
	public void Receive(BallHitsBrick brickId)
	{
		if (brickId.brickId == GetRid())
		{
			Console.WriteLine("Ball hits brick");
			QueueFree();
		}
	}
}
