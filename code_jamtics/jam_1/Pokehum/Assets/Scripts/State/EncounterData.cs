using UnityEngine;

public class EncounterData
{
	public Transform playerAvatar;
	public Vector3 playerAvatarPosition;

	public Human playerHuman;
	public Transform playerHumanTransform;
	public Vector3 playerHumanPosition;
	public MoveData playerMove;

	public Human opponentHuman;
	public Transform opponentHumanTransform;
	public Vector3 opponentHumanPosition;
	public MoveRegistration opponentMove;

	public EncounterData()
	{
		playerAvatar = GameObject.Find("battle/player").transform;
		playerAvatarPosition = playerAvatar.position;
	
		playerHumanTransform = GameObject.Find("battle/player-pokehum").transform;
		playerHumanPosition = playerHumanTransform.position;

		opponentHumanTransform = GameObject.Find("battle/encounter-pokehum").transform;
		opponentHumanPosition = opponentHumanTransform.position;
	}

	public void Init(Human encounter)
	{
		this.opponentHuman = encounter;
		opponentHumanTransform.GetComponentInChildren<SpriteRenderer>().sprite = encounter.data.sprite;
	}
}
