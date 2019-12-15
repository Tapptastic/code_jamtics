using System;
using UnityEngine;

[Serializable]
public class Ui
{
	public GameObject buttonBattleOption;

	[HideInInspector]
	public UiSplash splash;
	[HideInInspector]
	public UiMenu menu;
	[HideInInspector]
	public UiMap map;
	[HideInInspector]
	public UiBattle battle;

	public Ui()
	{
		splash = new UiSplash(this);
		menu = new UiMenu(this);
		map = new UiMap(this);
		battle = new UiBattle(this);
	}

	public void Init()
	{
		splash.Init("Canvas/ui-splash");
		menu.Init("Canvas/ui-menu");
		map.Init("Grid");
		battle.Init("battle");

		splash.Hide();
		menu.Hide();
		map.Hide();
		battle.Hide();
	}
}
