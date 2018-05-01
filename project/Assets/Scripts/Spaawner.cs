using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaawner : MonoBehaviour {

	public GameObject HostileShip;
	public GameObject LargeShip;
	public GameObject Hostiles;

	public int smallShipAmount = 8;
	public int largeShipAmount = 4;

	float spawnOffset = 30;
	int HostileShipIndex = 0;
	int LargeShipIndex = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (HostileShipIndex < smallShipAmount) {
			GameObject shipClone = Instantiate (HostileShip, transform.position + new Vector3(Random.Range(-spawnOffset * 3, spawnOffset * 3), Random.Range(-spawnOffset * 2, spawnOffset * 2), Random.Range(-spawnOffset * 3, spawnOffset * 3)), transform.rotation , Hostiles.transform) as GameObject;
			HostileShipIndex++;
		}

		if (LargeShipIndex < largeShipAmount) {
			GameObject largeShipClone = Instantiate (LargeShip, transform.position + Random.onUnitSphere * 300f, Quaternion.Euler(-90, 0, 0), Hostiles.transform) as GameObject;
			LargeShipIndex++;
		}

	}
}
