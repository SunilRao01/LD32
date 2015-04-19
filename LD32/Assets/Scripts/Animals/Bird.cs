using UnityEngine;
using System.Collections;

public class Bird : Animal {

	protected float currentHealth = 15f;
	public AudioClip deathSound;
	public AudioClip attackSound;
	protected override AudioClip getAttackSound()
	{
		return deathSound;
	}
	protected override AudioClip getDeathSound()
	{
		return attackSound;
	}
	protected override float getAttackTime()
	{
		return 1f;
	}
	protected override float getDamage()
	{
		return 5f;
	}
	protected override float getHealth()
	{
		return currentHealth;
	}
	protected override void setHealth (float newHealth)
	{
		currentHealth = newHealth;
	}
	protected override float defenseAdjust (float damage)
	{
		return damage;
	}
	protected override  int damageType()
	{
		return 1;
	}
}
