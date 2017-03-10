using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VectorField : MonoBehaviour {

	public InputField x;
	public InputField y;
	public InputField z;

	public void AssignValues(Vector3 data) {
		if (x) x.text = "" + data.x;
		if (y) y.text = "" + data.y;
		if (z) z.text = "" + data.z;
	}
		
	public Vector3 GetValue () {
		return new Vector3 (
			Utils.stringToFloat (x.text),
			Utils.stringToFloat (y.text),
			Utils.stringToFloat (z.text)
		);
	}
}
