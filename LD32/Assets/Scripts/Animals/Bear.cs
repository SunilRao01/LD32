using UnityEngine;
using System.Collections;

public class Bear : Animal
{
	protected float currentHealth = 20f;
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
		return 1.7f;
	}
	protected override float getDamage()
	{
		return 8f;
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
		return damage/1.2f;
	}
	protected override int damageType()
	{
		return 2;
	}
}

