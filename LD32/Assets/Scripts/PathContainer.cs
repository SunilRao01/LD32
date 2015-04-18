using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Hamelin;

namespace Hamelin
{
	public class PathContainer {
	
		public class PathNodes
		{
			private List<GameObject> nodes;

			public PathNodes(List<Integer> nodeNums, List<GameObject> regions)
			{
				nodes = new ArrayList();
				foreach (int node in nodeNums)
				{
					if (node >= 0 && node < regions.Count);
					{
						nodes.Add (regions[node]);
					}
				}
			}

			public List<GameObject> GetNodes()
			{
				return nodes;
			}
		}


		private List<PathNodes> paths;

		public PathContainer(GameObject RegionContainer) {

			List<GameObject> regions;

			int numRegions = 0;
			foreach (Transform region in RegionContainer.transform) { //get the number of regions
				numRegions++;
			}

			//output the num regions for reference
			Debug.Log ("Current Number Of Regions: " + numRegions);

			regions = new ArrayList (numRegions); //create a region list
			foreach (Transform region in RegionContainer.transform) {
				regions [region.GetComponent<RegionView> ().UNIQUE_ID] = region.gameObject;
			}

			//load in the paths into a data structure
			StreamReader sr = new StreamReader (Application.dataPath + "/StreamingAssets/paths.txt");
			while (!sr.EndOfStream) {
				List<String> nodeNumbersStrings = sr.ReadLine ().Split (',');

				List<Integer> nodeNumbersInts = new ArrayList ();
				foreach (string numString in nodeNumbersStrings) {
					nodeNumbersInts.Add ((int)numString);
				}
				PathNodes myPath = new PathNodes (nodeNumbersInts, regions);
				paths.Add (myPath);
			}
		}
			
			
		public List<GameObject> getPaths()
		{
			return paths[Random.Range (0, paths.Count)].GetNodes();
		}
	}
}
