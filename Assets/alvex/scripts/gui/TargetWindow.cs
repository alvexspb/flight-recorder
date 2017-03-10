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

	TargetDescription targetDescription;

	void Start() {
		saveButton.onClick.AddListener (save);
	}

	public void NewTarget() {
		targetDescription = new TargetDescription ();
		targetDescription.isNew = true;
		modelField.text = "";
		markersField.text = "";
		trajectoryFiled.text = "";
		modelScaleField.text = "1";
		modelCenter.AssignValues (Vector3.zero);
		modelRotation.AssignValues (Vector3.zero);
		targetDescription.index = FindObjectOfType<Launch> ().TargetCount ();
	}

	public void EditTarget(TargetDescription targetDescription) {
		this.targetDescription = targetDescription;
		modelField.text = targetDescription.model;
		markersField.text = targetDescription.markers;
		trajectoryFiled.text = targetDescription.trajectory;
		modelScaleField.text = "" + targetDescription.modelScale;
		modelCenter.AssignValues(targetDescription.modelCenter);
		modelRotation.AssignValues (targetDescription.modelRotation);
	}

	void save () {

		TargetsPanel targetsPanel = FindObjectOfType<TargetsPanel> ();
		
		targetDescription.model = modelField.text;
		targetDescription.markers = markersField.text;
		targetDescription.trajectory = trajectoryFiled.text;
		targetDescription.modelRotation = modelRotation.GetValue ();
		targetDescription.modelCenter = modelCenter.GetValue ();
		targetDescription.modelScale = Utils.stringToFloat (modelScaleField.text);

		if (targetDescription.isNew) {
			targetsPanel.AddTarget (targetDescription);
		} else {
			targetsPanel.EditTarget (targetDescription);
		}

		targetDescription.isNew = false;
		targetsPanel.SaveUserSettings ();
		GetComponentInParent<Window> ().HideWindow ();
	}
}
