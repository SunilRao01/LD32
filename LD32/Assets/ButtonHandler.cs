using UnityEngine;
using System.Collections;

public class ButtonHandler : MonoBehaviour {

	public GameObject screen;
	public GameObject text;
	public GameObject button;
	public GameObject instructionsText;

	private bool viewingCredits = false;

	public void Awake()
	{
		screen.SetActive (false);
		text.SetActive (false);
		button.SetActive (false);
		instructionsText.SetActive(false);
	}

	public void OnPressStart()
	{
		if (!viewingCredits) {
			Application.LoadLevel ("Loader");
		}
	}

	public void OnPressBackToMenu()
	{
		screen.SetActive (false);
		text.SetActive (false);
		button.SetActive (false);
		instructionsText.SetActive(false);
	}
	public void OnPressHowToPlay()
	{
		screen.SetActive(true);
		instructionsText.SetActive(true);
		button.SetActive(true);
	}
	public void OnPressCredits()
	{
			screen.SetActive (true);
			text.SetActive (true);
			button.SetActive (true);
			viewingCredits = true;

	}
	public void OnPressQuit()
	{
			Application.Quit ();

	}
}
