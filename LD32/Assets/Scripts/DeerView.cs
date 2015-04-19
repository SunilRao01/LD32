using UnityEngine;
using System.Collections;

public class DeerView : MonoBehaviour {
	private float currentHealth = 50;

	public void takeDamage(float damage)
	{
		currentHealth -= damage;
		if (currentHealth <= 0) {
			Application.LoadLevel ("Sandbox");
		}
	}
}
