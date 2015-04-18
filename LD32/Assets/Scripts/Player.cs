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
		originalPingScale = pingObject.transform.localScale;
	}

	void Start () 
	{
		pingObject.GetComponent<MeshRenderer>().enabled = false;
	}
	
	void Update () 
	{
		handleMovment();
		handleAimer();
		handleAnimalCall();
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

	void handleAnimalCall()
	{
		if (Input.GetMouseButtonDown(1) && !isCalling)
		{
			Debug.Log("Animal call!");

			// Make animal call circle visible
			pingObject.GetComponent<MeshRenderer>().enabled = true;

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
		
		isCalling = false;     
	}
}
