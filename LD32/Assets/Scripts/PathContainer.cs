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

			public PathNodes(List<int> nodeNums, GameObject[] regions)
			{
				nodes = new List<GameObject>();
				foreach (int node in nodeNums)
				{
					if (node >= 0 && node < regions.Length);
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

			GameObject[] regions;
			paths = new List<PathNodes> ();

			int numRegions = 0;
			foreach (Transform region in RegionContainer.transform) { //get the number of regions
				numRegions++;
			}

			//output the num regions for reference
			Debug.Log ("Current Number Of Regions: " + numRegions);

			regions = new GameObject[numRegions]; //create a region list
			foreach (Transform region in RegionContainer.transform) {
				Debug.Log (region.GetComponent<RegionView> ().UNIQUE_ID);
				regions [region.GetComponent<RegionView> ().UNIQUE_ID] = region.gameObject;
			}

			//load in the paths into a data structure
			StreamReader sr = new StreamReader (Application.dataPath + "/StreamingAssets/paths.txt");
			while (!sr.EndOfStream) {
				string[] nodeNumbersStrings = sr.ReadLine ().Split (',');

				List<int> nodeNumbersInts = new List<int>();
				foreach (string numString in nodeNumbersStrings) {
					nodeNumbersInts.Add (int.Parse(numString));
				}
				PathNodes myPath = new PathNodes (nodeNumbersInts, regions);
				paths.Add (myPath);
			}
		}
			
			
		public List<GameObject> getPath()
		{
			Debug.Log ("paths: " + paths.Count);
			return paths[Random.Range (0, paths.Count)].GetNodes();
		}
	}
}
