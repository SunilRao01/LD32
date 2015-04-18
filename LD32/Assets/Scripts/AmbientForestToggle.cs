using UnityEngine;
using System.Collections;


public class AmbientForestToggle : MonoBehaviour {
	public bool day;
	public AudioClip[] ambience;

	// Use this for initialization
	void Start () {
		day = true;
		GetComponent<AudioSource>().clip = ambience[0];
		GetComponent<AudioSource>().Play();
	}
	
	// Update is called once per frame
	void Update () {
		if (day == true) {
			GetComponent<AudioSource>().clip = ambience [0];
		} else {
			GetComponent<AudioSource>().clip = ambience [1];
		}

		if (GetComponent<AudioSource> ().isPlaying == false) {
			GetComponent<AudioSource>().Play();
		}
	}

	public bool isDay(){
		return day;
	}
}
