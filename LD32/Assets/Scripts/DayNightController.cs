using UnityEngine;
using System.Collections;

public class DayNightController : MonoBehaviour {

	float timer = 0;

	bool day = false;
	bool transitioning = false;
	bool quieting = true;

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
				if (quieting && GetComponentInChildren<AudioSource>().volume > 0)
				{
					if (GetComponentInChildren<AudioSource>().volume - .012f > 0)
					{
						foreach(AudioSource source in GetComponentsInChildren<AudioSource>())
						{
							source.volume -= .012f;
						}
					}
					else
					{
						GetComponentInChildren<AmbientForestToggle>().day = !GetComponentInChildren<AmbientForestToggle>().day;
					}
				}
				if (!quieting && GetComponentInChildren<AudioSource>().volume < 100)
				{
					if (GetComponentInChildren<AudioSource>().volume + .012f < 100)
					{
						foreach(AudioSource source in GetComponentsInChildren<AudioSource>())
						{
							source.volume += .012f;
						}
					}
				}
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
				if (quieting && GetComponentInChildren<AudioSource>().volume > 0)
				{
					if (GetComponentInChildren<AudioSource>().volume - .012f > 0)
					{
						foreach(AudioSource source in GetComponentsInChildren<AudioSource>())
						{
							source.volume -= .012f;
						}
					}
					else
					{
						GetComponentInChildren<AmbientForestToggle>().day = !GetComponentInChildren<AmbientForestToggle>().day;
					}
				}
				if (!quieting && GetComponentInChildren<AudioSource>().volume < 100)
				{
					if (GetComponentInChildren<AudioSource>().volume + .012f < 100)
					{
						foreach(AudioSource source in GetComponentsInChildren<AudioSource>())
						{
							source.volume += .012f;
						}
					}
				}			
			} else
			{
				transitioning = false;
			}
		}
	}
}
