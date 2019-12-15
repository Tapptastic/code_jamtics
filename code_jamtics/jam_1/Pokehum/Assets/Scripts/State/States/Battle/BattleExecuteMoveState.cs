using System.Linq;
using UnityEngine;

public class BattleExecuteMoveState : State<Game>
{
	private float countdown = 1f;
	private Human opponent;

	public override void Update(Game source)
	{
		countdown -= Time.deltaTime;
		if (countdown <= 0 || source.input.GetAnyButtonDown())
		{
			source.state.Pop();
			source.state.Push(new BattleExecuteMoveState());
		}
	}

	public override void Enter(Game source)
	{
		if (source.encounter.playerMove == null)
		{
			source.state.Push(new BattleChooseMoveState());
			return;
		}

		opponent = source.encounter.opponentHuman;
		var move = source.encounter.playerMove;
		var power = 0f;
		for (var i = 0; i < move.stims.Count; i++)
		{
			var mult = 1f;
			var effect = opponent.data.effects.FirstOrDefault(x => x.stimType == move.stims[i].typeName);
			if (effect != null)
				mult = effect.strength;

			power += move.stims[i].strength * mult;
		}

		// apply effect
		opponent.health -= power;
		if (opponent.health <= 0)
		{
			opponent.health = 0;
		}

		// reset
		source.encounter.playerMove = null;

		// update ui
		source.ui.battle.UpdateStats(source.encounter.playerHuman, source.encounter.opponentHuman);
		source.ui.battle.ShowMessage($"Your {source.encounter.playerHuman.data.typeName} used {move.moveName}");
	}

	public override void Exit(Game source)
	{
		source.ui.battle.HideMessage();
	}
}
