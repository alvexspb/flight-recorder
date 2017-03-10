using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : HasVectorList {

	LineRenderer line;

	void Start() {
		line = GetComponent<LineRenderer> ();
	}

	public override void SetList(List<Vector3> data) {
		base.SetList (data);

		line.numPositions = data.Count;
		line.SetPositions (data.ToArray ());
	}
}
