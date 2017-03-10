using System;
using UnityEngine;
using System.Collections.Generic;


public class TrackingCamera: MonoBehaviour {
	
	List<Vector3> positions;
	List<Vector3> rotations;
	List<Vector3> settings;

	public void ApplyState (int index, float lerpTo) {

		Vector3 pos = Utils.lerpPosition (positions, index, lerpTo);
		//cameraPosition.vectorField.AssignValues (pos);
		transform.localPosition = Utils.asPosition (pos);

		//cameraRotation.vectorField.AssignValues (rad2deg (lerp (cameraRotation.GetData)));
		transform.localRotation = Utils.lerpRotation(rotations, index, lerpTo);

		GetComponentInChildren<Camera>().fieldOfView = Utils.rad2deg(Utils.lerpPosition(settings, index, lerpTo)).x;
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

