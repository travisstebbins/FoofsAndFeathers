using UnityEngine;
using System.Collections;

public class ColorableObjectGenerator : MonoBehaviour {

	// public variables
	public GameObject colorableObject;
	public Texture2D image;
	public Texture2D bwImage;
	public int width = 64;
	public int height = 36;

	// Use this for initialization
	void Awake () {
		for (int i = 0; i < width; ++i) {
			for (int j = 0; j < height; ++j) {
				GameObject co = (GameObject)GameObject.Instantiate (colorableObject, new Vector3 ((i - (width / 2)) * 0.5f, (j - (height / 2)) * 0.5f, 0), Quaternion.identity);
				int x = (int)((((float)i) / width) * image.width);
				int y = (int)((((float)j) / height) * image.height);
				int bwX = (int)((((float)i) / width) * bwImage.width);
				int bwY = (int)((((float)j) / height) * bwImage.height);
				//Debug.Log ("(" + x + ", " + y + ")");
				//Color c = new Color(image.texture.GetPixel (x, y).r, image.texture.GetPixel (x, y).g, image.texture.GetPixel (x, y).b, 1);
				co.GetComponent<SpriteRenderer> ().color = image.GetPixel(x, y);
				co.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer> ().color = bwImage.GetPixel (bwX, bwY);
				//co.GetComponentInChildren<SpriteRenderer> ().color = bwImage.GetPixel (x, y);
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
