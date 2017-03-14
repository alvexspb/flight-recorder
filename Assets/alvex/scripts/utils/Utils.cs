using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils {

	public static float stringToFloat (string s) {
		double value = 0d;
		System.Double.TryParse(s, out value);
		return (float) value;
	}
		
	public static Vector3 lerp(List<Vector3> points, int index, float lerpTo) {
		Vector3 left = points [index];
		if (index + 1 == points.Count) { 
			return left;
		} 
		return Vector3.Lerp (left, points [index + 1], lerpTo);
	}

	public static Quaternion lerpAngles(List<Vector3> rotatrions, int index, float lerpTo) {
		Quaternion left = asQuaternion(rotatrions [index]);
		if (index + 1 == rotatrions.Count) { 
			return left;
		} 
		return Quaternion.Lerp (left, asQuaternion(rotatrions [index + 1]), lerpTo);
	}

	public static Vector3 asPosition (Vector3 pos) {
		return new Vector3 (pos.z, pos.y, pos.x);
	}

	public static Quaternion asQuaternion (Vector3 rot) {
		return Quaternion.Euler(rad2deg(new Vector3 (-rot.y, rot.x, -rot.z)));
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
