using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetsPanel : MonoBehaviour {

	public TargetWidget targetWidgetPrefab;
	public Target targetPrefab;
	public GameObject targets;

	GameObject addButton;
	Launch launch;

	List<TargetDescription> targetDescriptions;

	void Start() {
		targetDescriptions = new List<TargetDescription> ();
		addButton = transform.GetChild (transform.childCount - 1).gameObject;
		launch = FindObjectOfType<Launch> ();
		loadFromSavedSettings ();
	}

	void loadFromSavedSettings () {
		
		if (!Store.Has(Settings.TARGETS_COUNT)) {
			return;
		}
			
		for (int i = 0; i < Store.loadInt(Settings.TARGETS_COUNT); i++) {
			TargetDescription loaded = new TargetDescription ();

			loaded.index = i;
			loaded.model = Store.loadString (Settings.TARGET_MODEL, i);
			loaded.markers = Store.loadString (Settings.TARGET_MARKERS, i);
			loaded.trajectory = Store.loadString (Settings.TARGET_TRAJECTORY, i);
			loaded.modelCenter = Store.loadVector (Settings.TARGET_MODEL_CENTER, i);
			loaded.modelRotation = Store.loadVector (Settings.TARGET_MODEL_ROTATION, i);
			loaded.modelScale = Store.loadFloat (Settings.TARGET_MODEL_SCALE, i);
			loaded.showMarkers = Store.loadBoolean (Settings.TARGET_SHOW_MARKERS, i);

			AddTarget (loaded);
		}
	}

	void ApplyTargetSettings (TargetDescription targetDescription, Target target) {
		target.LoadModel (targetDescription.model);
		target.LoadMarkers (targetDescription.markers);
		target.LoadTrajectory (targetDescription.trajectory);
		target.Translate (targetDescription.modelCenter);
		target.Rotate (targetDescription.modelRotation);
		target.Scale (targetDescription.modelScale);
		target.ShowMarkers (targetDescription.showMarkers);
	}

	public void EditTarget (TargetDescription targetDescription) {
		Target target = targetDescription.target;

		ApplyTargetSettings (targetDescription, target);

		targetDescription.targetWidget.UpdateIcon();
	}

	public void AddTarget (TargetDescription targetDescription) {
		targetDescriptions.Add (targetDescription);

		addButton.transform.SetParent(null);

		targetDescription.target = NewTarget (targetDescription);
		targetDescription.targetWidget = NewWidget (targetDescription);

		addButton.transform.SetParent(transform);
	}

	public void SaveUserSettings () {
		Store.saveInt (Settings.TARGETS_COUNT, targetDescriptions.Count);

		for (int i = 0; i < targetDescriptions.Count; i++) {
			TargetDescription desc = targetDescriptions [i]; 
			Store.saveString (Settings.TARGET_MODEL, i, desc.model);
			Store.saveString (Settings.TARGET_MARKERS, i, desc.markers);
			Store.saveString (Settings.TARGET_TRAJECTORY, i, desc.trajectory);
			Store.saveVector (Settings.TARGET_MODEL_CENTER, i, desc.modelCenter);
			Store.saveVector (Settings.TARGET_MODEL_ROTATION, i, desc.modelRotation);
			Store.saveFloat (Settings.TARGET_MODEL_SCALE, i, desc.modelScale);
			Store.saveBoolean (Settings.TARGET_SHOW_MARKERS, i, desc.showMarkers);
		}

		PlayerPrefs.Save ();
	}
		
	TargetWidget NewWidget (TargetDescription targetDescription) {
		TargetWidget targetWidget = Instantiate (targetWidgetPrefab);
		targetWidget.transform.SetParent (transform);
		targetWidget.SetTargetName (targetDescription.index);
		targetWidget.setTargetDescription (targetDescription);
		return targetWidget;
	}

	Target NewTarget (TargetDescription desc) {
		Target target = Instantiate (targetPrefab);
		target.name = TargetName(desc);
		target.transform.parent = targets.transform;

		ApplyTargetSettings (desc, target);

		launch.AddTarget (target);
		return target;
	}

	void UpdateIndex (int i) {
		TargetDescription desc = targetDescriptions [i];
		desc.index = i;
		desc.target.gameObject.name = TargetName (desc);
		desc.targetWidget.SetTargetName (i);
	}

	public void DeleteTarget (TargetDescription desc) {
		Destroy (desc.targetWidget.gameObject);
		Destroy (desc.target.gameObject);
		targetDescriptions.Remove (desc);
		launch.DeleteTarget (desc.target);
		for (int i = 0; i < targetDescriptions.Count; i++) {
			UpdateIndex (i);
		}
		SaveUserSettings ();
	}

	string TargetName(TargetDescription desc) {
		return "Target_" + desc.index;
	}
}
