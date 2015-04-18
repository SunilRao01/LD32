using UnityEngine;
using System.Collections;
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

		protected void updateTarget()
		{

			Target t;
			if ((t = targetContainer.GetTarget()) != oldTarget) {
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
	}
}

