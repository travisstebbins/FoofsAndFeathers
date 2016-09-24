using UnityEngine;
using System.Collections;
using UnityEditor.SceneManagement;

public class GameManager : MonoBehaviour {

	// public variables
	public static GameManager instance = null;
	public float timeLimit = 180f;

	// private variables
	private GameObject player1;
	private GameObject player2;
	private int player1Score = 0;
	private int player2Score = 0;
	GameObject[] colorableObjects;
	UIManager uim;
	bool startTimer = false;
	private float timeRemaining;

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

		if(EditorSceneManager.GetActiveScene() == EditorSceneManager.GetSceneByName ("main")) {
			timeRemaining = timeLimit;
			startTimer = true;
			uim.setTime ((int)timeRemaining);
		}
	}

	void OnLevelWasLoaded () {
		if(EditorSceneManager.GetActiveScene() == EditorSceneManager.GetSceneByName ("main")) {
			timeRemaining = timeLimit;
			startTimer = true;
			uim.setTime ((int)timeRemaining);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (startTimer) {
			timeRemaining -= Time.deltaTime;
			uim.setTime ((int)timeRemaining);
			if (timeRemaining <= 0) {
				Debug.Log ("Game Over");
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

	public void player2IncrementScore (){
		player2Score++;
		player1Score--;
		uim.setPlayer1Score (player1Score);
		uim.setPlayer2Score (player2Score);
	}
}
