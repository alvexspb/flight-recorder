using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrowseCamera : Browse {

	protected override IEnumerator load (string path) {

		yield return new WaitForSecondsRealtime (0.1f);

		FindObjectOfType<TrackingCamera> ().LoadCamera (path);
	}
}
