using UnityEngine;
using System.Collections;
using Hamelin;

public class PlayerAnimation : MonoBehaviour {

	float xScale;
	Vector3 vector;

	public AudioClip[] singleSteps;
	public AudioClip[] groupSteps;
	
	public AnimationClip walking;
	
	private float footstepCounter = 0;

	// Use this for initialization
	void Start () 
	{
		xScale = transform.localScale.x;
		vector = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
		{
			GetComponent<Animator>().SetBool("walking", false);
			footstepCounter = 0;
		}
		else
		{
			AudioClip clip = getFootstepSound();

			if (clip != null)
			{
				GetComponent<AudioSource>().clip = clip;
				GetComponent<AudioSource>().Play();
			}
			if (Input.GetAxisRaw ("Horizontal") < 0)
			{
				transform.localScale= new Vector3(xScale * -1, vector.y, vector.z);
			}
			else
			{
				transform.localScale = new Vector3(xScale, vector.y, vector.z);
			}
			GetComponent<Animator>().SetBool("walking", true);
		}
	}

	public AudioClip getFootstepSound()
	{
		if (Time.timeSinceLevelLoad - footstepCounter > walking.length) {
			footstepCounter = Time.timeSinceLevelLoad;
			int n = Random.Range (0, singleSteps.Length);
			return singleSteps [n];
		}
		return null;
	}

}
