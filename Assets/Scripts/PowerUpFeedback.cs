using UnityEngine;
using System.Collections;

public class PowerUpFeedback : MonoBehaviour {

	// public variables
	public Sprite[] powerUpSprites;

	// components
	SpriteRenderer rend;
	Animator anim;

	// Use this for initialization
	void Awake () {
		rend = GetComponent<SpriteRenderer> ();
		anim = GetComponent<Animator> ();
		if (rend == null) {
			Debug.Log ("power up feedback sprite renderer null");
		}
		if (anim == null) {
			Debug.Log ("power up feedback animator null");
		}
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void setPowerUpType (int powerUpType) {
		rend.sprite = powerUpSprites [powerUpType];
		anim.SetInteger ("powerUpType", powerUpType);
	}
}
