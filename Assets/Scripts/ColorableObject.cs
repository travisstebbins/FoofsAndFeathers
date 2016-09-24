using UnityEngine;
using System.Collections;

public class ColorableObject : MonoBehaviour {

	public float transitionSpeed = 0.05f;

	// private variables
	private bool inColor = true;

	// components
	SpriteRenderer rend;

	// Use this for initialization
	void Start () {
		rend = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (inColor && rend.color.a < 1) {
			Debug.Log ("transitioning from BW to color");
			rend.color = new Color (rend.color.r, rend.color.g, rend.color.b, rend.color.a + transitionSpeed);
			if (rend.color.a > 1)
				rend.color = new Color (rend.color.r, rend.color.g, rend.color.b, 1);
		} else if (!inColor && rend.color.a > 0) {
			Debug.Log ("transitioning from color to BW");
			rend.color = new Color (rend.color.r, rend.color.g, rend.color.b, rend.color.a - transitionSpeed);
			if (rend.color.a < 0)
				rend.color = new Color (rend.color.r, rend.color.g, rend.color.b, 0);
		}
	}

	public void setColor () {
		inColor = true;
	}

	public void setBW () {
		inColor = false;
	}
}
