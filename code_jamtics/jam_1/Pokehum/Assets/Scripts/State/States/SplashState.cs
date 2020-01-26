public class SplashState : State<Game>
{
	public override void Update(Game source)
	{
		if (source.input.GetAnyButtonDown())
		{
			source.state.Push(new MenuState());
		}
	}

	public override void Enter(Game source)
	{
		source.ui.splash.Show();
	}

	public override void Exit(Game source)
	{
		source.ui.splash.Hide();
	}
}
