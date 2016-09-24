using UnityEngine;
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
		//rb.velocity = new Vector2 (Random.Range (-maxSpeed, maxSpeed), Random.Range (-maxSpeed, maxSpeed));
		//rb.AddForce(new Vector2(Random.Range(-randomVariance, randomVariance), Random.Range(-randomVariance, randomVariance)));
	}
	
	// Update is called once per frame
	void Update () {
		//rb.AddForce(new Vector2(Random.Range(-randomVariance, randomVariance), Random.Range(-randomVariance, randomVariance)));
		//rb.velocity = new Vector2 ((Mathf.Abs(rb.velocity.x) > maxSpeed) ?
		//	rb.velocity.x + Random.Range (-randomVariance, randomVariance) : rb.velocity.x,
		//	(Mathf.Abs(rb.velocity.y) > maxSpeed ?
		//		rb.velocity.y + Random.Range (-randomVariance, randomVariance) : rb.velocity.y));
		//rb.velocity = new Vector2(Random.Range(-maxSpeed, maxSpeed), Random.Range(-maxSpeed, maxSpeed));
		if (arrived) {
			StartCoroutine (NewRandomPoint ());
		}
		else {
			float step = maxSpeed * Time.deltaTime;
			//transform.position = Vector3.MoveTowards (transform.position, randomPoint, step);
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
		//transform.position = Vector3.Lerp (transform.position, randomPosition, 1);
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
