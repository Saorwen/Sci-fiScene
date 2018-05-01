using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraSwitch : MonoBehaviour {

	Camera[] cams;
	int currentCameraIndex = 0;
	public GameObject border;
	AudioListener al;

	// Use this for initialization
	void Awake () {
		Invoke ("SetCams", 1f);
		InvokeRepeating ("Swap", 5f, 5f);
	}

	void SetCams() {
		border.gameObject.GetComponent<Image> ().enabled = false;
		cams = Camera.allCameras;
		for (int i = 1; i < cams.Length; i++) {
			cams [i].gameObject.SetActive (false);
		}
		Starship.ready = true;
	}
	
	// Update is called once per frame
	void Update () {
	}

	void Swap() {
		if (Borg.hostilePresent) {
			currentCameraIndex++;
			if (currentCameraIndex < cams.Length) {
				Destroy (cams [currentCameraIndex - 1].gameObject.GetComponent <AudioListener> ());
				cams [currentCameraIndex - 1].gameObject.SetActive (false);
				cams [currentCameraIndex].gameObject.SetActive (true);
				al = cams [currentCameraIndex].gameObject.AddComponent<AudioListener> () as AudioListener;
				if (cams [currentCameraIndex].gameObject.tag == "LargerShip") {
					border.gameObject.GetComponent<Image> ().enabled = true;
				} else {
					border.gameObject.GetComponent<Image> ().enabled = false;
				}
			} else {
				Destroy (cams [currentCameraIndex - 1].gameObject.GetComponent <AudioListener> ());
				cams [currentCameraIndex - 1].gameObject.SetActive (false);
				currentCameraIndex = 0;
				cams [currentCameraIndex].gameObject.SetActive (true);
				al = cams [currentCameraIndex].gameObject.AddComponent<AudioListener> () as AudioListener;
				if (cams [currentCameraIndex].gameObject.tag == "LargerShip") {
					border.gameObject.GetComponent<Image> ().enabled = true;
				} else {
					border.gameObject.GetComponent<Image> ().enabled = false;
				}
			}
		}
	}
		
}
