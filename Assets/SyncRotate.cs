using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncRotate : MonoBehaviour {

	Material skyboxMaterial;
	float lastRotation;

	void Start () {
		skyboxMaterial = FindObjectOfType<Skybox> ().material;	
	}
	

	void Update () {
		if (lastRotation != transform.localEulerAngles.y) {
			lastRotation = transform.localEulerAngles.y;
			skyboxMaterial.SetFloat ("_Rotation", -lastRotation);
		}
	}
}
