using UnityEngine;
using System.Collections;
using Hamelin;

namespace Hamelin
{
	public class BushmanView : MonoBehaviour {

		public PathingController controller = null;
		private bool isActive = false;
		private GameObject nextNode = null;

		private float timer;
		private float walkTime;
		private float bushTime;

		private bool walking = false;

		private Vector2 direction;
		
		private GlobalView GlobalGO;
		private Rigidbody2D rigidbody;
		
		
		void Start()
		{
			GlobalGO = Camera.main.GetComponentInChildren<GlobalView> ();
			rigidbody = GetComponent<Rigidbody2D> ();
			walkTime = Random.Range (2.0f, 3.0f);
			bushTime = Random.Range (10.0f, 18.0f);
			timer = Time.timeSinceLevelLoad;
		}
		// Update is called once per frame
		void Update () {
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

				if (walking)
				{
					if (Time.timeSinceLevelLoad - timer > walkTime)
					{
						Debug.Log("B");
						walkTime = walkTime * 2/3;
						walking = false;
						timer = Time.timeSinceLevelLoad;
					}
					else
					{
						rigidbody.AddForce(movementDirection);
					}
				}
				else
				{
					if (Time.timeSinceLevelLoad - timer > bushTime)
					{
						Debug.Log ("A");
						bushTime = bushTime * 2/3;
						walking = true;
						timer = Time.timeSinceLevelLoad;
					}

				}
			}
		}
	}
}
