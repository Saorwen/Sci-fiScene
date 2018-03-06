using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaawner : MonoBehaviour {

	public GameObject HostileShip;
	public GameObject Hostiles;

	float spawnOffset = 10;
	int HostileShipIndex = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (HostileShipIndex < 8) {
			GameObject shipClone = Instantiate (HostileShip, transform.position + new Vector3(Random.Range(-spawnOffset, spawnOffset), Random.Range(-spawnOffset/2, spawnOffset/2), Random.Range(-spawnOffset/4, spawnOffset)), transform.rotation , Hostiles.transform) as GameObject;
			HostileShipIndex++;
		}
	}
}
