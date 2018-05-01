using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terminate : MonoBehaviour {
	public GameObject boom;
	public AudioClip boomSound;
	AudioSource sound;

	// Use this for initialization
	void Start () {
		Destroy (this.gameObject, 1.5f);
	}

	void OnCollisionEnter(Collision other) {
		if (other.gameObject.tag == "Borg") {
			if (this.gameObject.tag == "phaser") {
				other.gameObject.GetComponent<Borg> ().damage++;
			}
			if (this.gameObject.tag == "PhotonTorpedo") {
				GameObject kaboom = GameObject.Instantiate(boom, transform.position, transform.rotation) as GameObject;
				other.gameObject.GetComponent<Borg> ().damage += 20;
				sound = gameObject.GetComponent<AudioSource> ();
				sound.PlayOneShot (boomSound);

			}
		}
		Destroy (this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * 40);
	}
}