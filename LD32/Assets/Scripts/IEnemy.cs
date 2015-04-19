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
		protected float currentHealth = 15;

		public List<Target> selfAsTargets = new List<Target>();

		protected bool readyToDie = false;

		protected void updateTarget()
		{

			Target t;
			if ((t = targetContainer.GetTarget()) != oldTarget) {
				if (oldTarget == null)
				{
					GetComponent<AudioSource>().clip = Camera.main.GetComponentInChildren<GlobalView>().getEnemyDiscoveredSound();
					GetComponent<AudioSource>().Play ();
				}
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
		public bool takeDamage(float damage, float special)
		{
			setHealth (getHealth () - damage);
			if (getHealth () <= 0) {
				GameObject g = GameObject.Instantiate (Camera.main.GetComponentInChildren<GlobalView> ().killSpeaker);
				GetComponent<AudioSource> ().Stop ();
				g.GetComponent<AudioSource> ().clip = Camera.main.GetComponentInChildren<GlobalView> ().getEnemyKilledSound ();
				g.GetComponent<AudioSource> ().Play ();
				foreach (Target target in selfAsTargets) {
					target.cleanReferences ();
				}
				GameObject.Destroy (gameObject);
				return true;
			} else if (Random.Range (0, 9) > 7 && !GetComponent<AudioSource> ().isPlaying) {
				GetComponent<AudioSource> ().clip = Camera.main.GetComponentInChildren<GlobalView> ().getEnemyHurtSound ();
				GetComponent<AudioSource>().Play ();
			}
			return false;
		}
	}
}

