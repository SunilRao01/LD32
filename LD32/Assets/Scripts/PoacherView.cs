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

		void Start()
		{
			GlobalGO = Camera.main.GetComponentInChildren<GlobalView> ();
			rigidbody = GetComponent<Rigidbody2D> ();

		}
		// Update is called once per frame
		void Update () {

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
					GetComponent<Rigidbody2D>().AddForce((targetPosition - transform.position) * GlobalGO.MovementForce);
				}
				else
				{
					rigidbody.AddForce(movementDirection);
				}
			}
		}
	}
}