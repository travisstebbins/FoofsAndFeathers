﻿using UnityEngine;
using System.Collections;

public class Foofer : MonoBehaviour {

	// public variables
	public float maxSpeed = 10f;
	public float randomVariance = 2f;
	public float forceMultiplier = 0.5f;

	// components
	Rigidbody2D rb;
	private bool arrived = true;

	// private variables
	private Transform leftBound;
	private Transform lowerBound;
	private Vector3 randomPoint;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		leftBound = GameObject.FindGameObjectWithTag ("LeftBound").GetComponent<Transform> ();
		lowerBound = GameObject.FindGameObjectWithTag ("LowerBound").GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (arrived) {
			StartCoroutine (NewRandomPoint ());
		}
		else {
			Vector3 moveVector = randomPoint - transform.position;
			rb.AddForce (new Vector3(
				((rb.velocity.x > maxSpeed) && moveVector.x > 0) ? 
				((rb.velocity.x < -maxSpeed && moveVector.x < 0)) ? 0 : moveVector.x : moveVector.x,
				((rb.velocity.y > maxSpeed) && moveVector.y > 0) ? 
				((rb.velocity.y < -maxSpeed && moveVector.y < 0)) ? 0 : moveVector.y : moveVector.y, 0) * forceMultiplier);
		}
	}

	IEnumerator NewRandomPoint () {
		randomPoint = new Vector3 (Random.Range (leftBound.position.x, -leftBound.position.x), Random.Range (lowerBound.position.y, -lowerBound.position.y), 0);
		arrived = false;
		yield return new WaitForSeconds(1.0f / randomVariance);
		arrived = true;
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.CompareTag("Boundary")) {
			rb.velocity = new Vector2(-rb.velocity.x, -rb.velocity.y);
		}
	}
}
