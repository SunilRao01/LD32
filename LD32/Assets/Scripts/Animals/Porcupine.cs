using UnityEngine;
using System.Collections;

public class Porcupine : Animal
{
	protected float currentHealth = 25f;
		
	protected float getAttackTime()
	{
		return 1.3f;
	}
	protected float getDamage()
	{
		return 1f;
	}
	protected float getHealth()
	{
		return currentHealth;
	}
	protected void setHealth (float newHealth)
	{
		currentHealth = newHealth;
	}
	protected float defenseAdjust (float damage)
	{
		return damage/1.2f;
	}
	protected int damageType()
	{
		return -1;
	}
}

