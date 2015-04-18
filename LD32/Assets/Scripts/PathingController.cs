using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Hamelin;

namespace Hamelin
{
	public class PathingController
	{
		private List<GameObject> nodes;
		int position;

		public PathingController(List<GameObject> _nodes)
		{
			nodes = _nodes;
			position = 0;
		}

		public void popNode()
		{
			position++;
		}

		public GameObject getNextNode()
		{
			if (position < nodes.Count) {
				return nodes [position];
			}
			else { return null; }
		}
	}
}

