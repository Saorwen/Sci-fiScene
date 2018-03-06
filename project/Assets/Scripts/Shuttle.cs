using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuttle : MonoBehaviour {

	public Transform target;
	public float speed = 1;

	public bool fleeing = false;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (target != null && !fleeing) {
			MoveTo ();
		}
	}

	void MoveTo() {
		Vector3 dir = target.position - transform.position;
		transform.LookAt (target);
		transform.position = Vector3.Lerp (transform.position, dir, Time.deltaTime * speed);
	}
}
