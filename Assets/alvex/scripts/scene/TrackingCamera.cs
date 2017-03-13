using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;


public class TrackingCamera: MonoBehaviour {
	
	List<Vector3> positions;
	List<Vector3> rotations;
	List<Vector3> settings;

	public Text focus;
	public Text distance;

	string focusText;
	string distanceText;

	void Start() {
		focusText = focus.text;
		distanceText = distance.text;
	}

	public void ApplyState (int index, float lerpTo) {
		Vector3 pos = Utils.lerp (positions, index, lerpTo);
		transform.localPosition = Utils.asPosition (pos);

		transform.localRotation = Utils.lerpAngles(rotations, index, lerpTo);

		Vector3 cameraSettings = Utils.lerp (settings, index, lerpTo);
		GetComponentInChildren<Camera>().fieldOfView = Utils.rad2deg(cameraSettings).x;
		focus.text = focusText + " " + cameraSettings.y;
		distance.text = distanceText + " " + cameraSettings.z;
	}

	public void LoadCamera(string path) {
		positions = new List<Vector3> ();
		rotations = new List<Vector3> ();
		settings = new List<Vector3> ();
		foreach (var floats in Parser.loadFloats (path)) {
			positions.Add(new Vector3 (floats [0], floats [1], floats [2]));
			rotations.Add(new Vector3 (floats [3], floats [4], floats [5]));
			settings.Add(new Vector3 (floats [6], floats [7], floats [8]));
		}
	}
}

