using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Move", menuName = "Move Data")]
public class MoveData : ScriptableObject, IBattleMenuOption
{
	public string moveName;
	public List<StimData> stims;

	public string GetName()
	{
		return moveName;
	}
}
