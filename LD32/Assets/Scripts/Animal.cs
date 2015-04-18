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

	public float moveForce;
	private bool isMoving;
	private Transform playerTransform;
	private Vector3 targetPosition;
	public int queueIndex;

	void Start()
	{
		playerTransform = GameObject.Find("Player").GetComponent<Transform>();
	}

	void Update()
	{
		targetPosition = playerTransform.position;
		targetPosition.y -= 1.5f * queueIndex;

		if (isMoving)
		{
			GetComponent<Rigidbody2D>().AddForce((targetPosition - transform.position) * moveForce);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.name == "PingSphere" && !isMoving)
		{
			Debug.Log("You have pinged an animal");
			other.transform.parent.gameObject.GetComponent<Player>().addAnimal(this);
			queueIndex = other.transform.parent.gameObject.GetComponent<Player>().getCurrentAnimalQueueSize();
			isMoving = true;
		}
	}
}
