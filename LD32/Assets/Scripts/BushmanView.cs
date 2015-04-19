using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Hamelin;

namespace Hamelin
{
	public class BushmanView : IEnemy {

		public PathingController controller = null;
		private bool isActive = false;
		private GameObject nextNode = null;

		private float timer_safe;
		private float walkTime;
		private float bushTime;

		private bool walking = false;

		private Vector2 direction;
		
		private GlobalView GlobalGO;
		private Rigidbody2D rigidbody;

		private List<GameObject> targets;
		
		void Start()
		{
			GlobalGO = Camera.main.GetComponentInChildren<GlobalView> ();
			rigidbody = GetComponent<Rigidbody2D> ();
			walkTime = Random.Range (2.0f, 3.0f);
			bushTime = Random.Range (10.0f, 18.0f);
			timer_safe = Time.timeSinceLevelLoad;
		}
		// Update is called once per frame
		void Update () {
			updateAttack ();
			updateTarget ();

			if (!isActive) {
				if (controller != null) {
					GameObject node = controller.getNextNode();
					Debug.Log (node);
					transform.position = node.transform.position;
					controller.popNode ();
					nextNode = controller.getNextNode();
					isActive = true;
				}
			} 
			else {
				if (Vector3.Distance(transform.position, nextNode.transform.position) < 1) {
					controller.popNode();
					nextNode = controller.getNextNode(); //THIS NEEDS TO GET CHANGED
					if (nextNode == null) { 
						isActive = false;
						Destroy (this.gameObject); 
						return;
					} 
				}
				Vector2 movementDirection = new Vector2(0, 0);
				
				if (rigidbody.velocity.x < GlobalGO.MaxMovementSpeed && rigidbody.velocity.y < GlobalGO.MaxMovementSpeed)
				{
					movementDirection = nextNode.transform.position - transform.position;
					movementDirection = movementDirection.normalized * GlobalGO.MovementForce;
				}

				if (isFollowing)
				{
					if (Vector2.Distance(targetObject.transform.position, transform.position) > 1.2) { targetPosition = targetObject.transform.position; }
					GetComponent<Rigidbody2D>().AddForce((targetPosition - transform.position + new Vector3(offset.x, offset.y)) * GlobalGO.MovementForce);	
				}
				else if (walking)
				{
					if (Time.timeSinceLevelLoad - timer_safe > walkTime)
					{
						Debug.Log("B");
						walkTime = walkTime * 2/3;
						walking = false;
						timer_safe = Time.timeSinceLevelLoad;
					}
					else
					{
						rigidbody.AddForce(movementDirection);
					}
				}
				else
				{
					if (Time.timeSinceLevelLoad - timer_safe > bushTime)
					{
						Debug.Log ("A");
						bushTime = bushTime * 2/3;
						walking = true;
						timer_safe = Time.timeSinceLevelLoad;
					}

				}
			}
		}

		void AddEnemyToList (GameObject go)
		{
			targets.Add (go);
		}

		protected float getAttackTime()
		{
			return 1.2f;
		}
		protected float getDamage()
		{
			return 2f;
		}
		protected float getHealth()
		{
			return currentHealth;
		}
		protected void setHealth(float newHealth)
		{
			currentHealth = newHealth;
		}
		protected int getPoints()
		{
			return 15;
		}
		protected float defenseAdjust (float damage)
		{
			return damage/.9f;
		}
		protected bool getExtraDamage(int special)
		{
			if (special == 0) {
				return true;
			}
			return false;
		}
	}
}
