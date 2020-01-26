using UnityEngine;

public class BattleEnterState : State<Game>
{
	private float timeLeft = 1f;

	public override void Update(Game source)
	{
		AnimateIn(source.encounter.playerAvatar, new Vector3(-5f, 0, 0), source.encounter.playerAvatarPosition);
		AnimateIn(source.encounter.opponentHumanTransform, new Vector3(5f, 0, 0), source.encounter.opponentHumanPosition);
		if (timeLeft <= 0)
		{
			if (source.input.GetAnyButtonDown())
			{
				source.state.Pop();
				source.state.Push(new BattleSummonState());
			}

			return;
		}

		if (source.input.GetAnyButtonDown())
			timeLeft = 0;

		timeLeft -= Time.deltaTime;
		if (timeLeft < 0)
			timeLeft = 0;
	}

	public override void Push(Game source)
	{
		source.ui.battle.ShowMessage($"You encountered a wild [{source.encounter.opponentHuman.data.typeName}]");
	}

	protected void AnimateIn(Transform target, Vector3 offset, Vector3 to)
	{
		target.transform.position = Vector3.Lerp(to + offset, to, 1f - timeLeft);
	}
}
