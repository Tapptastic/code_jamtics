using Godot;

class Enemy : Dot
{
    private float moveTime = 0;
    private Vector2 newPosition;
    private float deathTime = 0;

    public Enemy(Color primary, Color secondary, float size = 5.0f) : base(size, 1f, primary, secondary)
    {
        deathTime = OS.GetTicksMsec() + 30000;
    }

    public override void _Process(float delta)
    {
        //Die after 10 seconds
        if(deathTime < OS.GetTicksMsec())
            GetParent().RemoveChild(this);

        //Random movement (bit crap)
        if(moveTime < OS.GetTicksMsec())
        {
            newPosition = Position + MoveAndSlide(new Vector2(Helpers.Random.RandfRange(-size * 10, size * 10),  Helpers.Random.RandfRange(-size * 10, size * 10)));

            moveTime = OS.GetTicksMsec() + (size * 50);
        } else
            Position = Position.MoveToward(newPosition, delta * (size * 10));
    }
}