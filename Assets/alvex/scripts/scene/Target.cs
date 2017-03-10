﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

	List<Vector3> positions;
	List<Vector3> rotations;

	public Transform markers;
	public Transform model;

	Transform mesh;

	TargetDescription currentState = new TargetDescription ();

	public void ApplyState (int index, float lerpTo) {
		Vector3 pos = Utils.lerpPosition (positions, index, lerpTo);
		transform.localPosition = Utils.asPosition(pos);
		//targetPosition.vectorField.AssignValues (pos);

		transform.localRotation = Utils.lerpRotation(rotations, index, lerpTo);
		//targetRotation.vectorField.AssignValues (rad2deg (lerp (targetRotation.GetData)));
	}

	public void LoadModel(string path) {
		if (currentState.model == path) {
			return;
		}

		DestroyChildren (model);

		mesh = OBJLoader.LoadOBJFile (path).transform;
		mesh.parent = model;
		mesh.localScale = Vector3.one;
		mesh.localPosition = Vector3.zero;
		mesh.localEulerAngles = Vector3.zero;

		currentState.model = path;
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
			marker.transform.localPosition = new Vector3(floats[2], floats[1], floats[0]);
			marker.GetComponent<MeshRenderer> ().material.shader = Shader.Find ("Unlit/Color");
			marker.GetComponent<MeshRenderer> ().material.color = new Color (floats [3], floats [4], floats [5]);
		}

		currentState.markers = path;
	}

	public void LoadTrajectory (string path) {
		if (currentState.trajectory == path) {
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
		mesh.localPosition = modelCenter;
	}

	public void Rotate (Vector3 modelRotation) {
		mesh.localEulerAngles = modelRotation;
	}

	public void Scale (float scale) {
		mesh.localScale = new Vector3 (scale, scale, scale);
	}
}