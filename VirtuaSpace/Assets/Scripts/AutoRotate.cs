using UnityEngine;
using System.Collections;

public class AutoRotate : MonoBehaviour {
	public float rotationSpeed = 0.05f;

	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.up * Time.deltaTime, rotationSpeed);
	}
}
