using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Hamelin
{
	public class TargetContainer
	{
		private List<Target> targets;

		public TargetContainer()
		{
			targets = new List<Target> ();
		}

		public void AddTarget(Target t)
		{
			targets.Add (t);
		}

		public void RemoveTarget(Target t)
		{
			targets.Remove (t);
		}

		public Target GetTarget()
		{
			if (targets.Count > 0) {
				Debug.Log ("Returning Target: " + targets[0] + ", Target count: " + targets.Count);
				return targets[0];
			}
			return null;
		}
	}
}

