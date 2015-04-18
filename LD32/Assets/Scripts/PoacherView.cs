using UnityEngine;
using System.Collections;

namespace Hamelin
{
	public class PoacherView : MonoBehaviour {

		private PathingController controller = null;
		private bool isSetup = false;
		private GameObject nextNode = null;

		// Update is called once per frame
		void Update () {
			if (!isSetup) {
				if (controller != null) {
					transform.position = controller.getNextNode ();
					controller.popNode ();
					nextNode = controller.getNextNode();
					isSetup = true;
				}
			} else {

			}
		}
	}
}