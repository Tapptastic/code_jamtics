using Rewired;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Game : MonoBehaviour
{
	[HideInInspector]
	public Rewired.Player input;
	[HideInInspector]
	public new Camera camera;
	public Monster player;
	public Ui ui = new Ui();
	public Tilemap tilemap;
	public EncounterData encounter;
	public List<HumanData> possibleEncounters;

	public StateMachine<Game> state;

    void Start()
	{
		input = ReInput.players.GetPlayer(0);
		camera = Camera.main;
		encounter = new EncounterData();
		
		ui.Init();

		state = new StateMachine<Game>(this);
		state.Set(new MapState());
    }

    void Update()
    {
		state?.Update();
    }
}