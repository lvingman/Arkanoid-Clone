using Godot;
//Messages for paddle-ball interaction
public record BallEntersStickyArea(bool value);

//Messages for ball-brick interaction
public record BallHitsBrick(Rid brickId);

//Messages for ball-ui interaction
public record BallFalls(bool value);