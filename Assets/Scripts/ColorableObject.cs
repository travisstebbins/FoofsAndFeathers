using UnityEngine;
using System.Collections;

public class ColorableObject : MonoBehaviour {

	// public variables
	public float transitionSpeed = 0.05f;
	public bool inColor = true;

	// components
	SpriteRenderer rend;

	// Use this for initialization
	void Start () {
		rend = GetComponent<SpriteRenderer> ();
		if (inColor) {
			rend.color = new Color (rend.color.r, rend.color.g, rend.color.b, 1);
		} else {
			rend.color = new Color (rend.color.r, rend.color.g, rend.color.b, 0);
		}
	}

	void OnLevelWasLoaded () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (inColor && rend.color.a < 1) {
			rend.color = new Color (rend.color.r, rend.color.g, rend.color.b, rend.color.a + transitionSpeed);
			if (rend.color.a >= 1) {
				rend.color = new Color (rend.color.r, rend.color.g, rend.color.b, 1);
			}
		} else if (!inColor && rend.color.a > 0) {
			rend.color = new Color (rend.color.r, rend.color.g, rend.color.b, rend.color.a - transitionSpeed);
			if (rend.color.a <= 0) {
				rend.color = new Color (rend.color.r, rend.color.g, rend.color.b, 0);
			}
		}
	}

	public void setColor () {
		inColor = true;
	}

	public void setBW () {
		inColor = false;
	}
}
