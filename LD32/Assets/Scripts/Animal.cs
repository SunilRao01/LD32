using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Hamelin;
using System.Collections.Generic;

public class Animal : MonoBehaviour
{
	public TargetContainer targetContainer = new TargetContainer ();

	protected Target oldTarget;
	protected GameObject targetObject;
	protected Vector3 targetPosition;
	protected Vector3 targetPosition_safe;
	protected bool isFollowing = false;
	protected Vector2 offset;

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

	public int queueIndex;
	public bool caught;

	private int state = 0;

	private List<Target> selfAsTargets = new List<Target>();

	private float currentHealth = 30;
	private float timer;

	private float forceFollowTimer = 0;


	void Start()
	{
		playerTransform = GameObject.Find("Player").GetComponent<Transform>();
	}

	void Update()
	{
		updateTarget ();
		if (targetObject != null) { updateAttack (); }
		updateTarget ();

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
			if (Vector2.Distance(targetObject.transform.position, transform.position) > 1.2) { targetPosition_safe = targetObject.transform.position; }
			GetComponent<Rigidbody2D>().AddForce((targetPosition_safe - transform .position) * moveForce);
		}
	}

	protected void updateTarget()
	{
		
		Target t;
		if ((t = targetContainer.GetTarget()) != oldTarget) {
			offset = Random.insideUnitCircle / 3;
			oldTarget = t;
			if (oldTarget != null) {
				targetObject = oldTarget.getGameObject ();
				targetPosition_safe = targetObject.transform.position;
				isAttacking = true;
			} else {
				isAttacking= false;
			}
			
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.name == "PingSphere" && !isMoving && !caught)
		{
			//Debug.Log("You have pinged an animal");
			isAttacking = false;
			other.transform.parent.gameObject.GetComponent<Player>().addAnimal(this);
			queueIndex = other.transform.parent.gameObject.GetComponent<Player>().getCurrentAnimalQueueSize();
			isMoving = true;
			caught = true;
			Target t;
			while ((t = targetContainer.GetTarget()) != null)
			{
				targetContainer.RemoveTarget(t);
			}
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
		if (other.gameObject.CompareTag("Enemy") && (Time.timeSinceLevelLoad - forceFollowTimer) > 1 && isMoving)
		{
			//Debug.Log("An animal has hit an enemy!");

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

			if (targetContainer.GetTarget() == null) {
				Target myTemp1 = new Target(other.gameObject, targetContainer);
				other.gameObject.GetComponent<IEnemy>().selfAsTargets.Add(myTemp1);
				targetContainer.AddTarget(myTemp1);
			}

			//add set up the target container
			TargetContainer tc = other.gameObject.GetComponent<IEnemy>().targetContainer;

			Target myTemp = new Target(gameObject, tc);
			selfAsTargets.Add(myTemp);
			tc.AddTarget(myTemp);

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

	protected void updateAttack()
	{
		if (isAttacking && Vector2.Distance (targetPosition_safe, transform.position) < 1.2 && Time.timeSinceLevelLoad - timer > getAttackTime ()) {

			timer = Time.timeSinceLevelLoad;
			Vector2 vector = targetPosition_safe - transform.position;
			Vector2 perpendicular = new Vector2(vector.y, -vector.x);
			iTween.ShakePosition(gameObject, new Vector3(perpendicular.x, perpendicular.y) / 5, .1f);
			if (targetObject.GetComponent<IEnemy>().takeDamage(getDamage ()))
			{
 				//Debug.Log ("setting attack false: " + Time.timeSinceLevelLoad);
				isAttacking = false;
			}
		}
	}

	protected virtual float getAttackTime()
	{
		return 1;
	}
	protected virtual float getDamage()
	{
		return 10;
	}
	protected virtual float getHealth()
	{
		return currentHealth;
	}
	protected virtual void setHealth (float newHealth)
	{
		currentHealth = newHealth;
	}
	protected virtual float defenseAdjust (float damage)
	{
		return damage;
	}

	public bool takeDamage(float damage)
	{
		setHealth (getHealth() - defenseAdjust(damage));
		if (getHealth () <= 0) {
			foreach (Target target in selfAsTargets)
			{
				target.cleanReferences ();
			}
			int temp = queueIndex;
			GameObject player = GameObject.Find("Player");
			
			player.GetComponent<Player>().removeFromQueue(this);
			queueIndex = player.GetComponent<Player>().getCurrentAnimalQueueSize();
			player.GetComponent<Player>().queueManagementExtras();
			player.GetComponent<Player>().updateQueuePositions(temp);
			GameObject.Destroy (gameObject);
			return true;
		}
		return false;
	}
}
