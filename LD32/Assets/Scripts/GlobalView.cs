using UnityEngine;
using System.Collections;
using Hamelin;

namespace Hamelin
{
	public class GlobalView : MonoBehaviour {
		
		private PathContainer pathContainer;

		public GameObject regionContainer;

		public GameObject poacher;

		public float MaxMovementSpeed = 5;
		public float MovementForce = 15;

		private float time = 0;

		void Start()
		{
			pathContainer = new PathContainer (regionContainer);
		}

		void Update()
		{
			if (Time.timeSinceLevelLoad - time > 5) {
				time = Time.timeSinceLevelLoad;
				GameObject myPoacher = GameObject.Instantiate(poacher);
				myPoacher.GetComponent<PoacherView>().controller = new PathingController(pathContainer.getPath());
			}
		}
	}
}
