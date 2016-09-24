using UnityEngine;
using System.Collections;

public class ColorableObjectGenerator : MonoBehaviour {

	// public variables
	public GameObject colorableObject;
	public int width = 64;
	public int height = 36;

	// Use this for initialization
	void Awake () {
		for (int i = 0; i < width; ++i) {
			for (int j = 0; j < height; ++j) {
				GameObject co = (GameObject)GameObject.Instantiate (colorableObject, new Vector3 ((i - (width / 2)) * 0.5f, (j - (height / 2)) * 0.5f, 0), Quaternion.identity);
				if (i < (width / 2)) {
					co.GetComponent<ColorableObject> ().setBW ();
				}
			}
		}	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
