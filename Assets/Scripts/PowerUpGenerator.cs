using UnityEngine;
using System.Collections;

public class PowerUpGenerator : MonoBehaviour {

	// public variables
	public GameObject powerUpPrefab;
	public int minWaitTime = 5;
	public int maxWaitTime = 15;

	// private variables
	private int waitTime;
	private float currentTime;
	private float lastSpawnTime;
	private Transform leftBound;
	private Transform lowerBound;

	// Use this for initialization
	void Start () {
		waitTime = Random.Range (minWaitTime, maxWaitTime + 1);
		currentTime = Time.time;
		lastSpawnTime = Time.time;
		leftBound = GameObject.FindGameObjectWithTag ("LeftBound").GetComponent<Transform> ();
		lowerBound = GameObject.FindGameObjectWithTag ("LowerBound").GetComponent<Transform> ();
	}

	// Update is called once per frame
	void Update () {
		currentTime = Time.time;
		if (currentTime - lastSpawnTime >= waitTime) {
			GameObject.Instantiate (powerUpPrefab, new Vector3 (Random.Range (leftBound.position.x, -leftBound.position.x), Random.Range (lowerBound.position.y, -lowerBound.position.y), 0), Quaternion.identity);
			lastSpawnTime = Time.time;
			waitTime = Random.Range (minWaitTime, maxWaitTime + 1);
		}
	}
}
