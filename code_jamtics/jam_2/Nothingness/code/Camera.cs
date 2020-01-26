using Godot;
using System;

public class Camera : Camera2D
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        this.CenterNode(Vector2.Zero);
    }
}
