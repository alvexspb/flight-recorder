using System;
using UnityEngine;
using System.Collections;

public class BrowseVectorList : Browse {
	
	public HasVectorList targetValuesList;

	protected override IEnumerator load (string path) {

		yield return new WaitForSecondsRealtime (0.1f);

		var list = Parser.Load (path);
		targetValuesList.SetList (list);
		foreach (var handler in GetComponents<FieldValueAssigner> ()) {
			handler.onLoad (list);
		}

		GameObject.Find ("Launch").GetComponent<Launch> ().StopLaunch ();
	}
}


