using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	// public variables
	public bool centered = true;
	public Transform target;
	public Transform upperBound;
	public Transform lowerBound;
	public Transform leftBound;
	public Transform rightBound;

	// Use this for initialization
	void Start () {
		transform.position = new Vector3 (target.position.x, target.position.y, transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = new Vector3 (target.position.x, target.position.y, transform.position.z);
		if (target.transform.position.x + GetComponent<Camera>().orthographicSize * (16.0 / 18.0) > rightBound.position.x) {
			//transform.position = new Vector3 (transform.position.x, target.position.y, transform.position.z);
			pos.x = transform.position.x;
		} else if (target.transform.position.x - GetComponent<Camera>().orthographicSize * (16.0 / 18.0) < leftBound.position.x) {
			//transform.position = new Vector3 (transform.position.x, target.position.y, transform.position.z);
			pos.x = transform.position.x;
		}
		if (target.transform.position.y + GetComponent<Camera>().orthographicSize * (16.0 / 18.0) > upperBound.position.y) {
			//transform.position = new Vector3 (target.position.x, transform.position.y, transform.position.z);
			pos.y = transform.position.y;
		} else if (target.transform.position.y - GetComponent<Camera>().orthographicSize * (16.0 / 18.0) < lowerBound.position.y) {
			//transform.position = new Vector3 (target.position.x, transform.position.y, transform.position.z);
			pos.y = transform.position.y;
		} 
		transform.position = pos;
	}
}
