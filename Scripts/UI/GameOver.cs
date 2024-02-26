using Godot;
using System;

public partial class GameOver : CanvasLayer
{
	public Button TryAgain { get; set; }
	// Called when the node enters the scene tree for the first time.

	public override void _Ready()
	{
		TryAgain = (Button)GetNode("BGBlack/TryAgain");
	}

	private void OnTryAgainPressed()
	{
		Console.WriteLine("TRY AGAIN PRESSED");
		GlobalVariables.Lives = 3;
		GlobalVariables.Score = 0;
		GetTree().ReloadCurrentScene();
	}

	private void OnMainMenuPressed()
	{
		GlobalVariables.Lives = 3;
		GlobalVariables.Score = 0;
		GetTree().ChangeSceneToFile("res://Scenes/UI/MainMenu.tscn");
	}
	

	
	
	
}
