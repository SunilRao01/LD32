using UnityEngine;
using System.Collections;

public class Bird : Animal {


	protected float getAttackTime()
	{
		return 1f;
	}
	protected float getDamage()
	{
		return 5f;
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
		return damage;
	}
	protected int damageType()
	{
		return 1;
	}
}
