﻿using UnityEngine;
using System.Collections;
using UnityEditor.SceneManagement;

public class GameManager : MonoBehaviour {

	// public variables
	public static GameManager instance = null;
	public float timeLimit = 180f;
	public float powerUpDuration = 5f;
	public float powerUpMaxSpeed = 50f;

	// private variables
	private GameObject player1;
	private GameObject player2;
	private int player1Score = 0;
	private int player2Score = 0;
	GameObject[] colorableObjects;
	UIManager uim;
	bool startTimer = false;
	private float timeRemaining;
	private int numPowerUps = 5;

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

		if(EditorSceneManager.GetActiveScene() == EditorSceneManager.GetSceneByName ("main")) {
			timeRemaining = timeLimit;
			startTimer = true;
			uim.setTime (Mathf.CeilToInt (timeRemaining));
		}
	}

	void OnLevelWasLoaded () {
		if(EditorSceneManager.GetActiveScene() == EditorSceneManager.GetSceneByName ("main")) {
			timeRemaining = timeLimit;
			startTimer = true;
			uim.setTime (Mathf.CeilToInt (timeRemaining));
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (startTimer) {
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
	}

	public void player1IncrementScore () {
		player1Score++;
		player2Score--;
		uim.setPlayer1Score (player1Score);
		uim.setPlayer2Score (player2Score);
	}

	public void player2IncrementScore () {
		player2Score++;
		player1Score--;
		uim.setPlayer1Score (player1Score);
		uim.setPlayer2Score (player2Score);
	}

	public void PowerUp (int player) {
		StartCoroutine (PowerUpCoroutine (player));
	}

	IEnumerator PowerUpCoroutine (int player) {
		Random.InitState ((int)System.DateTime.Now.Ticks);
		int power = Random.Range (0, numPowerUps);
		switch (power) {
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
}
