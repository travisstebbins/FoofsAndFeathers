using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	// public variables
	public static GameManager instance = null;

	// private variables
	private GameObject player1;
	private GameObject player2;
	private int player1Score = 0;
	private int player2Score = 0;
	GameObject[] colorableObjects;
	UIManager uim;

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
	}

	void OnLevelWasLoaded () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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

	public void player2IncrementScore (){
		player2Score++;
		player1Score--;
		uim.setPlayer1Score (player1Score);
		uim.setPlayer2Score (player2Score);
	}
}
