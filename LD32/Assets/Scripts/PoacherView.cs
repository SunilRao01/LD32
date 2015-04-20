using UnityEngine;
using System.Collections;

namespace Hamelin
{
	public class PoacherView : IEnemy {

		public PathingController controller = null;
		private bool isActive = false;
		private GameObject nextNode = null;

		private Vector2 direction;

		private GlobalView GlobalGO;
		private Rigidbody2D rigidbody;

		private bool attackingDeer = false;

		void Start()
		{
			GlobalGO = Camera.main.GetComponentInChildren<GlobalView> ();
			rigidbody = GetComponent<Rigidbody2D> ();

		}
		// Update is called once per frame
		void Update () {
			
			if (attackingDeer) {
				GetComponent<Animator>().SetBool("attack", true);
				if (Vector2.Distance(targetObject.transform.position, transform.position) > 1.2) { targetPosition = targetObject.transform.position; }
				GetComponent<Rigidbody2D>().AddForce((targetPosition - transform.position + new Vector3(offset.x, offset.y)) * GlobalGO.MovementForce);

				if (!GetComponent<AudioSource>().isPlaying)
				{
					GetComponent<AudioSource>().clip = GlobalGO.getEnemyScreamsSound();
					GetComponent<AudioSource>().Play();
				}

				if (Time.timeSinceLevelLoad - timer > getAttackTime())
				{
					targetObject.GetComponent<DeerView>().takeDamage(this.getDamage());
					timer = Time.timeSinceLevelLoad;
				}
				return;
			}
			updateAttack ();
			updateTarget ();

			if (!isActive) {
				if (controller != null) {
					GameObject node = controller.getNextNode ();
					Debug.Log (node);
					transform.position = node.transform.position;
					controller.popNode ();
					nextNode = controller.getNextNode ();
					isActive = true;
				}
			}
			else {
				if (Vector3.Distance(transform.position, nextNode.transform.position) < 1) {
					controller.popNode();
					nextNode = controller.getNextNode(); //THIS NEEDS TO GET CHANGED
					if (nextNode == null) { 
						attackingDeer = true;
						targetObject = GlobalGO.deer;
						offset = Random.insideUnitCircle / 3;
						targetPosition = GlobalGO.deer.transform.position + new Vector3(offset.x, offset.y);
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
				else
				{
					rigidbody.AddForce(movementDirection);
				}
			}

			// Set poacher animations
			if (rigidbody.velocity.x != 0)
			{
				GetComponent<Animator>().SetBool("walk", true);
			}
			else
			{
				GetComponent<Animator>().SetBool("walk", false);
			}
		}
		protected override float getAttackTime()
		{
			return 1.3f;
		}
		protected override float getDamage()
		{
			return 5f;
		}
		protected override float getHealth()
		{
			return 0;
		}
		protected override void setHealth(float newHealth)
		{
			return;
		}
		protected override int getPoints()
		{
			return 10;
		}
		protected override float defenseAdjust (float damage)
		{
			return damage/1.1f;
		}
		protected override bool getExtraDamage(int special)
		{
			if (special == 2) {
				return true;
			}
			return false;
		}
	}
}