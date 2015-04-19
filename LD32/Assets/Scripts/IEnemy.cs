using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Hamelin;

namespace Hamelin
{
	public class IEnemy : MonoBehaviour
	{
		public TargetContainer targetContainer = new TargetContainer ();

		protected Target oldTarget;
		protected GameObject targetObject;
		protected Vector3 targetPosition;
		protected bool isFollowing = false;
		protected Vector2 offset;

		protected float timer;
		private float currentHealth = 15;

		public List<Target> selfAsTargets = new List<Target>();

		protected void updateTarget()
		{

			Target t;
			if ((t = targetContainer.GetTarget()) != oldTarget) {
				offset = Random.insideUnitCircle / 3;
				oldTarget = t;
				if (oldTarget != null) {
					targetObject = oldTarget.getGameObject ();
					targetPosition = targetObject.transform.position;
					isFollowing = true;
				} else {
					isFollowing = false;
				}

			}
		}
		protected void updateAttack()
		{
			if (isFollowing && Vector2.Distance (targetPosition, transform.position) < 1.2 && Time.timeSinceLevelLoad - timer > getAttackTime ()) {
				timer = Time.timeSinceLevelLoad;
				if (targetObject.GetComponent<Animal>().takeDamage(getDamage ()))
				{
					isFollowing = false;
				}
			}
		}

		protected virtual float getAttackTime()
		{
			return 1;
		}
		protected virtual float getDamage()
		{
			return 5;
		}
		protected virtual float getHealth()
		{
			return currentHealth;
		}
		protected virtual void setHealth(float newHealth)
		{
			currentHealth = newHealth;
		}
		public bool takeDamage(float damage)
		{
			setHealth (getHealth () - damage);
			if (getHealth () <= 0) {
				foreach (Target target in selfAsTargets)
				{
					target.cleanReferences ();
				}
				GameObject.Destroy (gameObject);
				return true;
			}
			return false;
		}
	}
}

