using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlendColors : MonoBehaviour 
{
	public List<Color> blendColors;
	public float colorChangeRate;

	private SpriteRenderer spriteRenderer;
	private Color currentColor;
	private Color newColor;
	private int colorChoice;
	private float colorCount;

	public bool randomColors;
	public bool customAlpha;
	public float customAlphaValue;

	public bool isText;
	public bool textMesh;
	public bool isMaterial;

	public float introDelay;
	private bool enableBlend;

	public bool sequential;
	private int sequenceCount;



	public BlendColors()
	{
		randomColors = true;

		blendColors = new List<Color>();
		blendColors.Add(Color.white);

		colorChangeRate = 0.1f;
	}

	void Start () 
	{
		if (!isText)
		{
			spriteRenderer = GetComponent<SpriteRenderer>();
			currentColor = spriteRenderer.color;
		}
		else
		{
			if (textMesh)
			{
				currentColor = GetComponent<TextMesh>().color;
			}
			else
			{
				currentColor = GetComponent<GUIText>().color;
			}
		}

		if (isMaterial)
		{
			currentColor = GetComponent<Renderer>().material.color;
		}

		enableBlend = false;
		StartCoroutine(delay());
	}

	IEnumerator delay()
	{
		yield return new WaitForSeconds(introDelay);
		enableBlend = true;

	}
	
	void Update () 
	{
		if (enableBlend)
		{
			changeColors();
		}
	}

	void changeColors()
	{
		if (colorCount == 0)
		{
			colorChoice = Random.Range(0, (blendColors.Count));
			newColor = blendColors[colorChoice];

			if (sequential)
			{
				newColor = blendColors[sequenceCount];

				if (sequenceCount == blendColors.Count-1)
				{
					sequenceCount = 0;
				}
				else
				{
					sequenceCount++;
				}
			}

			if (randomColors)
			{
				newColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 1);
			}

			if (customAlpha)
			{
				newColor.a = customAlphaValue;
			}

			colorCount += colorChangeRate;
		}
		else if (colorCount > 0 && colorCount < 1)
		{
			colorCount += colorChangeRate;
		}
		else if (colorCount >= 1)
		{
			colorCount = 0;

			if (!isText)
			{
				currentColor = spriteRenderer.color;
			}
			else
			{
				if (textMesh)
				{
					currentColor = GetComponent<TextMesh>().color;
				}
				else
				{
					currentColor = GetComponent<GUIText>().color;
				}
			}

		}


		if (isText)
		{
			if (textMesh)
			{
				GetComponent<TextMesh>().color = Color.Lerp(currentColor, newColor, colorCount);
			}
			else
			{
				GetComponent<GUIText>().color = Color.Lerp(currentColor, newColor, colorCount);
			}
		}
		else
		{
			spriteRenderer.color = Color.Lerp(currentColor, newColor, colorCount);
		}
	}
}
