using UnityEngine;
using System.Collections;

public class Nest : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.name == "Player")
		{
			// TODO: Make buttons visible

		}
	}
}
