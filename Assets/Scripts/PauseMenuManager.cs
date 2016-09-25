using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour {

	// public variables
	public GameObject pauseMenu;

	// private variables
	private bool isPaused;

	public void Pause () {
		pauseMenu.SetActive (true);
		Time.timeScale = 0;
		isPaused = true;
	}

	public void Resume () {
		pauseMenu.SetActive (false);
		Time.timeScale = 1;
		isPaused = false;
	}

	public void Quit () {
		SceneManager.LoadScene ("menu");
	}

	// Use this for initialization
	void Start () {
		isPaused = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool getIsPaused () {
		return isPaused;
	}
}
