using UnityEngine;
using System.Collections;

public class ButtonHandler : MonoBehaviour {

	public GameObject screen;
	public GameObject text;
	public GameObject button;

	private bool viewingCredits = false;

	public void Awake()
	{
		screen.SetActive (false);
		text.SetActive (false);
		button.SetActive (false);
	}

	public void OnPressStart()
	{
		if (!viewingCredits) {
			Application.LoadLevel ("Loader");
		}
	}

	public void OnPressBackToMenu()
	{
		if (viewingCredits) {
			screen.SetActive (false);
			text.SetActive (false);
			button.SetActive (false);
			viewingCredits = false;
		}
	}
	public void OnPressHowToPlay()
	{
	}
	public void OnPressCredits()
	{
		if (!viewingCredits) {
			screen.SetActive (true);
			text.SetActive (true);
			button.SetActive (true);
			viewingCredits = true;
		}
	}
	public void OnPressQuit()
	{
		if (!viewingCredits) {
			Application.Quit ();
		}
	}
}
