using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Captain : MonoBehaviour {

	void OnEnable() {
		if (gameObject.GetComponentInParent<Starship> ().torpedosReady == true && Borg.hostilePresent) {
			gameObject.GetComponentInParent<Starship> ().fire();

		}
	}
}
