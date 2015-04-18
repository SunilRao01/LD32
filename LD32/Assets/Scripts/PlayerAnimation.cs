using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
		{
			GetComponent<Animator>().SetBool("walking", false);
		}
		else
		{
			GetComponent<Animator>().SetBool("walking", true);
		}
	}
}
