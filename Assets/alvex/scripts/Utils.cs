using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils {

	public static float stringToFloat (string s) {
		double value = 0d;
		System.Double.TryParse(s, out value);
		return (float) value;
	}
		
	public static Vector3 lerpPosition(List<Vector3> positions, int index, float lerpTo) {
		Vector3 left = positions [index];
		Vector3 right = positions [index + 1];
		return Vector3.Lerp (left, index + 1 < positions.Count ? right : left, lerpTo);
	}

	public static Quaternion lerpRotation(List<Vector3> rotatrions, int index, float lerpTo) {
		Quaternion left = Quaternion.Euler (asAngles(rotatrions [index]));
		Quaternion right = Quaternion.Euler (asAngles(rotatrions [index + 1]));
		return Quaternion.Lerp (left, index + 1 < rotatrions.Count ? right : left, lerpTo);
	}

	public static Vector3 asPosition (Vector3 pos) {
		return new Vector3 (pos.z, pos.y, pos.x);
	}

	public static Vector3 asAngles (Vector3 rot) {
		return rad2deg(new Vector3 (-rot.y, rot.x, -rot.z));
	}

	public static Vector3 rad2deg (Vector3 v) {
		return new Vector3 (v.x * Mathf.Rad2Deg, v.y * Mathf.Rad2Deg, v.z * Mathf.Rad2Deg);
	}

	public static void DestroyChildren (GameObject parent) {
		for (int i = parent.transform.childCount; i > 0; i--) {
			Transform child = parent.transform.GetChild (i - 1);
			child.SetParent(null);
			GameObject.Destroy (child.gameObject);
		}
	}

}
