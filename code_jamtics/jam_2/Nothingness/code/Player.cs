using Godot;
using System;

public class Player : Dot
{
    public Player() : base(10.0f, 2.0f, Colors.Green, Colors.Black)
    {
    }

    private Vector2 lastMouse;
    private Camera2D camera;
    private float moveTime;
    private Node2D moveNode;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        base._Ready();
        AddUserSignal("doteaten");

        //Setup
        this.CenterNode(new Vector2(size, size));

        //Center the start label next to the player
        var startLabel = (Label)GetNode("../StartText");
        startLabel.CenterNode(new Vector2(154, -20));

        camera = (Camera2D)GetNode("../Camera2D");
        area2D.Connect("area_entered", this, "_hit");
    }

    public void _hit(Area2D entered)
    {
        var dot = entered.GetParent();
        moveTime = OS.GetTicksMsec() + 1000;
        moveNode = ((Node2D)dot);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if(moveNode != null)
        {
            var diff = (Position - moveNode.Position).Length();
            
            if(moveTime > OS.GetTicksMsec() && diff > 0.2f)
                moveNode.Position = moveNode.Position.MoveToward(Position, 1f);
            else if( diff <= 0.1f ) {
                SetSize(size + ((Dot)moveNode).size / 5);
                moveNode.GetParent().RemoveChild(moveNode);
                moveNode = null;

                EmitSignal("doteaten");
            }
        }

        //Attract towards the mouse
        if(Input.IsMouseButtonPressed((int)Godot.ButtonList.Left))
        {
            var mouse = GetGlobalMousePosition();

            Position = Position.MoveToward(mouse, delta * 200);

            lastMouse = mouse;
        }
        
        camera.Position = camera.Position.LinearInterpolate(Position, 0.3f);

        base._Process(delta);
    }
}