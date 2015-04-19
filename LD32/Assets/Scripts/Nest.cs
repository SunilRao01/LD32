using UnityEngine;
using System.Collections;

public class Nest : MonoBehaviour 
{
	private bool canTransfer;
	private int currentSize;
	private GameObject playerObject;

	void Awake()
	{
		playerObject = GameObject.Find("Player");
	}

	void Start () 
	{
		GetComponent<BlendColors>().enabled = false;
	}
	
	void Update () 
	{
		if (canTransfer)
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				currentSize++;

				// TODO: move animal
				iTween.MoveTo(playerObject.GetComponent<Player>().getAnimal(0).gameObject, transform.position, 1.0f);
				// TODO: If animal is first in nest, add label to nest for that animal type
			}
		}
	}

	public int getSize()
	{
		return currentSize;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.name == "Player")
		{
			GetComponent<BlendColors>().enabled = true;

			canTransfer = true;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.name == "Player")
		{
			GetComponent<BlendColors>().enabled = false;
			GetComponent<SpriteRenderer>().color = Color.white;

			canTransfer = false;
		}
	}
}
