using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	// public variables
	public float maxSpeed = 3f;
	public float foofedParticleEmissionRate = 20f;
	public float superFoofDuration = 5f;
	public float attackDuration = 0.3f;

	// components
	private Rigidbody2D rb;
	private CircleCollider2D cc;
	private GameManager gm;
	private ParticleSystem particles;
	private bool reversed = false;
	private bool locked = false;

	// private variables
	int foofers = 0;
	UIManager uim;
	bool isAttacking = false;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		cc = GetComponent<CircleCollider2D> ();
		gm = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager> ();
		uim = GameObject.FindGameObjectWithTag ("UIManager").GetComponent<UIManager> ();
		particles = GetComponent<ParticleSystem> ();
		if (CompareTag ("Player1")) {
			uim.setPlayer1Foofers (foofers);
		} else if (CompareTag("Player2")) {
			uim.setPlayer2Foofers (foofers);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (CompareTag("Player1")) {
			Vector2 vel = new Vector2 (Input.GetAxis ("Horizontal1") * maxSpeed, Input.GetAxis ("Vertical1") * maxSpeed);
			if (isAttacking) {
				rb.velocity = vel * 3;
			}
			else {
				rb.velocity = vel;
			}
			if (Input.GetButtonDown("Player1Fire") && !isAttacking) {
				Debug.Log ("Player 1 fired");
				StartCoroutine (AttackCooldown ());
			}
		} else if (CompareTag("Player2")) {
			rb.velocity = new Vector2 (Input.GetAxis ("Horizontal2") * maxSpeed, Input.GetAxis ("Vertical2") * maxSpeed);
			if (Input.GetButtonDown("Player2Fire") && !isAttacking) {
				Debug.Log ("Player 2 fired");
			}
		}
	}

	void FixedUpdate () {

	}

	void OnCollisionEnter2D (Collision2D coll) {
		if (CompareTag("Player1")) {
			if (isAttacking) {
				if (coll.gameObject.CompareTag ("Player2")) {
					coll.gameObject.GetComponent<PlayerController> ().decrementFoofers ();
				}
			}
		}
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.CompareTag ("ColorableObject")) {
			ColorableObject co = (ColorableObject)other.gameObject.GetComponent<ColorableObject> ();
			if (CompareTag ("Player1")) {
				if (!locked) {
					if (!reversed) {
						if (!co.inColor) {
							co.setColor ();
							gm.player1IncrementScore ();
						}
					} else {
						if (co.inColor) {
							co.setBW ();
							gm.player2IncrementScore ();
						}
					}
				}
			} else if (CompareTag ("Player2")) {
				if (!locked) {
					if (!reversed) {
						if (co.inColor) {
							co.setBW ();
							gm.player2IncrementScore ();
						}
					} else {
						if (!co.inColor) {
							co.setColor ();
							gm.player1IncrementScore ();
						}
					}
				}
			}
		}

		else if (other.gameObject.CompareTag ("Foofer")) {
			foofers++;
			GameObject.Destroy (other.gameObject);
			Debug.Log (foofers);
			if (foofers >= 3) {
				foofers = 0;
				StartCoroutine (SuperFoof ());
			}
			if (CompareTag ("Player1")) {
				uim.setPlayer1Foofers (foofers);
			} else if (CompareTag("Player2")) {
				uim.setPlayer2Foofers (foofers);
			}
		}

		else if (other.gameObject.CompareTag ("PowerUp")) {
			if (CompareTag("Player1")) {
				gm.PowerUp (1);
			}
			else if (CompareTag("Player2")) {
				gm.PowerUp (2);
			}
			GameObject.Destroy (other.gameObject);
		}
	}

	IEnumerator SuperFoof () {
		if (CompareTag ("Player1")) {
			Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer ("Player1"), LayerMask.NameToLayer ("Foofer"), true);
			Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer ("Player1"), LayerMask.NameToLayer ("PowerUp"), true);
		}
		else if (CompareTag("Player2")) {
			Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer ("Player2"), LayerMask.NameToLayer ("Foofer"), true);
			Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer ("Player2"), LayerMask.NameToLayer ("PowerUp"), true);
		}
		cc.enabled = true;
		particles.startLifetime = 5;
		var emission = particles.emission;
		var rate = emission.rate;
		rate.constantMax = foofedParticleEmissionRate;
		emission.rate = rate;
		yield return new WaitForSeconds (superFoofDuration);
		if (CompareTag ("Player1")) {
			Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer ("Player1"), LayerMask.NameToLayer ("Foofer"), false);
			Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer ("Player1"), LayerMask.NameToLayer ("PowerUp"), false);
		}
		else if (CompareTag("Player2")) {
			Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer ("Player2"), LayerMask.NameToLayer ("Foofer"), false);
			Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer ("Player2"), LayerMask.NameToLayer ("PowerUp"), false);
		}
		cc.enabled = false;
		particles.startLifetime = 2;
		emission = particles.emission;
		rate = emission.rate;
		rate.constantMax = 10;
		emission.rate = rate;
	}

	public void setReversed (bool r) {
		reversed = r;
	}

	public void setBlocked (bool l) {
		locked = l;
	}

	IEnumerator AttackCooldown () {
		isAttacking = true;
		yield return new WaitForSeconds (attackDuration);
		isAttacking = false;
	}

	public void decrementFoofers () {
		if (foofers > 0) {
			foofers--;
		}
		if (CompareTag("Player1")) {
			uim.setPlayer1Foofers (foofers);
		}
		else if (CompareTag("Player2")) {
			uim.setPlayer2Foofers (foofers);
		}
	}
}
