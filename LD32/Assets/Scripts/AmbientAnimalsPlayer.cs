using UnityEngine;
using System.Collections;

public class AmbientAnimalsPlayer : MonoBehaviour {

	public AudioClip[] daySounds;
	public AudioClip[] nightSounds;
	public GameObject AmbientForestSource;
	private AmbientForestToggle aft;
	private int playInterval;
	private float soundCounter;

	// Use this for initialization
	void Start () {
		aft = AmbientForestSource.GetComponent<AmbientForestToggle>();
		Random.seed = (int)System.DateTime.Now.Ticks;
		playInterval = Random.Range(6,15);
		Debug.Log (playInterval);
	}
	
	// Update is called once per frame
	void Update () {
		periodicSound();
	}

	public void periodicSound(){
		soundCounter += Time.deltaTime;
		if (soundCounter >= playInterval) {
			bool day = aft.isDay();

			if(day == true){
				int n = Random.Range(0,daySounds.Length);
				GetComponent<AudioSource>().clip = daySounds[n];
			} else {
				int n = Random.Range(0,nightSounds.Length);
				GetComponent<AudioSource>().clip = nightSounds[n];
			}

			GetComponent<AudioSource>().Play();
			soundCounter = 0f;
			playInterval = Random.Range(6,15);
			Debug.Log (playInterval);
		}
	}
}
