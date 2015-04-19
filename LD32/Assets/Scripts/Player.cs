using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Player : MonoBehaviour 
{
	// Movement Variables
	public float movementForce;
	public float maxMovementSpeed;
	private Rigidbody2D rigidbody;

	// Aiming and pinging
	private GameObject aimerObject;
	private GameObject pingObject;
	private Vector3 originalPingScale;
	public float pingRange;
	private bool isCalling;

	// Animal Queue
	private List<Animal> animalQueue;
	public int maxAnimalQueueSize;
	private GameObject animalQueuePortraits;

	// Animal portraits
	public Sprite squirrelSprite;
	public Sprite birdSprite;

	void Awake()
	{
		rigidbody = GetComponent<Rigidbody2D>();
		aimerObject = transform.FindChild("AimerPivot").gameObject;
		pingObject = transform.FindChild("PingSphere").gameObject;
		pingObject.GetComponent<CircleCollider2D>().enabled = false;
		originalPingScale = pingObject.transform.localScale;

		// Ignore Animal/Animal and Player/Animal collision
		Physics2D.IgnoreLayerCollision(8, 9);
		Physics2D.IgnoreLayerCollision(9, 9);
	}

	void Start () 
	{
		animalQueuePortraits = GameObject.Find("ScreenCanvas");

		pingObject.GetComponent<MeshRenderer>().enabled = false;

		animalQueue = new List<Animal>();
	}
	
	void Update () 
	{
		handleMovement();
		handleAimer();
		handleAnimalCall();
		handleShooting();

		//Debug.Log("Animals in queue: " + animalQueue.Count.ToString());
	}

	void handleMovement()
	{
		Vector2 movementDirection = new Vector2(0, 0);

		if (movementDirection.x < maxMovementSpeed && movementDirection.y < maxMovementSpeed)
		{
			movementDirection.x = Input.GetAxisRaw("Horizontal") * movementForce;
			movementDirection.y = Input.GetAxisRaw("Vertical") * movementForce;
		}
	

		rigidbody.AddForce(movementDirection);
	}

	void handleAimer()
	{
		Vector3 aimerPosition = Input.mousePosition;
		aimerPosition = Camera.main.ScreenToWorldPoint(aimerPosition);
		aimerPosition -= transform.position;
		//v3T = v3T * 10000.0f + transform.position;
		aimerPosition.z = 0;

		aimerObject.transform.up = aimerPosition;
	}

	public void queueManagementExtras()
	{
		//what the fuck are you doing sunil.
		Color newColor = animalQueuePortraits.transform.GetChild(animalQueue.Count).GetComponent<Image>().color;
		newColor.a = 0;
		animalQueuePortraits.transform.GetChild(animalQueue.Count).GetComponent<Image>().color = newColor;
		
		// Update portraits
		for (int i = 0; i < animalQueue.Count; i++)
		{
			if (animalQueue[i].name == "Squirrel")
			{
				animalQueuePortraits.transform.GetChild(i).GetComponent<Image>().sprite = squirrelSprite;
			}
			else if (animalQueue[i].name == "Bird")
			{
				animalQueuePortraits.transform.GetChild(i).GetComponent<Image>().sprite = birdSprite;
			}
		}
	}

	void handleShooting()
	{
		// Direction to shoot animal: aimerObject.transform.up
		if (Input.GetMouseButtonDown(0) && animalQueue.Count != 0)
		{
			animalQueue[0].shootAnimal(aimerObject.transform.up);
			animalQueue[0].caught = false;
			animalQueue[0].queueIndex = animalQueue.Count;
			animalQueue.RemoveAt(0);

			Color newColor = animalQueuePortraits.transform.GetChild(animalQueue.Count).GetComponent<Image>().color;
			newColor.a = 0;
			animalQueuePortraits.transform.GetChild(animalQueue.Count).GetComponent<Image>().color = newColor;

			// Update portraits
			for (int i = 0; i < animalQueue.Count; i++)
			{
				if (animalQueue[i].name == "Squirrel")
				{
					animalQueuePortraits.transform.GetChild(i).GetComponent<Image>().sprite = squirrelSprite;
				}
				else if (animalQueue[i].name == "Bird")
				{
					animalQueuePortraits.transform.GetChild(i).GetComponent<Image>().sprite = birdSprite;
				}
			}

			if (animalQueue.Count > 0)
			{
				// TODO: Update Queue Position of animals in a smoother fashion
				updateQueuePositions();
			}
		}
	}

	void handleAnimalCall()
	{
		if (Input.GetMouseButtonDown(1) && !isCalling)
		{
			// Make animal call circle visible
			pingObject.GetComponent<MeshRenderer>().enabled = true;
			pingObject.GetComponent<CircleCollider2D>().enabled = true;

			Vector3 scaleTarget = new Vector3(pingRange, pingRange, pingRange);
			iTween.ScaleTo(pingObject, 
			    iTween.Hash("scale", scaleTarget, "time", 0.5f, "OnCompleteTarget", gameObject, "OnComplete", "afterPing"));
			isCalling = true;
		}
		else
		{
			isCalling = false;
		}
	}

	public Animal getAnimal(int animalIndex)
	{
		return animalQueue[animalIndex];
	}

	public int getCurrentAnimalQueueSize()
	{
		return animalQueue.Count;
	}

	public void removeFromQueue(Animal animal)
	{
		animalQueue.Remove (animal);
	}

	public void addAnimal(Animal inputAnimal)
	{
		if (animalQueue.Count < 5)
		{
			animalQueue.Add(inputAnimal);
			//animalQueue[animalQueue.Count-1].queueIndex = animalQueue.Count

			// update animal portraits in GUI
			Color newColor = animalQueuePortraits.transform.GetChild(animalQueue.Count-1).GetComponent<Image>().color;
			newColor.a = 1;
			animalQueuePortraits.transform.GetChild(animalQueue.Count-1).GetComponent<Image>().color = newColor;
			if (inputAnimal.name == "Squirrel")
			{
				animalQueuePortraits.transform.GetChild(animalQueue.Count-1).GetComponent<Image>().sprite = squirrelSprite;
			}
			else if (inputAnimal.name == "Bird")
			{
				animalQueuePortraits.transform.GetChild(animalQueue.Count-1).GetComponent<Image>().sprite = birdSprite;
			}
		}
	}

	void updateQueuePositions()
	{
		// Update order in line
		for (int i = 0; i < animalQueue.Count; i++)
		{
			animalQueue[i].queueIndex--;

			if (animalQueue[i].name == "Squirrel")
			{
				animalQueuePortraits.transform.GetChild(i).GetComponent<Image>().sprite = squirrelSprite;
			}
			else if (animalQueue[i].name == "Bird")
			{
				animalQueuePortraits.transform.GetChild(i).GetComponent<Image>().sprite = birdSprite;
			}
		}
	}

	public void updateQueuePositions(int startingPos)
	{
		for (int i = startingPos; i < animalQueue.Count; i++) 
		{
			animalQueue[i].queueIndex--;

			if (animalQueue[i].name == "Squirel")
			{
				animalQueuePortraits.transform.GetChild(i).GetComponent<Image>().sprite = squirrelSprite;
			}
			else if (animalQueue[i].name == "Bird")
			{
				animalQueuePortraits.transform.GetChild(i).GetComponent<Image>().sprite = birdSprite;
			}
		}
	}

	void afterPing()
	{
		pingObject.transform.localScale = originalPingScale;

		// Make animal call circle visible
		pingObject.GetComponent<MeshRenderer>().enabled = false;
		pingObject.GetComponent<CircleCollider2D>().enabled = false;
		
		isCalling = false;     
	}
}
