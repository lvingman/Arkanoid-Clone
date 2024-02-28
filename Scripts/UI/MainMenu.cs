using Godot;
using System;

public partial class MainMenu : CanvasLayer
{
	//ATTRIBUTES

	public Button Play { get; set; }
	public Button Options { get; set; }
	public Button Quit { get; set; }
	
	
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Play = (Button)GetNode("Control/Play");
		Options = (Button)GetNode("Control/Options");
		Quit = (Button)GetNode("Control/Quit");
	}


	// SIGNALS
	private void OnPlayPressed()
	{
		Console.WriteLine("PLAY IS WORKING");
		GetTree().ChangeSceneToFile("res://Scenes/Levels/Level1.tscn");
	}

	private void OnOptionsPressed()
	{
		Console.WriteLine("OPTIONS IS WORKING");
	}
	private void OnQuitPressed()
	{
		GetTree().Quit();
	}
	
	
}
