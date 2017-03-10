using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetWidget : MonoBehaviour {
	
	TargetDescription targetDescription;

	public int iconSize;

	public PhotoRoom photoRoom;

	void Start() {
		GetComponent<Button> ().onClick.AddListener (edit);
		GetComponentInChildren<Toggle> ().onValueChanged.AddListener (toggleModel);
	}

	void toggleModel (bool show) {
		targetDescription.target.gameObject.SetActive (show);
	}

	void edit() {
		TargetWindow targetWindow = FindObjectOfType<TargetWindow> ();
		targetWindow.GetComponentInParent<Window> ().ShowWindow ();
		GetComponentInParent<Window> ().HideWindow ();
		targetWindow.EditTarget (targetDescription);
	}

	public void UpdateIcon () {
		GetComponentInChildren<RawImage> ().texture = photoRoom.TakePhoto (targetDescription.target.gameObject, 85);
	}

	public void setTargetDescription (TargetDescription targetDescription) {
		this.targetDescription = targetDescription;
		UpdateIcon ();
	}
	
	public void SetTargetName (int index) {
		gameObject.name = "TargetWidget" + index;
		GetComponentInChildren<Text> ().text = "Цель " + (index+1);
	}
}
