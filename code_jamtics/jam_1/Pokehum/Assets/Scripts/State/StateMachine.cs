using System.Collections.Generic;
using System.Linq;

public class StateMachine<T>
{
	private T source;
	private Stack<State<T>> state;

	public StateMachine(T source)
	{
		this.source = source;
		state = new Stack<State<T>>();
	}

	public void Update()
	{
		if (state.Any())
			state.Peek().Update(source);
	}

	public bool Any()
	{
		return state.Any();
	}

	public void Clear()
	{
		if (state.Any())
		{
			state.Peek().Exit(source);
			foreach (var s in state)
			{
				s.Pop(source);
			}
			state.Clear();
		}
	}

	public void Set(State<T> next)
	{
		Clear();
		state.Push(next);
		next.Push(source);

		next.Enter(source);
	}

	public void Push(State<T> next)
	{
		if (state.Any())
		{
			state.Peek().Exit(source);
		}

		state.Push(next);
		next.Push(source);

		next.Enter(source);
	}

	public State<T> Pop(bool skipNext = false)
	{
		if (state.Any())
		{
			state.Peek().Exit(source);
		}

		var prev = state.Pop();
		prev.Pop(source);

		if (!skipNext && state.Any())
		{
			state.Peek().Enter(source);
		}

		return prev;
	}
}