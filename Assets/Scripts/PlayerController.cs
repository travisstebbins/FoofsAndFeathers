using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	// components
	private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (CompareTag("Player1")) {
			rb.velocity = new Vector2 (Input.GetAxis ("Horizontal1"), Input.GetAxis ("Vertical1"));
		}
		else if (CompareTag("Player2")) {
			rb.velocity = new Vector2 (Input.GetAxis ("Horizontal2"), Input.GetAxis ("Vertical2"));
		}
	}

	void FixedUpdate () {

	}
}
