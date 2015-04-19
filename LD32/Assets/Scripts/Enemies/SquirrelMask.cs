using UnityEngine;
using System.Collections;
using Hamelin;

namespace Hamelin
{
	public class BirdMask : PoacherView
	{
		private float currentHealth = 8;

		protected float getAttackTime()
		{
			return .8f;
		}
		protected float getDamage()
		{
			return 2f;
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
			return damage/1;
		}
		protected bool getExtraDamage(int special)
		{
			if (special == 0) {
				return true;
			}
			return false;
		}
	}
}

