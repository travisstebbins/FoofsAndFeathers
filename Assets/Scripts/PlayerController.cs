using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	// public variables
	public float maxSpeed = 3f;
	public float foofedParticleEmissionRate = 20f;
	public float superFoofDuration = 5f;

	// components
	private Rigidbody2D rb;
	private CircleCollider2D cc;
	private GameManager gm;
	private ParticleSystem particles;
	private bool isSuperFoof = false;

	// private variables
	int foofers = 0;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		cc = GetComponent<CircleCollider2D> ();
		gm = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager> ();
		particles = GetComponent<ParticleSystem> ();
		Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer ("Foofer"), LayerMask.NameToLayer ("BoardIgnoreCollisions"), false);
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
			ColorableObject co = (ColorableObject)other.gameObject.GetComponent<ColorableObject> ();
			if (CompareTag ("Player1")) {
				co.setColor ();
			} else if (CompareTag ("Player2")) {
				co.setBW ();
			}
			gm.UpdateAndSetScore ();
		}

		if (other.gameObject.CompareTag ("Foofer")) {
			foofers++;
			GameObject.Destroy (other.gameObject);
			Debug.Log (foofers);
			if (foofers >= 3) {
				foofers = 0;
				StartCoroutine (superFoof ());
			}
		}
	}

	IEnumerator superFoof () {
		isSuperFoof = true;
		Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer ("Player"), LayerMask.NameToLayer ("Foofer"), true);
		cc.enabled = true;
		particles.startLifetime = 5;
		var emission = particles.emission;
		var rate = emission.rate;
		rate.constantMax = foofedParticleEmissionRate;
		emission.rate = rate;
		yield return new WaitForSeconds (superFoofDuration);
		Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer ("Player"), LayerMask.NameToLayer ("Foofer"), false);
		cc.enabled = false;
		particles.startLifetime = 2;
		emission = particles.emission;
		rate = emission.rate;
		rate.constantMax = 10;
		emission.rate = rate;
	}
}
