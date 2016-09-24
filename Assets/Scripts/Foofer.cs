using UnityEngine;
using System.Collections;

public class Foofer : MonoBehaviour {

	// public variables
	public float maxSpeed = 10f;
	public float randomVariance = 2f;

	// components
	Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		rb.velocity = new Vector2 (Random.Range (-maxSpeed, maxSpeed), Random.Range (-maxSpeed, maxSpeed));
		//rb.AddForce(new Vector2(Random.Range(-randomVariance, randomVariance), Random.Range(-randomVariance, randomVariance)));
	}
	
	// Update is called once per frame
	void Update () {
		//rb.AddForce(new Vector2(Random.Range(-randomVariance, randomVariance), Random.Range(-randomVariance, randomVariance)));
		//rb.velocity = new Vector2 ((Mathf.Abs(rb.velocity.x) > maxSpeed) ?
		//	rb.velocity.x + Random.Range (-randomVariance, randomVariance) : rb.velocity.x,
		//	(Mathf.Abs(rb.velocity.y) > maxSpeed ?
		//		rb.velocity.y + Random.Range (-randomVariance, randomVariance) : rb.velocity.y));
		rb.velocity = new Vector2(Random.Range(-maxSpeed, maxSpeed), Random.Range(-maxSpeed, maxSpeed));
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.CompareTag("Boundary")) {
			rb.velocity = new Vector2(-rb.velocity.x, -rb.velocity.y);
		}
	}
}
