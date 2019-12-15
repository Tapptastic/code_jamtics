using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
	private Rigidbody2D _rigidbody;

	public float speed = 2f;
	[SerializeField]
	public List<Human> party = new List<Human>();

    void Start()
    {
		_rigidbody = GetComponent<Rigidbody2D>();
    }

	public void Move(Vector2 m)
	{
		var pos = new Vector2(transform.position.x, transform.position.y);
		_rigidbody.MovePosition(pos + m * speed);
	}

	public Vector3Int GetTilePosition()
	{
		return new Vector3Int(Mathf.FloorToInt(transform.position.x), Mathf.FloorToInt(transform.position.y), 0);
	}
}
