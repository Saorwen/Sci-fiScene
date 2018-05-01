using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Borg : MonoBehaviour {

	public static bool hostilePresent = true;

	public int damage;
	public GameObject debris;
	public GameObject laser;
	GameObject beam;
	List<GameObject> ships = new List<GameObject>();
	GameObject[] beams;

	AudioSource sound;
	public GameObject soundMan;
	public AudioClip boomsound;

	// Use this for initialization
	void Start () {
		sound = soundMan.gameObject.GetComponent<AudioSource> ();
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Starship") {
			ships.Add (other.gameObject);
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.gameObject.tag == "Starship") {
			ships.Remove (other.gameObject);
			other.gameObject.GetComponentInParent<Boid> ().takingDamage = false;
			other.gameObject.GetComponentInParent<Boid> ().targeted = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < ships.Count; i++) {
			if (ships [i] != null) {
				if (ships [i].gameObject.GetComponentInParent<Boid> ().shooting == false) {
					ships [i].gameObject.GetComponentInParent<Boid> ().Engage ();
				}
				if(ships [i].gameObject.GetComponentInParent<Boid> ().targeted == true) {
					ships [i].gameObject.GetComponentInParent<Boid> ().takingDamage = true;
				}
			}
			beam = Instantiate (laser, transform) as GameObject;
			if (beam != null && ships [i] != null && ships[i].GetComponentInParent<Boid>().targeted == true) {
				beam.GetComponent<LineRenderer> ().SetPosition (1, ships [i].transform.position);
			}
			Destroy (beam, 0.5f);
		}
			

		if (damage > 100) {
			sound.pitch = 0.6f;
			sound.priority = 1;
			sound.volume = 1;
			sound.PlayOneShot (boomsound);
			GameObject boom = GameObject.Instantiate(debris, transform.position, transform.rotation) as GameObject;
			hostilePresent = false;
			Destroy (this.gameObject);
		}
	}
		
}
