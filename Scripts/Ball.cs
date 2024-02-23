using CommunityToolkit.Mvvm.Messaging;
using Godot;
using System;

namespace ArkanoidClone.Game;

public partial class Ball : RigidBody2D, IRecipient<BallEntersStickyArea>
{

	//ATTRIBUTES
	[Export] 
	public float BallAcceleration { get; set; } = 1.1f;
	
	[Export]
	public Paddle Paddle {get; set;}
	private Sprite2D PaddleSprite {get;set;}
	private bool IsBallSticky {get; set;}
	
	const int maxSpeed = 600;
	const int minSpeed = 300;
	private Vector2 CurrentSpeed { get; set; }

	public override void _EnterTree()   //Lets to listen messages from IRecipient and the type of message emmited
    {
        base._EnterTree();
		StrongReferenceMessenger.Default.RegisterAll(this);
    }

	public override void _ExitTree()
    {
        base._ExitTree();

		StrongReferenceMessenger.Default.UnregisterAll(this); //Lets to unregister messages (For what idk)
    }




	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		PaddleSprite = (Sprite2D)Paddle.GetNode("PaddleSprite");
		BallReset();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

		var collisionInfo = MoveAndCollide(LinearVelocity * (float)delta);
		if (collisionInfo != null)
		{
			BallBounceMath(collisionInfo, BallAcceleration);
		}


	}

	//Changes to RigidBodies physics and positions are made here
    public override void _IntegrateForces(PhysicsDirectBodyState2D state)
    {
 	//If ball falls it resets to the paddle's position
		if (GlobalPosition.Y > GetViewportRect().Size.Y)
		{
			BallReset();
		}

		//Checks for sticky ball
		if (IsBallSticky)
		{
			BallStickyMovement();
		}
		
		//Gives current speed
		if (CurrentSpeed != LinearVelocity)
		{
			Console.WriteLine(LinearVelocity);
			CurrentSpeed = LinearVelocity;
		}
		
    }

/// <summary>
/// Checks if the ball is sticky in order to manipulate the ball with the paddle
/// </summary>
/// <param name="ballSticky">Checks if ball is sticky</param>


	private void BallStickyMovement()
	{
		LinearVelocity = new Vector2(0,0);
		Vector2 direction = Input.GetVector("Left", "Right", "Up", "Down");
		if (direction != Vector2.Zero)
		{
			LinearVelocity=  new Vector2(direction.X * 800.0f, 0) ;
		}
			

		if (Input.IsActionPressed("Select")){
			Console.WriteLine("Key was pressed");
			IsBallSticky = false;
			BallLaunch();
		}
	}

	/// <summary>
	/// Checks the current speed of the ball and rounds it to a lower/higher
	/// value according to what is needed
	/// </summary>
	/// <param name="ballSticky">Checks if the ball is sticky in order to avoid executing the function</param>

		
	
	/// <summary>
	/// Resets the ball position to be on top of the Paddle, while also
	/// adding an extra impulse to it
	/// </summary>
	private void BallReset()
    {
        GlobalPosition = new Vector2(Paddle.GlobalPosition.X , Paddle.GlobalPosition.Y - 30);
		IsBallSticky = true;
		Console.WriteLine("Ball is reset");

    }

	/// <summary>
	/// Pushes the ball in a (preferably) random direction.
	/// </summary>
	private void BallImpulse(){

	}


	public void Receive(BallEntersStickyArea message)
	{
		if (message.value)
		{
			Console.WriteLine("Message received");
		}
	}

	private Vector2 CheckDiagonalSpeed(Vector2 futureVelocity)
	{

		float futureXSpeed = futureVelocity.X;
		float futureYSpeed = futureVelocity.Y;

		while (Math.Abs(futureXSpeed) / Math.Abs(futureYSpeed) > 4)
		{
			futureXSpeed = futureXSpeed * 0.8f;
			futureYSpeed = futureYSpeed * 2f;
		}

		futureVelocity = new Vector2(futureXSpeed, futureYSpeed);

		return futureVelocity;
	}
	
	private void BallBounceMath(KinematicCollision2D collision, float acceleration)
	{
		if (collision.GetCollider() is Paddle paddle)
		{
			float distance = Paddle.GlobalPosition.X - GlobalPosition.X;
			float paddleWidth = +Paddle.PaddleGetWidth();
			float relation = distance / paddleWidth;
			float angle = relation * 100f;
			float angleRad = -Mathf.DegToRad(angle);

			Vector2 futureSpeed = CheckDiagonalSpeed(LinearVelocity.Bounce(collision.GetNormal()).Rotated(angleRad));





			LinearVelocity = futureSpeed * BallAcceleration;

		}
		else
		{
			Vector2 futureSpeed = CheckDiagonalSpeed(LinearVelocity.Bounce(collision.GetNormal()));
			LinearVelocity = futureSpeed;
		}

	}

	private void BallLaunch()
	{
		float radianMultiplier = 0.00886524822f; // This value multiplied by collision gives the radian value of the bounce of the ball
		float radianOrigin = 0.08333333333f; // This value represents 1pi/12, angle of the right side of the paddle
		var ballXAxis = GlobalPosition.X;
		float collision =  Paddle.GlobalPosition.X + Paddle.PaddleGetWidth() - ballXAxis;
		LinearVelocity = Vector2.Up.Rotated((collision * radianMultiplier + radianOrigin) * Mathf.Pi)*500f;
	}
	
	
}
