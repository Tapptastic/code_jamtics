using UnityEngine;
using UnityEngine.Tilemaps;

public class MapState : State<Game>
{
	private Vector3Int tilePosition, lastTilePosition;
	private TileBase tile;
	private Animator playerAnimator;

	public override void Update(Game source)
	{
		// keep camera in sync with player
		source.camera.transform.position = source.player.transform.position + new Vector3(0, 0, -10f);

		// move player using input
		var mx = source.input.GetAxis("Move Horizontal");
		var my = source.input.GetAxis("Move Vertical");
		var m = new Vector2(mx, my) * Time.deltaTime;

		playerAnimator.SetFloat("Horizontal", mx);
		playerAnimator.SetFloat("Vertical", my);
		playerAnimator.SetFloat("Speed", Mathf.Abs(mx) > 0.1f || Mathf.Abs(my) > 0.1f ? 1f : 0);

		source.player.Move(m);

		// get tile change
		tilePosition = source.player.GetTilePosition();
		if (lastTilePosition != tilePosition)
		{
			tile = source.tilemap.GetTile(tilePosition);
			if (tile.name == "tile-encounter")
			{
				var roll = Random.Range(1, 3);
				if (roll == 1)
				{
					source.encounter.Init(source.possibleEncounters[Random.Range(0, source.possibleEncounters.Count)].Instantiate());
					source.state.Push(new BattleState());
				}
			}
			lastTilePosition = tilePosition;
		}
	}

	public override void Push(Game source)
	{
		playerAnimator = source.player.GetComponentInChildren<Animator>();
	}

	public override void Enter(Game source)
	{
		source.ui.map.Show();
	}

	public override void Exit(Game source)
	{
		source.ui.map.Hide();
	}
}
