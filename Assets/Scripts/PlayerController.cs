using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	// public variables
	public float maxSpeed = 3f;

	// components
	private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (CompareTag("Player1")) {
			rb.velocity = new Vector2 (Input.GetAxis ("Horizontal1") * maxSpeed, Input.GetAxis ("Vertical1") * maxSpeed);
		} else if (CompareTag("Player2")) {
			rb.velocity = new Vector2 (Input.GetAxis ("Horizontal2") * maxSpeed, Input.GetAxis ("Vertical2") * maxSpeed);
		}
	}

	void FixedUpdate () {

	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.CompareTag ("ColorableObject")) {
			Debug.Log ("ColorableObject triggered");
			ColorableObject co = (ColorableObject)other.gameObject.GetComponent<ColorableObject> ();
			if (CompareTag ("Player1")) {
				Debug.Log ("co set color");
				co.setColor ();
			} else if (CompareTag ("Player2")) {
				Debug.Log ("co set BW");
				co.setBW ();
			}
		}
	}
}
