using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Hamelin;

namespace Hamelin
{
	public class GlobalView : MonoBehaviour {
		
		private PathContainer pathContainer;

		public GameObject regionContainer;

		public List<GameObject> genericPoachers;
		public GameObject bushman;

		public float MaxMovementSpeed = 5;
		public float MovementForce = 15;

		private float time = 0;
		private float nextTime = 5;

		protected bool TESTING = false;

		public AudioClip[] discovered;
		public AudioClip[] damaged;
		public AudioClip[] killed;

		public GameObject killSpeaker;

		public float minEnemySpawnTime;
		public float maxEnemySpawnTime;

		public GameObject deer;

		public AudioClip[] screams;

		public int Score;

		void Start()
		{
			pathContainer = new PathContainer (regionContainer);
		}

		void Update()
		{
			if (Time.timeSinceLevelLoad - time > nextTime) 
			{
				nextTime = Random.Range(minEnemySpawnTime, maxEnemySpawnTime);
				time = Time.timeSinceLevelLoad;

				// Spawn poacher
				GameObject myPoacher = GameObject.Instantiate(genericPoachers[Random.Range (0,genericPoachers.Count)]);
				myPoacher.GetComponent<PoacherView>().controller = new PathingController(pathContainer.getPath());
			}
		}

		public AudioClip getEnemyDiscoveredSound()
		{
			return discovered[Random.Range(0,discovered.Length)];
		}

		public AudioClip getEnemyHurtSound()
		{
			return damaged[Random.Range(0,damaged.Length)];
		}

		public AudioClip getEnemyKilledSound()
		{
			return killed[Random.Range(0,killed.Length)];
		}

		public AudioClip getEnemyScreamsSound()
		{
			return screams[Random.Range(0,screams.Length)];
		}
	}
}
