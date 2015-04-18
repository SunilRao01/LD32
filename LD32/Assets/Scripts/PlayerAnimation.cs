using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour {

	float xScale;
	Vector3 vector;
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
		}
		else
		{
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
}
