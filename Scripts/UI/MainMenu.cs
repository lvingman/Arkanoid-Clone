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

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Play._Pressed())
	}
}
