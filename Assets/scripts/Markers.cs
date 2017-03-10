using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Markers : HasVectorList {

	public GameObject markers;
	public GameObject cross;

	public override void SetList(List<Vector3> data) {
		base.SetList (data);

		if (cross)
			cross.SetActive(data.Count == 0);

		while (markers.transform.childCount > 0) {
			Destroy (markers.transform.GetChild(0));
		}

		foreach (var p in data) {
			GameObject marker = GameObject.CreatePrimitive (PrimitiveType.Sphere);
			Destroy (marker.GetComponent<Collider> ());
			marker.transform.parent = markers.transform;
			marker.transform.localPosition = new Vector3(p.z, p.y, p.x);
		}
	}
}
