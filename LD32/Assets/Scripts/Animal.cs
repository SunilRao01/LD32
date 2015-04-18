using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Hamelin;

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
	public bool isMoving;
	public bool isAttacking;
	private bool isShot;
	private Transform playerTransform;
	private Vector3 enemyPosition;
	private Vector3 targetPosition;

	private GameObject enemyToFollow;

	public int queueIndex;
	public bool caught;

	private float forceFollowTimer = 0;

	void Start()
	{
		playerTransform = GameObject.Find("Player").GetComponent<Transform>();
		enemyPosition = new Vector3(0, 0, 0);
	}

	void Update()
	{
		if (queueIndex <= 1)
		{
			targetPosition = playerTransform.position;
			targetPosition.y -= 1.2f * queueIndex;
		}
		else
		{
			targetPosition = playerTransform.position;
			targetPosition.y -= 1.0f * queueIndex;
		}

		if (isMoving)
		{
			GetComponent<Rigidbody2D>().AddForce((targetPosition - transform.position) * moveForce);
		}

		if (isAttacking)
		{
			if (Vector2.Distance(enemyToFollow.transform.position, transform.position) > 1.2) { enemyPosition = enemyToFollow.transform.position; }
			GetComponent<Rigidbody2D>().AddForce((enemyPosition - transform.position) * moveForce);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.name == "PingSphere" && !isMoving && !caught)
		{
			Debug.Log("You have pinged an animal");
			isAttacking = false;
			other.transform.parent.gameObject.GetComponent<Player>().addAnimal(this);
			queueIndex = other.transform.parent.gameObject.GetComponent<Player>().getCurrentAnimalQueueSize();
			isMoving = true;
			caught = true;
			forceFollowTimer = Time.timeSinceLevelLoad;
		}

		// When animal is returning, add animal back to queue
		if (other.gameObject.name == "PlayerSprite" && isShot && !isAttacking)
		{
			other.transform.parent.gameObject.GetComponent<Player>().addAnimal(this);
			isShot = false;
		}
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		// If your animal collides with an enemy
		if (other.gameObject.CompareTag("Enemy") && (Time.timeSinceLevelLoad - forceFollowTimer) > 1)
		{
			Debug.Log("An animal has hit an enemy!");

			// Don't make the player go back to the player
			StopCoroutine(waitAndComeBack());
			StopAllCoroutines();

			isAttacking = true;
			if (isMoving == true)
			{
				caught = false;
				int temp = queueIndex;
				GameObject player = GameObject.Find("Player");

				player.GetComponent<Player>().removeFromQueue(this);
				queueIndex = player.GetComponent<Player>().getCurrentAnimalQueueSize();
				player.GetComponent<Player>().queueManagementExtras();
				player.GetComponent<Player>().updateQueuePositions(temp);

			}
			isMoving = false;
			enemyToFollow = other.gameObject;
			enemyPosition = other.transform.position;

			//add set up the target container
			TargetContainer tc = other.gameObject.GetComponent<IEnemy>().targetContainer;

			tc.AddTarget(new Target(gameObject, tc));

			// TODO: Make the enemy stop moving (This was taken care of by me (Ian) if I understand correctly)
		}
	}

	public void shootAnimal(Vector3 direction)
	{
		isShot = true;
		GetComponent<Rigidbody2D>().AddForce(direction * 1500);
		StartCoroutine(waitAndComeBack());
	}

	public void setPosition(Vector3 inputPosition)
	{
		transform.position = inputPosition;
	}

	IEnumerator waitAndComeBack()
	{
		yield return new WaitForSeconds(3.0f);
		isMoving = true;

	}
}
