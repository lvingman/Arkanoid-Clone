using Godot;
using System;
using CommunityToolkit.Mvvm.Messaging;

public partial class HUD : CanvasLayer, IRecipient<BallHitsBrick>, IRecipient<BallFalls>
{
	//ATTRIBUTES
	private CanvasLayer GameOver { get; set; }
	public Label PlayerScore { get; set; }
	public Label HighScore { get; set; }
	public Label Stage { get; set; }
	public Label Lives { get; set; }
	
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		PlayerScore = (Label)GetNode("BGBlack/Score");
		HighScore = (Label)GetNode("BGBlack/High Score");
		Stage = (Label)GetNode("BGBlack/Stage");
		Lives = (Label)GetNode("BGBlack/Lives");
	}


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


	public void Receive(BallHitsBrick message)
	{
		if (message != null)
		{
			Console.WriteLine("Score up");
			GlobalVariables.Score += 100;
			PlayerScore.Text = "SCORE: " + (GlobalVariables.Score);
		}
	}

	public void Receive(BallFalls message)
	{
		if (message.value)
		{
			Console.WriteLine("Life down");
			GlobalVariables.Lives--;
			Lives.Text = "LIVES: " + (GlobalVariables.Lives);
			if (GlobalVariables.Lives == 0)
			{
				GameOverScreen();
			}
		}
	}

	public void GameOverScreen()
	{
		PackedScene gameOver = ResourceLoader.Load("res://Scenes/GameOver.tscn") as PackedScene;
		GameOver = gameOver.Instantiate<CanvasLayer>();
		AddChild(GameOver);
	}
}