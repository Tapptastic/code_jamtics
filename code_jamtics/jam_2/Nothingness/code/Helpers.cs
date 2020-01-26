using Godot;

public static class Helpers
{
    public static void CenterNode(this Node2D node, Vector2 size)
    {
        var v = node.GetTree().Root;
        node.Position = (v.Size / 2) - (size / 2);
    }
    
    public static void CenterNode(this Control node, Vector2 size)
    {
        var v = node.GetTree().Root;
        node.SetPosition((v.Size / 2) - (size / 2));
    }

    private static RandomNumberGenerator _random = null;

    public static RandomNumberGenerator Random { get {
        if(_random == null)
        {
            _random = new RandomNumberGenerator();
            _random.Seed = 1;
            _random.Randomize();
        }

        return _random;
    } }
}