using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;


public class BrowseLaunch : Browse {

	public HasVectorList targetPositions;
	public HasVectorList targetRotations;

	public HasVectorList trackerPositions;
	public HasVectorList trackerRotations;

	public HasVectorList cameraProperties;

	protected override IEnumerator load (string path) {

		yield return new WaitForSecondsRealtime (0.1f);

		List<Vector3> targetPositionsList = new List<Vector3> ();
		List<Vector3> targetRotationsList = new List<Vector3> ();
		List<Vector3> trackerPositionsList = new List<Vector3> ();
		List<Vector3> trackerRotationsList = new List<Vector3> ();
		List<Vector3> cameraPropertiesList = new List<Vector3> ();

		Parser.LoadLaunch (path, targetPositionsList, targetRotationsList, trackerPositionsList, trackerRotationsList, cameraPropertiesList);

		targetPositions.SetList (targetPositionsList);
		targetRotations.SetList (targetRotationsList);
		trackerPositions.SetList (trackerPositionsList);
		trackerRotations.SetList (trackerRotationsList);
		cameraProperties.SetList (cameraPropertiesList);

		GameObject.Find ("Launch").GetComponent<Launch> ().StopLaunch ();
	}
}


