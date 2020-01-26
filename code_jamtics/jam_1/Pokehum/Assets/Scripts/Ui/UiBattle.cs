using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiBattle : UiContainer
{
	private GameObject statsContainer;
	private RectTransform playerHealthBar;
	private RectTransform encounterHealthBar;

	private GameObject messageContainer;
	private Text message;

	private GameObject optionContainer;
	private Text optionMessage;
	private Menu<IBattleMenuOption> optionMenu;

	public UiBattle(Ui ui) : base(ui)
	{
	}

	public override void Init(string path)
	{
		base.Init(path);

		statsContainer = GameObject.Find("Canvas/ui-battle");
		playerHealthBar = GameObject.Find("Canvas/ui-battle/player-health/bar-wrapper/bar").GetComponent<RectTransform>();
		encounterHealthBar = GameObject.Find("Canvas/ui-battle/encounter-health/bar-wrapper/bar").GetComponent<RectTransform>();
		HideStats();

		messageContainer = GameObject.Find("Canvas/ui-battle-message");
		message = GameObject.Find("Canvas/ui-battle-message/text").GetComponent<Text>();
		HideMessage();

		optionContainer = GameObject.Find("Canvas/ui-battle-message/options");
		optionMessage = GameObject.Find("Canvas/ui-battle-message/options-message").GetComponent<Text>();
		HideOptions();

		HideContainer();
	}

	public void ShowStats()
	{
		statsContainer.SetActive(true);
	}

	public void UpdateStats(Human player, Human encounter)
	{
		playerHealthBar.localScale = new Vector3(player.health / player.healthMax, 1, 1);
		encounterHealthBar.localScale = new Vector3(encounter.health / encounter.healthMax, 1, 1);
	}

	public void HideStats()
	{
		statsContainer.SetActive(false);
	}

	public void ShowContainer()
	{
		messageContainer.SetActive(true);
	}

	public void HideContainer()
	{
		messageContainer.SetActive(false);
	}

	public void ShowOptions(string message, IEnumerable<IBattleMenuOption> options, Menu<IBattleMenuOption>.InvokeEvent callback)
	{
		optionMenu = new Menu<IBattleMenuOption>();
		optionMenu.Update(options);
		optionMenu.InvokeAction = callback;

		optionMessage.text = message;
		optionMessage.gameObject.SetActive(true);

		var i = 0;
		foreach (var option in optionMenu.List())
		{
			var btn = Object.Instantiate(ui.buttonBattleOption).GetComponent<Button>();
			var txt = btn.transform.Find("text").GetComponent<Text>();
			txt.text = option.GetName();
			btn.transform.SetParent(optionContainer.transform);

			int index = i; // closure fix
			btn.onClick.AddListener(() =>
			{
				HandleOptionClick(index);
			});
			i++;
		}

		optionContainer.SetActive(true);
	}

	public void HandleOptionClick(int index)
	{
		optionMenu.Change(index);
		optionMenu.Invoke();
	}

	public void HideOptions()
	{
		optionMenu = null;
		foreach (Transform child in optionContainer.transform)
		{
			Object.Destroy(child.gameObject);
		}
		optionMessage.gameObject.SetActive(false);
		optionContainer.SetActive(false);
	}

	public void ShowMessage(string message)
	{
		this.message.gameObject.SetActive(true);
		this.message.text = message;
	}

	public void HideMessage()
	{
		message.gameObject.SetActive(false);
	}
}

public interface IBattleMenuOption
{
	string GetName();
}