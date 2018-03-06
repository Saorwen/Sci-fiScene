using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour {

	Camera[] cams;
	int currentCameraIndex = 0;

	// Use this for initialization
	void Start () {
		Invoke ("SetCams", 2f);
		InvokeRepeating ("Swap", 5f, 5f);
	}

	void SetCams() {
		cams = Camera.allCameras;
		for (int i = 1; i < cams.Length; i++) {
			cams [i].gameObject.SetActive (false);
		}
	}
	
	// Update is called once per frame
	void Update () {
	}

	void Swap() {
		currentCameraIndex++;
		if (currentCameraIndex < cams.Length) {
			cams [currentCameraIndex - 1].gameObject.SetActive (false);
			cams [currentCameraIndex].gameObject.SetActive (true);
		} else {
			cams [currentCameraIndex - 1].gameObject.SetActive (false);
			currentCameraIndex = 0;
			cams [currentCameraIndex].gameObject.SetActive (true);
		}
	}
		
}
