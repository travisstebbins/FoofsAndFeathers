using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour {

	// public variables
	public static GameManager instance = null;
	public float timeLimit = 180f;
	public float powerUpDuration = 5f;
	public float powerUpMaxSpeed = 50f;

	// private variables
	private GameObject pauseMenuDisplay;
	private GameObject player1;
	private GameObject player2;
	private int player1Score = 0;
	private int player2Score = 0;
	GameObject[] colorableObjects;
	UIManager uim;
	bool startTimer = false;
	private float timeRemaining;
	private bool isPaused = false;

	void Awake () {
		if (instance == null)
			instance = this;
		else if (instance != this) {
			Destroy (gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		uim = GameObject.FindGameObjectWithTag ("UIManager").GetComponent<UIManager> ();
		colorableObjects = GameObject.FindGameObjectsWithTag ("ColorableObject");
		UpdateAndSetScore ();

		player1 = GameObject.FindGameObjectWithTag ("Player1");
		player2 = GameObject.FindGameObjectWithTag ("Player2");

		if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName ("main")) {
			timeRemaining = timeLimit;
			startTimer = true;
			uim.setTime (Mathf.CeilToInt (timeRemaining));
			pauseMenuDisplay = GameObject.FindGameObjectWithTag ("PauseMenuManager");
		}
	}

	void OnLevelWasLoaded () {
		if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName ("main")) {
			Debug.Log("main loaded");
			Debug.Log (EventSystem.current.currentSelectedGameObject);
			EventSystem.current.CancelInvoke ();
		}
		Time.timeScale = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if (startTimer && !isPaused) {
			timeRemaining -= Time.unscaledDeltaTime;
			uim.setTime (Mathf.CeilToInt (timeRemaining));
			if (timeRemaining <= 0) {
				Debug.Log ("Game Over");
				EndGame ();
			}
		}
	}

	public void UpdateAndSetScore () {
		int player1Total = 0;
		int player2Total = 0;
		foreach (GameObject co in colorableObjects) {
			if (co.GetComponent<ColorableObject> ().inColor) {
				player1Total++;
			} else {
				player2Total++;
			}
		}
		player1Score = player1Total;
		player2Score = player2Total;
		uim.setPlayer1Score (player1Score);
		uim.setPlayer2Score (player2Score);
		uim.setProgressSlider (player1Score, player2Score);
	}

	public void player1IncrementScore () {
		player1Score++;
		player2Score--;
		uim.setPlayer1Score (player1Score);
		uim.setPlayer2Score (player2Score);
		uim.setProgressSlider (player1Score, player2Score);
	}

	public void player2IncrementScore () {
		player2Score++;
		player1Score--;
		uim.setPlayer1Score (player1Score);
		uim.setPlayer2Score (player2Score);
		uim.setProgressSlider (player1Score, player2Score);
	}

	public void PowerUp (int player, int powerUpType) {
		StartCoroutine (PowerUpCoroutine (player, powerUpType));
	}

	IEnumerator PowerUpCoroutine (int player, int powerUpType) {
		switch (powerUpType) {
		case 0:
			if (player == 1) {
				Debug.Log ("Player 1 speed power up");
				float oldMaxSpeed = player1.GetComponent<PlayerController> ().maxSpeed;
				player1.GetComponent<PlayerController> ().maxSpeed = powerUpMaxSpeed;
				yield return new WaitForSeconds (powerUpDuration);
				player1.GetComponent<PlayerController> ().maxSpeed = oldMaxSpeed;
			} else if (player == 2) {
				Debug.Log ("Player 2 speed power up");
				float oldMaxSpeed = player2.GetComponent<PlayerController> ().maxSpeed;
				player2.GetComponent<PlayerController> ().maxSpeed = powerUpMaxSpeed;
				yield return new WaitForSeconds (powerUpDuration);
				player2.GetComponent<PlayerController> ().maxSpeed = oldMaxSpeed;
			}
			break;
		case 1:
			Debug.Log ("Slow down time power up");
			Time.timeScale = 0.5f;
			yield return new WaitForSecondsRealtime (powerUpDuration);
			Time.timeScale = 1;
			break;
		case 2:
			Debug.Log ("Speed up time power up");
			Time.timeScale = 2;
			yield return new WaitForSecondsRealtime (powerUpDuration);
			Time.timeScale = 1;
			break;
		case 3:
			if (player == 1) {
				Debug.Log ("Player 1 reverses player 2's color");
				player2.GetComponent<PlayerController> ().setReversed (true);
				yield return new WaitForSeconds (powerUpDuration);
				player2.GetComponent<PlayerController> ().setReversed (false);
			} else if (player == 2) {
				Debug.Log ("Player 2 reverses player 1's color");
				player1.GetComponent<PlayerController> ().setReversed (true);
				yield return new WaitForSeconds (powerUpDuration);
				player1.GetComponent<PlayerController> ().setReversed (false);
			}
			break;
		case 4:
			if (player == 1) {
				Debug.Log ("Player 1 blocks player 2 from painting");
				player2.GetComponent<PlayerController> ().setBlocked (true);
				yield return new WaitForSeconds (powerUpDuration);
				player2.GetComponent<PlayerController> ().setBlocked (false);
			} else if (player == 2) {
				Debug.Log ("Player 2 blocks player 1 from painting");
				player1.GetComponent<PlayerController> ().setBlocked (true);
				yield return new WaitForSeconds (powerUpDuration);
				player1.GetComponent<PlayerController> ().setBlocked (false);
			}
			break;
		}
	}

	public void TogglePause () {
		if (pauseMenuDisplay.GetComponent<PauseMenuManager>().getIsPaused()) {
			pauseMenuDisplay.GetComponent<PauseMenuManager> ().Resume ();
			isPaused = false;
		}
		else {
			pauseMenuDisplay.GetComponent<PauseMenuManager> ().Pause ();
			isPaused = true;
		}
	}

	private void EndGame () {
		Time.timeScale = 0;
		if (player1Score > player2Score) {
			uim.EndGame (1);
		}
		else if (player2Score > player1Score) {
			uim.EndGame (2);
		}
		else {
			uim.EndGame (0);
		}
	}

	public void Quit () {
		SceneManager.LoadScene ("menu");
		Destroy (this);
	}
}
