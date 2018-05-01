using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : SteeringBehaviour {
	public Vector3 targetPosition = Vector3.zero;
	public float slowingDistance = 100.0f;
	public float deceleration = 0.9f;
	public GameObject targetGameObject;

	// Use this for initialization
	void Start () {
		
	}

	public override Vector3 Calculate ()
	{
		return boid.ArriveForce(targetPosition, slowingDistance, deceleration);
	}
	
	// Update is called once per frame
	void Update () {
		if (targetGameObject != null) {
			targetPosition = targetGameObject.transform.position;
		} else {
			targetPosition = transform.position;
		}
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.blue;
		Gizmos.DrawLine (this.transform.position, targetPosition);
	}
}
