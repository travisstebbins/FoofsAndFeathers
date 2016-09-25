using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class MainMenuManager : MonoBehaviour {

	// public variables
	public GameObject mainMenuDisplay;
	public GameObject instructionsDisplay;
	public GameObject powerUpsDisplay;
	public Button playButton;
	public Button backButton;
	public Button powerupsBackButton;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void Play () {
		SceneManager.LoadScene ("main");
	}

	public void Instructions () {
		mainMenuDisplay.SetActive (false);
		instructionsDisplay.SetActive (true);
		backButton.Select ();
	}

	public void Quit () {
		Application.Quit ();
	}

	public void instructionsBack () {
		instructionsDisplay.SetActive (false);
		mainMenuDisplay.SetActive (true);
		playButton.Select ();
	}

	public void powerUpsButton () {
		mainMenuDisplay.SetActive (false);
		powerUpsDisplay.SetActive (true);
		powerupsBackButton.Select ();
	}

	public void powerUpsBackButton () {
		powerUpsDisplay.SetActive (false);
		mainMenuDisplay.SetActive (true);
		playButton.Select ();
	}
}
