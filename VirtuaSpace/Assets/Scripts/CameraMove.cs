using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {
	public static Vector3 newPosition;
	
	void Awake () {
		newPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.Lerp (transform.position, newPosition, Time.deltaTime);
	}
}
