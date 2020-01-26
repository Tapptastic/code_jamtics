using UnityEngine;

public class BattleState : State<Game>
{
	public override void Update(Game source)
	{
	
	}

	public override void Enter(Game source)
	{
		source.camera.transform.position = new Vector3(0, 0, -10f);

		if (source.encounter.opponentHuman.health <= 0)
		{
			source.state.Push(new BattleOpponentFaintedState());
		}
	}

	public override void Push(Game source)
	{
		source.encounter.playerAvatar.position = new Vector3(-100f, 0, 0);
		source.encounter.opponentHumanTransform.position = new Vector3(100f, 0, 0);

		source.ui.battle.Show();
		source.ui.battle.ShowContainer();
		source.state.Push(new BattleEnterState());
	}

	public override void Pop(Game source)
	{
		source.ui.battle.HideContainer();
		source.ui.battle.Hide();
	}
}
