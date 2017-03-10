using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrowseMarkers : Browse {

	public GameObject markersHolder;

	protected override IEnumerator load (string path) {

		yield return new WaitForSecondsRealtime (0.1f);

		Utils.DestroyChildren (markersHolder);

		foreach (var floats in Parser.loadFloats (path)) {
			GameObject marker = GameObject.CreatePrimitive (PrimitiveType.Sphere);
			Destroy (marker.GetComponent<Collider> ());
			marker.transform.parent = markersHolder.transform;
			marker.transform.localPosition = new Vector3(floats[2], floats[1], floats[0]);
			marker.GetComponent<MeshRenderer> ().material.shader = Shader.Find ("Unlit/Color");
			marker.GetComponent<MeshRenderer> ().material.color = new Color (floats [3], floats [4], floats [5]);
		}
		
	}
}