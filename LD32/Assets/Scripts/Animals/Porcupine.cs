using UnityEngine;
using System.Collections;

public class Porcupine : Animal
{
	protected float currentHealth = 25f;
		
	protected override float getAttackTime()
	{
		return 1.3f;
	}
	protected override float getDamage()
	{
		return 1f;
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
		return -1;
	}
}

