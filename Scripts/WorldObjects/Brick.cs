using Godot;
using System;
using CommunityToolkit.Mvvm.Messaging;

[Tool]
public partial class Brick : StaticBody2D, IRecipient<BallHitsBrick>
{
	// Called when the node enters the scene tree for the first time.
	[Export]
	public Texture2D BrickSprite
	{
		get { return GetSprite();}
		set { SetSprite(value);}
	}


	public override void _Ready()
	{
		if (BrickSprite != null)
		{
			SetSprite(BrickSprite);
		}
	}

	private void SetSprite(Texture2D sprite)
	{
		((Sprite2D)GetNode("Sprite2D")).Texture = sprite;
	}

	private Texture2D GetSprite()
	{
		return ((Sprite2D)GetNode("Sprite2D")).Texture;
	}
	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Engine.IsEditorHint()) {
			return;
		}
	}

	//Message listener overrides
	public override void _EnterTree()
	{
		base._EnterTree();
		if (Engine.IsEditorHint()) {
			return;
		}
		StrongReferenceMessenger.Default.RegisterAll(this);
	}

	public override void _ExitTree()
	{
		base._ExitTree();
		if (Engine.IsEditorHint()) {
			return;
		}
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
