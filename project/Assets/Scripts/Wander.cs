using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : SteeringBehaviour {

	public float radius = 5;
	public float distance = 180;
	public float jitter = 5f;

	public Transform centre;
	public float distFromCentre = 70;

	public float maxTimeTillExit = 5f;

	Vector3 target;
	Vector3 worldTarget;

	// Use this for initialization
	void Start () {
		target = Random.onUnitSphere * distFromCentre;
	}

	void OnEnable() {
		Invoke ("attack", Random.Range (1f, maxTimeTillExit));
	}

	// Update is called once per frame
	public override Vector3 Calculate() {
		float jitterTimeSlice = Time.deltaTime * jitter;

		Vector3 offset = Random.insideUnitSphere * jitterTimeSlice;
		target += offset;
		target.Normalize();
		target *= radius;

		Vector3 localtarget = target + Vector3.forward * distance;
		worldTarget = transform.TransformPoint(localtarget);

		return worldTarget - transform.position;

	}

	void setPoint() {
		target = Random.onUnitSphere * distFromCentre;
	}

	void attack() {
		gameObject.GetComponent<Attack> ().enabled = true;
		gameObject.GetComponent<Wander> ().enabled = false;
		gameObject.GetComponent<Regroup> ().enabled = false;
	}
}
