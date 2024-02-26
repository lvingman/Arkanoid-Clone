using Godot;
using System;

public partial class GlobalVariables : Node
{

	public static int Lives = 3;
	public static int Score = 0;
	public static float radianMultiplier = 0.00886524822f; // This value multiplied by collision gives the radian value of the bounce of the ball
	public static float radianOrigin = 0.08333333333f;

	public static float radianMultiplierBall = 0.66666666666f; //this value x 1 will give ball direction
	public static float radianOriginBall = 0.16666666666f;
}
