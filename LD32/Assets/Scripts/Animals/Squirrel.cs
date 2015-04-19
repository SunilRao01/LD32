using UnityEngine;
using System.Collections;

public class Squirrel : Animal 
{
	protected float currentHealth = 15f;
	
	protected float getAttackTime()
	{
		return .7f;
	}
	protected float getDamage()
	{
		return 3f;
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
		return 0;
	}
}
