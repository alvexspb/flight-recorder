using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersitentCheckbox : MonoBehaviour {

	void Start () {
		Toggle toggle = GetComponent<Toggle> ();
		toggle.onValueChanged.AddListener (persist);
		if (PlayerPrefs.HasKey (gameObject.name)) {
			toggle.isOn = bool.Parse(PlayerPrefs.GetString (gameObject.name));
		}
		persist (toggle.isOn);
	}

	void persist(bool isOn) {
		PlayerPrefs.SetString (gameObject.name, isOn.ToString());
		PlayerPrefs.Save ();
	}
}
