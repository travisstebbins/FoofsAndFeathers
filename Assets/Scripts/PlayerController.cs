﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	// public variables
	public float maxSpeed = 3f;
	public bool foofed = true;
	public float foofedParticleEmissionRate = 20f;

	// components
	private Rigidbody2D rb;
	private CircleCollider2D cc;
	private GameManager gm;
	private ParticleSystem particles;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		cc = GetComponent<CircleCollider2D> ();
		gm = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager> ();
		particles = GetComponent<ParticleSystem> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (CompareTag("Player1")) {
			rb.velocity = new Vector2 (Input.GetAxis ("Horizontal1") * maxSpeed, Input.GetAxis ("Vertical1") * maxSpeed);
		} else if (CompareTag("Player2")) {
			rb.velocity = new Vector2 (Input.GetAxis ("Horizontal2") * maxSpeed, Input.GetAxis ("Vertical2") * maxSpeed);
		}
		if (foofed) {
			cc.enabled = true;
			particles.startLifetime = 5;
			var emission = particles.emission;
			var rate = emission.rate;
			rate.constantMax = foofedParticleEmissionRate;
			emission.rate = rate;
		}
	}

	void FixedUpdate () {

	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.CompareTag ("ColorableObject")) {
			ColorableObject co = (ColorableObject)other.gameObject.GetComponent<ColorableObject> ();
			if (CompareTag ("Player1")) {
				co.setColor ();
			} else if (CompareTag ("Player2")) {
				co.setBW ();
			}
			gm.UpdateAndSetScore ();
		}
	}
}
