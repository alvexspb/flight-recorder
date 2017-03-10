using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetsPanel : MonoBehaviour {
	
	static string TARGETS_COUNT = "TargetsCount";
	static string TARGET_MODEL = "TargetModel";
	static string TARGET_MARKERS = "TargetMarkers";
	static string TARGET_TRAJECTORY = "TargetTrajectory";
	static string TARGET_MODEL_CENTER = "TargetModelCenter";
	static string TARGET_MODEL_ROTATION = "TargetModelRotation";
	static string TARGET_MODEL_SCALE = "TargetModelScale";
	static string X = "X";
	static string Y = "Y";
	static string Z = "Z";

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
		
		if (!PlayerPrefs.HasKey (TARGETS_COUNT)) {
			return;
		}
			
		for (int i = 0; i < PlayerPrefs.GetInt (TARGETS_COUNT); i++) {
			TargetDescription loaded = new TargetDescription ();

			loaded.index = i;
			loaded.model = PlayerPrefs.GetString (TARGET_MODEL + i);
			loaded.markers = PlayerPrefs.GetString (TARGET_MARKERS + i);
			loaded.trajectory = PlayerPrefs.GetString (TARGET_TRAJECTORY + i);
			loaded.modelCenter = LoadVectorFromPlayerPrefs (TARGET_MODEL_CENTER, i);
			loaded.modelRotation = LoadVectorFromPlayerPrefs (TARGET_MODEL_ROTATION, i);
			loaded.modelScale = PlayerPrefs.GetFloat (TARGET_MODEL_SCALE + i);

			AddTarget (loaded);
		}
	}

	Vector3 LoadVectorFromPlayerPrefs (string key, int i) {
		return new Vector3(
			PlayerPrefs.GetFloat(key + i + X),
			PlayerPrefs.GetFloat(key + i + Y),
			PlayerPrefs.GetFloat(key + i + Z)
		);
	}

	void ApplyTargetSettings (TargetDescription targetDescription, Target target) {
		target.LoadModel (targetDescription.model);
		target.LoadMarkers (targetDescription.markers);
		target.LoadTrajectory (targetDescription.trajectory);
		target.Translate (targetDescription.modelCenter);
		target.Rotate (targetDescription.modelRotation);
		target.Scale (targetDescription.modelScale);
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
		PlayerPrefs.SetInt (TARGETS_COUNT, targetDescriptions.Count);

		for (int i = 0; i < targetDescriptions.Count; i++) {
			var desc = targetDescriptions [i];
			PlayerPrefs.SetString (TARGET_MODEL + i, desc.model);
			PlayerPrefs.SetString (TARGET_MARKERS + i, desc.markers);
			PlayerPrefs.SetString (TARGET_TRAJECTORY + i, desc.trajectory);
			SaveVectorToPlayerPrefs (TARGET_MODEL_CENTER + i, desc.modelCenter);
			SaveVectorToPlayerPrefs (TARGET_MODEL_ROTATION + i, desc.modelRotation);
			PlayerPrefs.SetFloat (TARGET_MODEL_SCALE + i, desc.modelScale);
		}

		PlayerPrefs.Save ();
	}

	void SaveVectorToPlayerPrefs (string key, Vector3 modelCenter) {
		PlayerPrefs.SetFloat(key + X, modelCenter.x);
		PlayerPrefs.SetFloat(key + Y, modelCenter.y);
		PlayerPrefs.SetFloat(key + Z, modelCenter.z);
	}

	TargetWidget NewWidget (TargetDescription targetDescription) {
		TargetWidget targetWidget = Instantiate (targetWidgetPrefab);
		targetWidget.transform.SetParent (transform);
		targetWidget.SetTargetName (targetDescription.index);
		targetWidget.setTargetDescription (targetDescription);
		return targetWidget;
	}

	Target NewTarget (TargetDescription targetDescription) {
		Target target = Instantiate (targetPrefab);
		target.name = "Target" + targetDescription.index;
		target.transform.parent = targets.transform;

		ApplyTargetSettings (targetDescription, target);

		launch.AddTarget (target);
		return target;
	}
}
