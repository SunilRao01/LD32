using UnityEngine;
using System.Collections;
using Hamelin;

public class DeerView : MonoBehaviour {
	private float currentHealth = 50;

	public Canvas deathView;

	void Start()
	{
		deathView.enabled = false;
	}

	public void takeDamage(float damage)
	{
		currentHealth -= damage;
		if (currentHealth <= 0) {	
			GameObject player = Camera.main.GetComponentInChildren<GlobalView>().player;
			GameObject.Destroy (player.GetComponent<Player>().canvas);
			GameObject.Destroy(player);
			GameObject.Destroy (GameObject.Find ("RegionContainer 1"));
			GameObject.Destroy (GameObject.Find ("BG Music"));
			GameObject.Destroy (gameObject);
			Application.LoadLevel ("GameOver");
		}
	}
}
