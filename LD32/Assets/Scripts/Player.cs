using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	public float movementForce;
	public float maxMovementSpeed;

	void Start () 
	{
	
	}
	
	void Update () 
	{
	
	}

	void handleMovment()
	{
		Vector2 movementDirection = new Vector2();
		movementDirection.x = Input.GetAxisRaw("Horizontal") * movementForce;
		movementDirection.y = Input.GetAxisRaw("Vertical") * movementForce;

	}
}
