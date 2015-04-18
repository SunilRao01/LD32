using UnityEngine;
using System.Collections;
using Hamelin;

namespace Hamelin
{
	public class GlobalView : MonoBehaviour {
		
		private PathContainer pathContainer;

		public RegionView regionContainer;

		private float time = 0;

		void Start()
		{
			pathContainer = new PathContainer (regionContainer);
		}

		void Update()
		{
			if (Time.timeSinceLevelLoad - time > 5) {
				time = Time.timeSinceLevelLoad;

			}
		}
	}
}
