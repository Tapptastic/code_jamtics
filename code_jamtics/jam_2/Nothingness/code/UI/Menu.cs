using Godot;
using System;

public class Menu : Control
{
    public override void _Ready()
    {
        var play = GetNode("Play");
        play.Connect("pressed", this, "_OnPlay");
    }

    public void _OnPlay()
    {
        GetTree().ChangeScene("res://scene/MainScene.tscn");
    }
}
