using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModelScaler : MonoBehaviour {

	public GameObject targetModel;

	void Start () {
		InputField field = GetComponent<InputField> ();
		field.onValueChanged.AddListener (assign);
		assign (field.text);
	}

	void assign (string value) {
		float v = Utils.stringToFloat (value);
		targetModel.transform.localScale = new Vector3 (v, v, v);
	}
}
