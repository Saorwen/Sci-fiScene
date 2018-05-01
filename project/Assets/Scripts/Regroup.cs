using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regroup : SteeringBehaviour {
	RaycastHit hit;
	Vector3 p1; 
	Vector3 p2;
	public float radius = 60f;
	bool following = false;

	Vector3 target;
	public float offset = 20f;

	// Use this for initialization
	void Start () {
		p1 = transform.position + (Vector3.forward * 2);
		p2 = transform.position + (Vector3.forward * 30);
		InvokeRepeating ("randomiseOffset", 2f, 2f);
	}
	public override Vector3 Calculate() {
		if (Physics.CapsuleCast (p1, p2, radius, Vector3.forward, out hit)) {
			if (hit.collider.gameObject.tag == "Starship" && hit.collider.gameObject.GetComponentInParent<Regroup>().following == false){
				target = hit.transform.position + Vector3.right * offset;
			}
		}

		return target;
	}

	void randomiseOffset() {
		offset = Random.Range (40f, -40f);
	}


}
