using System;
using System.Collections.Generic;
using System.Linq;

public class Menu<T>
{
	protected int index;
	protected List<T> children;

	public delegate void ChangeEvent(int to, int from);
	public delegate void InvokeEvent(T item);

	public event ChangeEvent Changed;
	public event ChangeEvent ChangeFailed;

	public InvokeEvent InvokeAction;
	public event InvokeEvent Invoked;

	public Menu()
	{
		children = new List<T>();
	}

	public void Add(T child)
	{
		children.Add(child);
	}

	public void Update(IEnumerable<T> children)
	{
		index = 0;
		this.children = children.ToList();
	}

	public List<T> List()
	{
		return children;
	}

	public void Prev()
	{
		var from = index;
		var to = index - 1;

		for (var i = to; i >= 0; i--)
		{
			if (IsActive(children[i]))
			{
				Change(i);
				return;
			}
		}

		ChangeFailed?.Invoke(to, from);
	}

	public void Next()
	{
		var from = index;
		var to = index + 1;

		for (var i = to; i < children.Count; i++)
		{
			if (IsActive(children[i]))
			{
				Change(i);
				return;
			}
		}

		ChangeFailed?.Invoke(to, from);
	}

	public void Change(int to)
	{
		var from = index;

		if (index < 0 || to > children.Count - 1 || !IsActive(children[to]))
		{
			ChangeFailed?.Invoke(to, from);
			return;
		}

		index = to;
		Changed?.Invoke(to, from);
	}

	public void ChangeFirst(Func<T, bool> predicate)
	{
		Change(children.FindIndex(x => IsActive(x) && predicate(x)));
	}

	public void Invoke()
	{
		var item = children[index];
		InvokeAction?.Invoke(item);
		Invoked?.Invoke(item);
	}

	public virtual bool IsActive(T item)
	{
		return true;
	}

	public override string ToString()
	{
		var output = "";
		for (var i = 0; i < children.Count; i++)
		{
			var item = children[i];
			var indent = "- ";
			if (index == i)
				indent = "> ";

			output += $"{indent}{item}\n";
		}
		return output;
	}
}