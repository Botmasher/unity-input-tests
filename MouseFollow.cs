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

	// raycasting for finding target position
	private RaycastHit hit;
	private Ray ray;

	// camera targeting and movement
	private Vector3 startingPos;
	private Vector3 targetPos;
	private bool targetAcquired;
	private float travelSpeed;


	void Awake () {
		// camera references
		mc = Camera.main;

		// initial camera movement
		startingPos = Camera.main.transform.position;
		targetPos = startingPos;
		travelSpeed = 1f;

		// default values for states
		targetAcquired = false;

	}


	void Update () {
		
		// cast ray at current mouse position and find any tagged targets
		ray = (mc.ScreenPointToRay (Input.mousePosition));
		Physics.Raycast (ray, out hit);
		if (hit.collider != null && hit.collider.tag == taggedTarget.tag) {
			// store x and y of target, add z distance to keep camera off target
			targetPos = new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z-8f);
			// activate check and speed for movement toward target
			targetAcquired = true;
			travelSpeed = 1f;
		}

		// check if near a recently acquired target
		if (targetAcquired && Mathf.Abs(mc.transform.position.x - targetPos.x) < 0.1f ) {
			// reset state
			targetAcquired = false;
			// reset target to resting position with slow movement
			targetPos = startingPos;
			travelSpeed = 0.26f;
		} else {
			// lerp camera towards target
			mc.transform.position = Vector3.Lerp (mc.transform.position, targetPos, travelSpeed*Time.deltaTime);
		}

	}

}

}