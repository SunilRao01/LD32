using UnityEngine;
using System.Collections;

public class Animal : MonoBehaviour
{
	// Animal Stats
	public int health;
	public int attackDamage;
	public int defense;
	public float damageMultiplier;

	public string strongAgainst;
	public string weakAgainst;

	private bool isMoving;

	void Start()
	{

	}

	void Update()
	{
		if (isMoving)
		{
			GetComponent<Rigidbody2D>().AddForce(Vector2.right * 10);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.name == "PingSphere")
		{
			Debug.Log("You have pinged an animal");
			isMoving = true;
		}
	}
}
