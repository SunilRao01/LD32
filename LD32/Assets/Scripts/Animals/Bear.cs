using UnityEngine;
using System.Collections;

public class Bear : Animal
{
	protected float currentHealth = 20f;
	
	protected float getAttackTime()
	{
		return 1.7f;
	}
	protected float getDamage()
	{
		return 8f;
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
		return damage/1.2;
	}
}

