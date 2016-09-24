using UnityEngine;
using System.Collections;

public class ColorableObjectGenerator : MonoBehaviour {

	// public variables
	public GameObject colorableObject;
	public Texture2D image;
	public Texture2D bwImage;
	public int width = 64;
	public int height = 36;
	public float resolution = 2;

	// Use this for initialization
	void Awake () {
		for (int i = 0; i < (width * resolution); ++i) {
			for (int j = 0; j < (height * resolution); ++j) {
				GameObject co = (GameObject)GameObject.Instantiate (colorableObject, new Vector3 ((i - (width * resolution / 2)) * (0.5f / resolution), (j - (height * resolution/ 2)) * (0.5f / resolution), 0), Quaternion.identity);
				int x = (int)((((float)i) / (width * resolution)) * image.width);
				int y = (int)((((float)j) / (height * resolution)) * image.height);
				int bwX = (int)((((float)i) / (width * resolution)) * bwImage.width);
				int bwY = (int)((((float)j) / (height * resolution)) * bwImage.height);
				//Debug.Log ("(" + x + ", " + y + ")");
				//Color c = new Color(image.texture.GetPixel (x, y).r, image.texture.GetPixel (x, y).g, image.texture.GetPixel (x, y).b, 1);
				co.GetComponent<SpriteRenderer> ().color = image.GetPixel(x, y);
				co.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer> ().color = bwImage.GetPixel (bwX, bwY);
				//co.GetComponentInChildren<SpriteRenderer> ().color = bwImage.GetPixel (x, y);
				if (i < (width * resolution / 2)) {
					co.GetComponent<ColorableObject> ().setBW ();
				}
			}
		}	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
