using UnityEngine;

public class BattleOpponentFaintedState : State<Game>
{
	private float countdown = 1f;

	public override void Update(Game source)
	{
		if (countdown > 0)
		{
			countdown -= Time.deltaTime;
			return;
		}

		if (source.input.GetAnyButtonDown())
		{
			source.state.Pop(true);
			source.state.Pop();
		}
	}

	public override void Enter(Game source)
	{
		source.ui.battle.ShowMessage("Opponent Fainted...");
	}

	public override void Exit(Game source)
	{
		source.ui.battle.HideMessage();
	}
}

