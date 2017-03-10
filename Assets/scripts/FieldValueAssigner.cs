using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FieldValueAssigner : MonoBehaviour {

	public InputField field;

	public VectorElement element;

	public void onLoad (List<Vector3> values) {
		field.text = "" + extract (values [0]);
	}
		
	float extract (Vector3 v) {
		if (element == VectorElement.X) 
			return v.x;
		if (element == VectorElement.Y) 
			return v.y;
		return v.z;
	}
}

