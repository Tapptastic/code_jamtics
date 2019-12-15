using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Human", menuName = "Human Data")]
public class HumanData : ScriptableObject
{
	public string typeName;
	public Sprite sprite;
	public List<MoveRegistration> attacks;
	public List<Effect> effects;

	public Human Instantiate()
	{
		return new Human
		{
			data = this
		};
	}
}

[Serializable]
public class MoveRegistration
{
	public int level;
	public MoveData data;
}

[Serializable]
public class Effect
{
	public string stimType;
	public float strength;
}