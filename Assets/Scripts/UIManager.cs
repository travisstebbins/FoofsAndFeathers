using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	// public variables
	public Text player1ScoreText;
	public Text player2ScoreText;
	public Text player1FoofersText;
	public Text player2FoofersText;
	public Text timeText;
	public Text endGameText;
	public Slider progressSlider;
	public GameObject endGameScreen;
	public Button endGameQuitButton;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setProgressSlider (int p1Score, int p2Score) {
		float percent = ((float)p1Score) / (p1Score + p2Score);
		progressSlider.value = percent;
	}

	public void setPlayer1Score (int p1s) {
		player1ScoreText.text = "Player 1: " + p1s;
	}

	public void setPlayer2Score (int p2s) {
		player2ScoreText.text = "Player 2: " + p2s;
	}

	public void setPlayer1Foofers (int p1f) {
		player1FoofersText.text = "Foofers: " + p1f;
	}

	public void setPlayer2Foofers (int p2f) {
		player2FoofersText.text = "Foofers: " + p2f;
	}

	public void setTime (int time) {
		timeText.text = "" + time;
	}

	public void EndGame (int winner) {
		player1ScoreText.enabled = false;
		player2ScoreText.enabled = false;
		player1FoofersText.enabled = false;
		player2FoofersText.enabled = false;
		timeText.enabled = false;
		if (winner != 0) {
			endGameText.text = "Player " + winner + " W ins!";
		}
		else {
			endGameText.text = "It's a tie!";
		}
		endGameScreen.SetActive (true);
		endGameQuitButton.Select ();
		//endGameText.enabled = true;
	}
}
