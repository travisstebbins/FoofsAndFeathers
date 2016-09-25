using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	// public variables
	public float maxSpeed = 3f;
	public float foofedParticleEmissionRate = 20f;
	public float superFoofRadius = 10f;
	public float superFoofDuration = 5f;
	public float attackDuration = 0.3f;
	public float attackCooldown = 2.0f;
	public float attackRadius = 5f;
	public float attackForceMultiplier = 1000f;
	public float player2ScaleFactor = 2f;
	public float player2ScaleSpeed = 0.01f;
	public int numFoofersRequired = 3;
	public GameObject powerUpFeedbackPrefab;
	public AudioClip[] sounds;
	public AudioClip[] player1FoofSounds;
	public AudioClip[] player2FoofSounds;

	// components
	private Rigidbody2D rb;
	private CircleCollider2D cc;
	private GameManager gm;
	private ParticleSystem particles;
	private Animator anim;
	private AudioSource audio;

	// private variables
	int foofers = 0;
	UIManager uim;
	private bool reversed = false;
	private bool locked = false;
	bool isAttacking = false;
	bool attackReady = true;
	static bool pausePushed = false;
	private bool facingRight;
	private bool isSuperFoof = false;
	private float player2MinScale;
	private bool player2ScaleComplete = true;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		cc = GetComponent<CircleCollider2D> ();
		gm = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager> ();
		uim = GameObject.FindGameObjectWithTag ("UIManager").GetComponent<UIManager> ();
		particles = GetComponent<ParticleSystem> ();
		anim = GetComponent<Animator> ();
		audio = GetComponent<AudioSource> ();
		if (CompareTag ("Player1")) {
			uim.setPlayer1Foofers (foofers);
			facingRight = true;
		} else if (CompareTag("Player2")) {
			uim.setPlayer2Foofers (foofers);
			facingRight = false;
			player2MinScale = transform.localScale.x;
			cc.radius = superFoofRadius;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (CompareTag("Player1")) {
			if (Input.GetButtonDown("Cancel1")) {
				Debug.Log ("Cancel button pushed");
				gm.TogglePause ();
			}
			Vector2 vel = new Vector2 (Input.GetAxis ("Horizontal1") * maxSpeed, Input.GetAxis ("Vertical1") * maxSpeed);
			if (isAttacking) {
				rb.velocity = vel * 3;
			}
			else {
				rb.velocity = vel;
			}
			if (Input.GetButtonDown("Player1Fire") && attackReady) {
				Debug.Log ("Player 1 fired");
				StartCoroutine (AttackCooldown ());
			}
		} else if (CompareTag("Player2")) {
			if (Input.GetButtonDown("Cancel2")) {
				Debug.Log ("Cancel button pushed");
				gm.TogglePause ();
			}
			rb.velocity = new Vector2 (Input.GetAxis ("Horizontal2") * maxSpeed, Input.GetAxis ("Vertical2") * maxSpeed);
			if (Input.GetButtonDown("Player2Fire") && attackReady) {
				Debug.Log ("Player 2 fired");
				StartCoroutine (AttackCooldown ());
			}
			if (isAttacking) {
				Debug.Log ("about to call Physics2D.OverlapCircle()");
				Collider2D[] colls = Physics2D.OverlapCircleAll (transform.position, attackRadius);
				Debug.Log ("Physics2D.OverlapCircle() called");
				foreach (Collider2D coll in colls) {
					if (coll.gameObject.CompareTag("Player1")) {
						coll.gameObject.GetComponent<PlayerController> ().decrementFoofers ();
						Vector2 force = coll.gameObject.transform.position - transform.position;
						coll.gameObject.GetComponent<PlayerController> ().getRigidbody ().AddForce (force * attackForceMultiplier);
						break;
					}
				}
			}
//			if (isSuperFoof && !player2ScaleComplete) {
//				transform.localScale = new Vector3 (transform.localScale.x + player2ScaleSpeed, transform.localScale.y + player2ScaleSpeed, 0);
//				cc.radius = superFoofRadius * (player2MinScale / transform.localScale.x);
//				if (transform.localScale.x >= (player2MinScale * player2ScaleFactor)) {
//					transform.localScale = new Vector3 ((player2MinScale * player2ScaleFactor), (player2MinScale * player2ScaleFactor), 0);
//					cc.radius = superFoofRadius * (player2MinScale / (player2MinScale * player2ScaleFactor));
//					player2ScaleComplete = true;
//				}
//			}
//			if (!isSuperFoof && !player2ScaleComplete) {
//				transform.localScale = new Vector3 (transform.localScale.x - player2ScaleSpeed, transform.localScale.y - player2ScaleSpeed, 0);
//				cc.radius = superFoofRadius * (player2MinScale / transform.localScale.x);
//				if (transform.localScale.x <= player2MinScale) {
//					transform.localScale = new Vector3 (player2MinScale, player2MinScale, 0);
//					cc.radius = superFoofRadius;
//					player2ScaleComplete = true;
//				}
//			}
			Debug.DrawRay (transform.position, new Vector3 (attackRadius, 0, 0));
			Debug.DrawRay (transform.position, new Vector3 (-attackRadius, 0, 0));
			Debug.DrawRay (transform.position, new Vector3 (0, attackRadius, 0));
			Debug.DrawRay (transform.position, new Vector3 (0, -attackRadius, 0));
		}
		if (rb.velocity.x > 0 && !facingRight) {
			Flip ();
		}
		else if (rb.velocity.x < 0 && facingRight) {
			Flip ();
		}
		if (Input.GetButtonDown("Cancel")) {
			Debug.Log ("Cancel button pushed");
			if (!pausePushed) {
				pausePushed = true;
				gm.TogglePause ();
				//StartCoroutine (PauseCoroutine ());
			}
		}
		StartCoroutine (PauseCoroutine ());
	}

	void Flip () {
		Vector2 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
		facingRight = !facingRight;
	}

	IEnumerator PauseCoroutine () {
		yield return new WaitForSeconds (0.1f);
		pausePushed = false;
	}

	void FixedUpdate () {

	}

	void OnCollisionEnter2D (Collision2D coll) {
		if (CompareTag("Player1")) {
			if (isAttacking) {
				if (coll.gameObject.CompareTag ("Player2")) {
					coll.gameObject.GetComponent<PlayerController> ().decrementFoofers ();
					Vector2 force = coll.gameObject.transform.position - transform.position;
					coll.gameObject.GetComponent<PlayerController> ().getRigidbody ().AddForce (force * attackForceMultiplier);
				}
			}
		}
		if (coll.gameObject.CompareTag("Boundary")) {
			audio.PlayOneShot (sounds [1]);
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
			anim.SetInteger ("foofers", foofers);
			audio.PlayOneShot (sounds [0]);
			if (CompareTag ("Player1")) {
				audio.PlayOneShot (player1FoofSounds [foofers - 1]);
			}
			else if (CompareTag("Player2")) {
				audio.PlayOneShot(player2FoofSounds[foofers - 1]);
			}
			GameObject.Destroy (other.gameObject);
			Debug.Log (foofers);
			if (foofers >= numFoofersRequired) {
				foofers = 0;
				anim.SetInteger ("foofers", foofers);
				StartCoroutine (SuperFoof ());
			}
			if (CompareTag ("Player1")) {
				uim.setPlayer1Foofers (foofers);
			} else if (CompareTag("Player2")) {
				uim.setPlayer2Foofers (foofers);
			}
		}

		else if (other.gameObject.CompareTag ("PowerUp")) {
			int powerUpType = other.gameObject.GetComponent<PowerUp> ().getPowerUpType ();
			audio.PlayOneShot (sounds [0]);
			if (CompareTag("Player1")) {
				gm.PowerUp (1, powerUpType);
			}
			else if (CompareTag("Player2")) {
				gm.PowerUp (2, powerUpType);
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
		anim.SetBool ("isSuperFoof", true);
		isAttacking = false;
		attackReady = false;
		isSuperFoof = true;
		player2ScaleComplete = false;
		yield return new WaitForSeconds (superFoofDuration);
		if (CompareTag ("Player1")) {
			Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer ("Player1"), LayerMask.NameToLayer ("Foofer"), false);
			Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer ("Player1"), LayerMask.NameToLayer ("PowerUp"), false);
		}
		if (CompareTag ("Player2")) {
			Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer ("Player2"), LayerMask.NameToLayer ("Foofer"), false);
			Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer ("Player2"), LayerMask.NameToLayer ("PowerUp"), false);
		}
		cc.enabled = false;
		particles.startLifetime = 2;
		emission = particles.emission;
		rate = emission.rate;
		rate.constantMax = 10;
		emission.rate = rate;
		attackReady = true;
		isAttacking = false;
		isSuperFoof = false;
		player2ScaleComplete = false;
		anim.SetBool ("isSuperFoof", false);
	}

	public void setReversed (bool r) {
		reversed = r;
	}

	public void setBlocked (bool l) {
		locked = l;
	}

	IEnumerator AttackCooldown () {
		isAttacking = true;
		attackReady = false;
		anim.SetBool("isAttacking", true);
		yield return new WaitForSeconds (attackDuration);
		anim.SetBool("isAttacking", false);
		isAttacking = false;
		yield return new WaitForSeconds (attackCooldown);
		attackReady = true;
	}

	public void decrementFoofers () {
		if (foofers > 0) {
			foofers--;
			anim.SetInteger ("foofers", foofers);
		}
		if (CompareTag("Player1")) {
			uim.setPlayer1Foofers (foofers);
		}
		else if (CompareTag("Player2")) {
			uim.setPlayer2Foofers (foofers);
		}
	}

	public Rigidbody2D getRigidbody () {
		return rb;
	}

	public void powerUpFeedback (int powerUpType) {
		StartCoroutine (PowerUpFeedbackCoroutine (powerUpType));
	}

	IEnumerator PowerUpFeedbackCoroutine (int powerUpType) {
		GameObject feedback = (GameObject)GameObject.Instantiate (powerUpFeedbackPrefab, new Vector3(transform.position.x + 2, transform.position.y + 2), Quaternion.identity, transform);
		//GameObject feedback = (GameObject)GameObject.Instantiate (powerUpFeedbackPrefab, new Vector3(0, 0, 0), Quaternion.identity);
		feedback.GetComponent<PowerUpFeedback> ().setPowerUpType (powerUpType);
		yield return new WaitForSecondsRealtime (gm.powerUpDuration);
		Destroy (feedback);
	}
}
