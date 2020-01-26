using UnityEngine;

public class BattleSummonState : State<Game>
{
	public override void Update(Game source)
	{
		if (source.input.GetAnyButtonDown())
		{
			source.state.Pop();
			source.state.Push(new BattleExecuteMoveState());
		}
	}

	public override void Enter(Game source)
	{
		source.encounter.playerHuman = source.player.party[0];
		var human = source.encounter.playerHuman;

		source.ui.battle.ShowMessage($"Go! {human.data.typeName}!");
		source.encounter.playerHumanTransform.GetComponent<Animator>().SetTrigger("Summon");

		source.ui.battle.UpdateStats(source.encounter.playerHuman, source.encounter.opponentHuman);
		source.ui.battle.ShowStats();
	}

	public override void Exit(Game source)
	{
		source.ui.battle.HideMessage();
	}
}