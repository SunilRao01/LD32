using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

	void Awake()
	{
		rigidbody = GetComponent<Rigidbody2D>();
		aimerObject = transform.FindChild("AimerPivot").gameObject;
		pingObject = transform.FindChild("PingSphere").gameObject;
		pingObject.GetComponent<CircleCollider2D>().enabled = false;
		originalPingScale = pingObject.transform.localScale;

		Physics2D.IgnoreLayerCollision(8, 9);
		Physics2D.IgnoreLayerCollision(9, 9);
		Physics.IgnoreLayerCollision(8, 9);
		Physics.IgnoreLayerCollision(9, 9);
	}

	void Start () 
	{
		pingObject.GetComponent<MeshRenderer>().enabled = false;

		animalQueue = new List<Animal>();
	}
	
	void Update () 
	{
		handleMovment();
		handleAimer();
		handleAnimalCall();
		handleShooting();
	}

	void handleMovment()
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

	void handleShooting()
	{
		// Direction to shoot animal: aimerObject.transform.up

		if (Input.GetMouseButtonDown(0))
		{
			Debug.Log("Shoot animal!");
			animalQueue[0].shootAnimal(aimerObject.transform.up);
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
	}

	public Animal getAnimal(int animalIndex)
	{
		return animalQueue[animalIndex];
	}

	public int getCurrentAnimalQueueSize()
	{
		return animalQueue.Count;
	}

	public void addAnimal(Animal inputAnimal)
	{
		if (animalQueue.Count < 5)
		{
			animalQueue.Add(inputAnimal);
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
