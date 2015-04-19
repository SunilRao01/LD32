using UnityEngine;
using System.Collections;
using Hamelin;

namespace Hamelin
{
	public class SquirrelMask : PoacherView
	{
		private float currentHealth = 8;

		protected override float getAttackTime()
		{
			return .8f;
		}
		protected override float getDamage()
		{
			return 2f;
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
			return damage/1;
		}
		protected override bool getExtraDamage(int special)
		{
			if (special == 0) {
				return true;
			}
			return false;
		}
	}
}

