using System.Linq;

public class BattleChooseMoveState : State<Game>
{
	public override void Update(Game source)
	{
	}

	public override void Enter(Game source)
	{
		source.encounter.playerHuman = source.player.party[0];
		var human = source.encounter.playerHuman;

		source.ui.battle.ShowOptions("Select a move", human.data.attacks.Where(x => x.level <= human.level).Select(x => x.data), item =>
		{
			source.encounter.playerMove = (MoveData) item;
			source.state.Pop();
		});
	}

	public override void Exit(Game source)
	{
		source.ui.battle.HideOptions();
	}
}

