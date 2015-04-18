using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Hamelin;

namespace Hamelin
{
	public class PathingController
	{
		private List<GameObject> nodes;

		public PathingController(List<GameObject> _nodes)
		{
			nodes = _nodes;
		}

		public void popNode()
		{
			nodes.RemoveAt (0);
		}

		public GameObject getNextNode()
		{
			if (nodes.Count > 0) {
				return nodes [0];
			}
			else { return null; }
		}
	}
}

