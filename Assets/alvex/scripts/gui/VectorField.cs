using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VectorField : MonoBehaviour {

	public InputField x;
	public InputField y;
	public InputField z;

	public void AssignValues(Vector3 data) {
		if (x) x.text = "" + data.z;
		if (y) y.text = "" + data.y;
		if (z) z.text = "" + data.x;
	}
		
	public Vector3 GetValue () {
		return new Vector3 (
			Utils.stringToFloat (z.text),
			Utils.stringToFloat (y.text),
			Utils.stringToFloat (x.text)
		);
	}
}
