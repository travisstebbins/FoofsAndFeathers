using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class MainMenuManager : MonoBehaviour {

	// public variables
	public GameObject mainMenuDisplay;
	public GameObject instructionsDisplay;
	public Button playButton;
	public Button backButton;

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
}
