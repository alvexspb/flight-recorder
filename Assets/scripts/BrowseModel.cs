using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrowseModel : Browse {

	public GameObject parent;

	protected override IEnumerator load(string path) {

		yield return new WaitForSecondsRealtime (0.1f);

		Utils.DestroyChildren (parent);

		var t = OBJLoader.LoadOBJFile (path).transform;
		t.parent = parent.transform;
		t.localScale = Vector3.one;
		t.localPosition = Vector3.zero;
		t.localEulerAngles = Vector3.zero;

	}
		
}
