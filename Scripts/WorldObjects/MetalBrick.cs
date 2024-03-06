using Godot;
using System;

public partial class MetalBrick : Brick
{
	// Called when the node enters the scene tree for the first time.
	public int brickHealth = 2;


	public override void _Ready()
	{
		base._Ready();
	}

	public override void Receive(BallHitsBrick brickId)
	{
		
		if (brickId.brickId == GetRid())
		{
			brickHealth--;
			if(brickHealth == 0)
			{
				QueueFree();
			}
			else
			{
				Console.WriteLine("Metal Brick Hit");

				//Play animation for brick getting hit
			}
		}
	}

}
