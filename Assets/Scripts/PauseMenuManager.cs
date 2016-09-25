using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour {

	// public variables
	public GameObject pauseMenu;
	public Button resumeButton;

	// private variables
	private GameManager gm;

	// private variables
	private bool isPaused = false;

	public void Pause () {
		pauseMenu.SetActive (true);
		Time.timeScale = 0;
		isPaused = true;
		resumeButton.Select ();
	}

	public void Resume () {
		pauseMenu.SetActive (false);
		Time.timeScale = 1;
		isPaused = false;
	}

	public void Quit () {
		//SceneManager.LoadScene ("menu");
		gm.Quit();
	}

	// Use this for initialization
	void Start () {
		gm = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool getIsPaused () {
		return isPaused;
	}
}
