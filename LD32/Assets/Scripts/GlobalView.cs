using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
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

		public GameObject player;

		public int Score;

		// Wave/Timer Labels
		private int currentWave;
		private Text waveLabel;
		private Text waveTimerLabel;
		private float timer;
		private bool isCounting;

		private bool isFresh = true;

		void Awake()
		{
			waveLabel = GameObject.Find("WaveLabel").GetComponent<Text>();
			waveTimerLabel = GameObject.Find("WaveTimer").GetComponent<Text>();
		}

		void Start()
		{
			isCounting = true;
			pathContainer = new PathContainer (regionContainer);
			currentWave = 1;

			StartCoroutine(waveTimer());
		}

		void Update()
		{
			if (!isCounting) {
				if (isFresh) {
					Application.LoadLevel("ExplorationScene2");
				}
			}
			if (isCounting)
			{
				isFresh = true;
				timer += Time.deltaTime;
			}

			waveLabel.text = "Wave " + currentWave.ToString();
			float seconds = Mathf.RoundToInt(timer);
			waveTimerLabel.text = (60 - seconds).ToString();

			if (Time.timeSinceLevelLoad - time > nextTime && isCounting) 
			{
				// Set a random timed interval before spawning next 
				float minTime = minEnemySpawnTime - currentWave;
				float maxTime = maxEnemySpawnTime - currentWave;
				nextTime = Random.Range(minTime, maxTime);
				time = Time.timeSinceLevelLoad;

				// Spawn random type of poacher
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
		
		IEnumerator waveTimer()
		{
			while (true && isCounting)
			{
				yield return new WaitForSeconds(60);
				
				currentWave++;
				timer = 0;
				isCounting = false;
				
				// TODO: Give the player a 5 second breather before the next wave starts
				yield return new WaitForSeconds(60);
				
				isCounting = true;
			}
		}
	}
}
