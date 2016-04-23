using UnityEngine;
using System.Collections;

public class MouseTracker : MonoBehaviour {

	/**
	 * 	Script camera to move towards mouseover targets
	 * 	 1. Place script on any game object
	 * 	 2. Drag game object with tagged target you're tracking to inspector slot
	 */
	public Transform taggedTarget; 	// game object with tag to track

	// store reference to main scene camera
	private Camera mc;

	// raycasting for new position
	private RaycastHit hit;
	private Ray ray;
	private Vector3 targetPos;


	void Awake () {
		// pointers to game objects
		mc = Camera.main;
	}


	void Update () {
		
		// cast ray at current mouse position and find any tagged targets
		ray = (mc.ScreenPointToRay (Input.mousePosition));
		Physics.Raycast (ray, out hit);
		if (hit.collider != null && hit.collider.tag == taggedTarget.tag) {
			// store x and y but not z
			targetPos = new Vector3(hit.transform.position.x, hit.transform.position.y, mc.transform.position.z);
		}

		// lerp camera towards hit tagged target
		if (mc.transform.position != targetPos) {
			mc.transform.position = Vector3.Lerp (mc.transform.position, targetPos, Time.deltaTime);
		}

	}

}