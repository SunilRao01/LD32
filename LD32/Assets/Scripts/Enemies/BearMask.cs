using UnityEngine;
using System.Collections;
using Hamelin;

namespace Hamelin
{
	public class BearMask : PoacherView
	{
		private float currentHealth = 18;

		protected override float getAttackTime()
		{
			return 1.3f;
		}
		protected override float getDamage()
		{
			return 5f;
		}
		protected override float getHealth()
		{
			return currentHealth;
		}
		protected override void setHealth(float newHealth)
		{
			currentHealth = newHealth;
		}
		protected override int getPoints()
		{
			return 10;
		}
		protected override float defenseAdjust (float damage)
		{
			return damage/1.1f;
		}
		protected override bool getExtraDamage(int special)
		{
			if (special == 2) {
				return true;
			}
			return false;
		}
	}
}

