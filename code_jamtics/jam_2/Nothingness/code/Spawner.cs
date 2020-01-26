using Godot;
using System;

public class Spawner : Node2D
{
    public int spawnRate = 10;
    public float spawnTime = 0.0f;
    public float spawnSize = 5.0f;
    public KinematicBody2D player = null;
    public Camera2D camera = null;

    public override void _Ready()
    {
        camera = (Camera2D)GetNode("Camera2D");
        player = (KinematicBody2D)GetNode("Player");
        player.Connect("doteaten", this, "_DotEaten");
    }

    public void _DotEaten()
    {
        spawnSize += 0.5f;
        spawnRate += 1;
    }

    public override void _Process(float delta)
    {
        if(spawnTime < OS.GetTicksMsec())
        {
            int missCount = 0;
            for(int i = 0; i < spawnRate + missCount; i++)
            {
                var pos = camera.Position;
                var size = camera.GetViewport().Size * 2;

                var randx = Helpers.Random.RandfRange(pos.x - size.x, pos.x + size.x);
                var randy = Helpers.Random.RandfRange(pos.y - size.y, pos.y + size.y);

                if(camera.GetViewportRect().HasPoint(new Vector2(randx, randy)))
                {
                    missCount++;
                    continue;
                }

                //Random colours
                var p = Color.FromHsv(Helpers.Random.RandfRange(0, 6), 1, 1);
                //var s = Color.FromHsv(random.RandfRange(0, 6), 1, 1);

                var enemy = new Enemy(p, p, spawnSize) {
                    Position = new Vector2(randx, randy)
                };

                AddChild(enemy);
            }

            spawnTime = OS.GetTicksMsec() + 10000.0f;
        }
    }
}