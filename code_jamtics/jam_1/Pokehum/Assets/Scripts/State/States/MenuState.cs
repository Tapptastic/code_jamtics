public class MenuState : State<Game>
{
	public override void Update(Game source)
	{
		if (source.input.GetAnyButtonDown())
		{
			source.state.Push(new MapState());
		}
	}

	public override void Enter(Game source)
	{
		source.ui.menu.Show();
	}

	public override void Exit(Game source)
	{
		source.ui.menu.Hide();
	}
}
