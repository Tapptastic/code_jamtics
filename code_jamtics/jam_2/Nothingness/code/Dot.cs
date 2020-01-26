using Godot;

public class Dot : KinematicBody2D
{
    public float size;
    private float sizeTo;
    private float borderSize;
    public Color primaryColour;
    public Color secondaryColour;
    protected Area2D area2D = new Area2D();
    private CircleShape2D circleShape = new CircleShape2D();

    public Dot(float size, float borderSize, Color primary, Color secondary)
    {
        this.size = size;
        this.borderSize = borderSize;
        this.primaryColour = primary;
        this.secondaryColour = secondary;
    }

    public override void _Ready()
    {
        var shape = new CollisionShape2D();
        circleShape.Radius = size;
        shape.Shape = circleShape;
        area2D.AddChild(shape);
        AddChild(area2D);
    }

    public void SetSize(float size)
    {
        sizeTo = size;
        circleShape.Radius = size;
    }

    public override void _Process(float delta)
    {
        if(sizeTo > 0)
        {
            var diff = sizeTo - size;
            if(diff > 0)
            {
                size += (diff * delta * 10);
                Update();
            } else
                sizeTo = 0;
        }
    }

    public override void _Draw()
    {
        DrawCircle(Vector2.Zero, size + borderSize, secondaryColour);
        DrawCircle(Vector2.Zero, size, primaryColour);
    }
}