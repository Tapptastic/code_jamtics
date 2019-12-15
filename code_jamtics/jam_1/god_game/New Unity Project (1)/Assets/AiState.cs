using UnityEngine;

public class AiStateWaving : State<BasicAi>
{
    public override void Update(BasicAi source)
    {
        source.State.Set(new AiStateWalking());
    }
}

public class AiStateCollectWater : State<BasicAi>
{
    public override void Update(BasicAi source)
    {
        
    }
}

public class AiStateWalking : State<BasicAi>
{
    public override void Update(BasicAi source)
    {
        
    }
}

public class AiStateGathering : State<BasicAi>
{
    public override void Update(BasicAi source)
    {
        source.State.Set(new AiStateWaving());
    }
}

public class AiStateIdle : State<BasicAi>
{
    public override void Update(BasicAi source)
    {
        source.GetFood();
        source.GetWater();
        source.Move();
    
    }
}