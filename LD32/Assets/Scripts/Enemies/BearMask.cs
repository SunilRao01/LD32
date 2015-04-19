using UnityEngine;
using System.Collections;
using Hamelin;

namespace Hamelin
{
	public class BearMask : PoacherView
	{
		private float currentHealth = 18;

		protected float getAttackTime()
		{
			return 1.3f;
		}
		protected float getDamage()
		{
			return 5f;
		}
		protected float getHealth()
		{
			return currentHealth;
		}
		protected void setHealth(float newHealth)
		{
			currentHealth = newHealth;
		}
		protected int getPoints()
		{
			return 10;
		}
		protected float defenseAdjust (float damage)
		{
			return damage/1.1f;
		}
		protected bool getExtraDamage(int special)
		{
			if (special == 2) {
				return true;
			}
			return false;
		}
	}
}

