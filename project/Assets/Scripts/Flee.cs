using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flee : SteeringBehaviour {
	public Vector3 targetPosition = Vector3.zero;
	public GameObject targetGameObject;
	public float startFleeSpeed = 2f;
	public float fleeingAcc = 5;
	public float maxFleeingOffset = 150f;
	float fleeingOffset;

	bool fleeing = false;

	// Use this for initialization
	void Start () {

	}

	void OnEnable() {
		fleeingOffset = Random.Range (-maxFleeingOffset, maxFleeingOffset);
		Invoke ("wandering", 5f);
	}

	public override Vector3 Calculate ()
	{
		if (Borg.hostilePresent) {
			targetPosition = Vector3.MoveTowards (transform.position, targetGameObject.transform.position + Vector3.right * fleeingOffset, -startFleeSpeed * gameObject.GetComponent<Boid> ().maxSpeed * Time.deltaTime);
			startFleeSpeed += fleeingAcc * Time.deltaTime;
			return targetPosition - transform.position;
		} else {
			return transform.position;
		}
	}

	// Update is called once per frame
	void Update () {
	}

	void wandering(){
		if (gameObject.GetComponent<Boid> ().takingDamage == false) {
			targetPosition = Vector3.zero;
			gameObject.GetComponent<Wander> ().enabled = true;
			gameObject.GetComponent<Regroup> ().enabled = true;
			gameObject.GetComponent<Flee> ().enabled = false;
		} else {
			StartCoroutine (checkForDamage());
		}

	}

	IEnumerator checkForDamage() {
		yield return new WaitForSeconds (3f);
		wandering ();
	}

}
