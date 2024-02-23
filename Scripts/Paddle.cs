using CommunityToolkit.Mvvm.Messaging;
using Godot;
using System;


namespace ArkanoidClone.Game;
public partial class Paddle : CharacterBody2D
{

//ToDo: Add Enum for Powerups
//ToDo: Change direction of ball depending on the position of the paddle where it bounces


// ##################
// ### ATTRIBUTES ###
// ##################

	public const float Speed = 800.0f;	
	private Area2D PaddleAttachable {get; set;}
	private float paddleWidth { get; set; }

// ###############
// ### METHODS ###
// ###############
	
	/// <summary>
	/// Gets the width of the paddle in order to give the paddle
	/// the functionality of manouvering the ball depending on 
	/// where on the paddle it lands
	/// </summary>
	/// <param name="paddleAttachable">
	/// Paddle Attachable Area, the Area2D 
	/// containing the sticky part
	/// </param>
	/// <returns>
	/// The width of the paddle as a float.
	/// </returns>
	public float PaddleGetWidth()
	{
		CollisionShape2D PaddleAttachableArea = (CollisionShape2D)PaddleAttachable.GetNode("CollisionShape2D");
		Rect2 PaddleAttachableAreaBox = PaddleAttachableArea.Shape.GetRect();
		paddleWidth = PaddleAttachableAreaBox.End.X - PaddleAttachableAreaBox.Position.X;
		return paddleWidth;
	}
	
	public override void _Ready()
	{
		PaddleAttachable = (Area2D)GetNode("PaddleAttachable");
		paddleWidth = PaddleGetWidth();
	}

	public override void _PhysicsProcess(double delta)
	{
		PaddleMovement(delta);
		PaddleBallSticks();
	}

/// <summary>
/// Checks if the ball enters the sticky area
/// of the Paddle
/// </summary>
	private void PaddleBallSticks()
	{
		foreach (Node2D Body in PaddleAttachable.GetOverlappingBodies())
		{
			if (Body is RigidBody2D ballSticky)
			{
				//Meant for sticky powerup

			}
			else
			{
				//Meant for sticky powerup
			}
		}
	}

/// <summary>
/// Manages the movement of the paddle
/// </summary>
/// <param name="delta">Variable for the physics of the object</param>
	private void PaddleMovement(double delta)
	{
		Vector2 velocity = Velocity;
		Vector2 direction = Input.GetVector("Left", "Right", "Up", "Down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		var collision = MoveAndCollide(velocity* (float)delta);
	
	}








}
