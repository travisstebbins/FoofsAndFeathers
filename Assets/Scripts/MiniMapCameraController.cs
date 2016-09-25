using UnityEngine;
using System.Collections;

public class MiniMapCameraController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Camera c = GetComponent<Camera> ();
		c.aspect = 2.2388f;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
