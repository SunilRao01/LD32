using UnityEngine;
using System.Collections;
using Hamelin;

public class DeerView : MonoBehaviour {
	private float currentHealth = 50;

	public void takeDamage(float damage)
	{
		currentHealth -= damage;
		if (currentHealth <= 0) {
			GameObject player = Camera.main.GetComponentInChildren<GlobalView>().player;
			GameObject.Destroy (player.GetComponent<Player>().canvas);
			GameObject.Destroy(player);
			Application.LoadLevel ("Loader");
		}
	}
}
