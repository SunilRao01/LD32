using UnityEngine;
using System.Collections;

namespace Hamelin
{
	public class Target
	{
		private GameObject instance;
		private TargetContainer targetContainer;

		public Target(GameObject go, TargetContainer tc)
		{
			instance = go;
			targetContainer = tc;
		}

		public GameObject getGameObject()
		{
			return instance;
		}
		public void cleanReferences()
		{
			targetContainer.RemoveTarget (this);
		}
		
	}
}

