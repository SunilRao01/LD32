using UnityEngine;
using System.Collections;

public class DayNightController : MonoBehaviour {

	float timer = 0;

	bool day = false;
	bool transitioning = false;

	Material material;

	void Start() {
		material = GetComponent<Renderer>().material;
	}

	// Update is called once per frame
	void Update () {
		if (Time.timeSinceLevelLoad - timer > 20)
		{
			day = !day;
			transitioning = true;
			timer = Time.timeSinceLevelLoad;
		} 
		if (transitioning && !day) {
			if (material.color.a < .5)
			{
				material.color = new Color(material.color.r, material.color.g, material.color.b, material.color.a + .003f);
			} else
			{
				transitioning = false;

			}
		}
		else if (transitioning && day) {
			if (material.color.a >0)
			{
				float a = material.color.a -.003 < 0 ? 0.0f : material.color.a - .003f;
				material.color = new Color(material.color.r, material.color.g, material.color.b, a);
			} else
			{
				transitioning = false;
			}
		}
	}
}
