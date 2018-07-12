using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Target : MonoBehaviour {

	List<Vector3> positions;
	List<Vector3> rotations;

	public Transform markers;
	public Transform model;

	Transform mesh;

	TargetDescription currentState = new TargetDescription ();

	public void ApplyState (int index, float lerpTo) {
		if (positions == null || rotations == null) {
			return;
		}

		Vector3 pos = Utils.lerp (positions, index, lerpTo);
		transform.localPosition = Utils.asPosition(pos);
		transform.localRotation = Utils.lerpAngles (rotations, index, lerpTo);
	}

	public void LoadModel(string path) {
		if (currentState.model == path) {
			return;
		}

		DestroyChildren (model);

		try {
			mesh = OBJLoader.LoadOBJFile (path).transform;
			mesh.parent = model;
			mesh.localScale = Vector3.one;
			mesh.localPosition = Vector3.zero;
			mesh.localEulerAngles = Vector3.zero;
			currentState.model = path;
		} catch (Exception e) {
			Debug.Log (e);
		}
	}

	public void LoadMarkers (string path) {
		if (currentState.markers == path) {
			return;
		}

		DestroyChildren (markers);

		foreach (var floats in Parser.loadFloats (path)) {
			GameObject marker = GameObject.CreatePrimitive (PrimitiveType.Sphere);
			Destroy (marker.GetComponent<Collider> ());
			marker.transform.parent = markers;
			marker.transform.localScale = new Vector3 (floats[6], floats[6], floats[6]);
			marker.transform.localPosition = new Vector3(floats[2], floats[1], floats[0]);
			marker.GetComponent<MeshRenderer> ().material.shader = Shader.Find ("Unlit/Color");
			marker.GetComponent<MeshRenderer> ().material.color = new Color (floats [3], floats [4], floats [5]);
		}

		currentState.markers = path;
	}

	public void LoadTrajectory (string path) {
		if (currentState.trajectory == path || path == "") {
			return;
		}

		positions = new List<Vector3> ();
		rotations = new List<Vector3> ();
		foreach (var floats in Parser.loadFloats (path)) {
			positions.Add(new Vector3 (floats [0], floats [1], floats [2]));
			rotations.Add(new Vector3 (floats [3], floats [4], floats [5]));
		}

		currentState.trajectory = path;
	}

	protected void DestroyChildren (Transform parent) {
		for (int i = parent.childCount; i > 0; i--) {
			Transform child = parent.GetChild (i - 1);
			child.parent = null;
			Destroy (child.gameObject);
		}
	}

	public void Translate (Vector3 modelCenter) {
		if (null != mesh) {
			mesh.localPosition = modelCenter;
		}
	}

	public void Rotate (Vector3 modelRotation) {
		if (null != mesh) {
			mesh.localEulerAngles = modelRotation;
		}
	}

	public void Scale (float scale) {
		if (null != mesh) {
			mesh.localScale = new Vector3 (scale, scale, scale);
		}
	}

	public void ShowMarkers (bool showMarkers) {
		markers.gameObject.SetActive (showMarkers);
	}
}
