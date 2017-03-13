using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetWindow : MonoBehaviour {

	public InputField modelField;
	public InputField markersField;
	public InputField trajectoryFiled;
	public InputField modelScaleField;
	public VectorField modelCenter;
	public VectorField modelRotation;
	public Button saveButton;

	Toggle showMarkers;
	TargetDescription desc;

	void Start() {
		saveButton.onClick.AddListener (save);
		showMarkers = GetComponentInChildren<Toggle> ();
	}

	public void NewTarget() {
		desc = new TargetDescription ();
		desc.isNew = true;
		modelField.text = "";
		markersField.text = "";
		trajectoryFiled.text = "";
		modelScaleField.text = "1";
		modelCenter.AssignValues (Vector3.zero);
		modelRotation.AssignValues (Vector3.zero);
		desc.index = FindObjectOfType<Launch> ().TargetCount ();
		showMarkers.isOn = false;
	}

	public void EditTarget(TargetDescription targetDescription) {
		this.desc = targetDescription;
		modelField.text = targetDescription.model;
		markersField.text = targetDescription.markers;
		trajectoryFiled.text = targetDescription.trajectory;
		modelScaleField.text = "" + targetDescription.modelScale;
		modelCenter.AssignValues(-targetDescription.modelCenter);
		modelRotation.AssignValues (targetDescription.modelRotation);
		showMarkers.isOn = targetDescription.showMarkers;
	}

	void save () {

		TargetsPanel targetsPanel = FindObjectOfType<TargetsPanel> ();
		
		desc.model = modelField.text;
		desc.markers = markersField.text;
		desc.trajectory = trajectoryFiled.text;
		desc.modelRotation = modelRotation.GetValue ();
		desc.modelCenter = -modelCenter.GetValue ();
		desc.modelScale = Utils.stringToFloat (modelScaleField.text);
		desc.showMarkers = showMarkers.isOn;

		if (desc.isNew) {
			targetsPanel.AddTarget (desc);
		} else {
			targetsPanel.EditTarget (desc);
		}

		desc.isNew = false;
		targetsPanel.SaveUserSettings ();
		GetComponentInParent<Window> ().HideWindow ();
	}
}
