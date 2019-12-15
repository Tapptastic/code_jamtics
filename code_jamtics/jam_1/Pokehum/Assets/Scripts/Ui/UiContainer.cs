using UnityEngine;

public class UiContainer
{
	public Ui ui;
	public GameObject container;

	public UiContainer(Ui ui)
	{
		this.ui = ui;
	}

	public virtual void Init(string path)
	{
		container = GameObject.Find(path);
	}

	public void Show()
	{
		container.gameObject.SetActive(true);
	}

	public void Hide()
	{
		container.gameObject.SetActive(false);
	}
}
