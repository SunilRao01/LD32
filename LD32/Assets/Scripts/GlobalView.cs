using UnityEngine;
using System.Collections;
using Hamelin;

namespace Hamelin
{
	public class GlobalView : MonoBehaviour {
		
		private PathContainer pathContainer;

		public GameObject regionContainer;

		public GameObject poacher;
		public GameObject bushman;

		public float MaxMovementSpeed = 5;
		public float MovementForce = 15;

		private float time = 0;
		private float nextTime = 5;

		private bool TESTING = false;



		void Start()
		{
			pathContainer = new PathContainer (regionContainer);
		}

		void Update()
		{
			if (Time.timeSinceLevelLoad - time > nextTime) {
				nextTime = Random.Range(3.0f, 7.0f);
				time = Time.timeSinceLevelLoad;
				GameObject myPoacher = GameObject.Instantiate(poacher);
				myPoacher.GetComponent<PoacherView>().controller = new PathingController(pathContainer.getPath());
			}
		}
	}
}
