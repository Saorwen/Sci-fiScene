using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starship : MonoBehaviour {

	AudioSource sound;
	public AudioClip warpsound;
	public Transform target;
	public static bool ready = false;
	bool warp = true;
	bool move = false;
	bool fireDelay = false;
	public bool torpedosReady = false;

	public GameObject torpedo;

	float distToTarget;

	float currView;

	// Use this for initialization
	void Start () {
		currView = gameObject.GetComponentInChildren<Camera> ().fieldOfView;
		sound = gameObject.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt (target);

		distToTarget = Vector3.Distance (this.transform.position, target.position);
		if (gameObject.GetComponentInChildren<Camera> () != null) {
			if (gameObject.GetComponentInChildren<Camera> ().enabled == true && warp && ready) {
				gameObject.GetComponentInChildren<Camera> ().fieldOfView += 80f * Time.deltaTime;
				currView = gameObject.GetComponentInChildren<Camera> ().fieldOfView;
				StartCoroutine (Warp ());
				if (move) {
					transform.position = Vector3.MoveTowards (this.transform.position, target.position, 4.5f);
				}
			}
		}
		 if (distToTarget < 30f) {
			warp = false;
			if (gameObject.GetComponentInChildren<Camera> () != null) {
				while (gameObject.GetComponentInChildren<Camera> ().fieldOfView > 60f) {
					gameObject.GetComponentInChildren<Camera> ().fieldOfView -= Time.deltaTime * 5;
				}
			}
			if (!torpedosReady && !fireDelay) {
				StartCoroutine (ReadyTorpedos ());
			}
		}
			
	}

	public void fire () {
		StartCoroutine (FireTorpedos ());
	}

	IEnumerator Warp() {
		sound.pitch = 0.5f;
		sound.PlayOneShot(warpsound);
		yield return new WaitForSeconds (1.5f);
		move = true;
	}

	IEnumerator ReadyTorpedos() {
		fireDelay = true;
		yield return new WaitForSeconds (5f);
		torpedosReady = true;
	}

	IEnumerator FireTorpedos() {
		torpedosReady = false;
		GameObject pewpew = Instantiate (torpedo, transform.position, transform.rotation) as GameObject;

		yield return new WaitForSeconds (0.5f);

		GameObject pewpew2 = Instantiate (torpedo, transform.position, transform.rotation) as GameObject;
		fireDelay = false;
	}
}
