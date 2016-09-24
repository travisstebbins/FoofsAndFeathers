using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	// public variables
	public Text player1ScoreText;
	public Text player2ScoreText;
	public Text player1FoofersText;
	public Text player2FoofersText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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
}
