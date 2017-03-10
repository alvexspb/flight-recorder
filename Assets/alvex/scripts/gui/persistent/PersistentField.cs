using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersistentField : MonoBehaviour {

	void Start () {
		InputField inputField = GetComponentInChildren<InputField> ();
		inputField.onValueChanged.AddListener (persist);
		if (PlayerPrefs.HasKey (gameObject.name)) {
			inputField.text = PlayerPrefs.GetString (gameObject.name);
		}
		persist (inputField.text);
	}
		
	void persist(string value) {
		PlayerPrefs.SetString (gameObject.name, value);
		PlayerPrefs.Save ();
	}
}
