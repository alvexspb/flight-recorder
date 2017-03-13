using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Launch : MonoBehaviour {

	List<Target> targets;
	TrackingCamera trackingCamera;

	public InputField frameNumberField;


	public float fps = 10;
	public bool linearInterpolation = true;
	public bool recording = false;

	float startTime;
	float lerpToTime; 
	int leftFrameIndex;
	//int rightFrameIndex;
	int lastCapturedFrame = -1;
	DateTime startDate;
	string focusText;
	string distanceText;

	void Start() {
		trackingCamera = FindObjectOfType<TrackingCamera> ();
	}

	public void AddTarget(Target target) {
		if (null == targets) {
			targets = new List<Target> ();
		}
		targets.Add (target);
	}

	public void DeleteTarget(Target target) {
		targets.Remove(target);
	}

	public void StartLaunch () {
		startTime = Time.time;
		startDate = DateTime.Now;
	}

	public void StopLaunch () {
		Time.timeScale = 1;
		startTime = 0;
		leftFrameIndex = 0;
		lastCapturedFrame = -1;
		lerpToTime = 0;
		frameNumberField.GetComponent<InputField> ().text = "0";
		ApplyTargetsState ();
		ApplyCameraState();
	}


	public void PauseLaunch() {
		Time.timeScale = Time.timeScale == 0 ? 1 : 0;
	}

	void Update () {
		if (startTime == 0)  
			return;

		CalcTime ();

		try {
			ApplyTargetsState ();
			ApplyCameraState();
			SaveFrame();
		} catch {
			StopLaunch ();
		}
	}

	void CalcTime () {
		float t = Time.time - startTime;

		leftFrameIndex = Mathf.FloorToInt (t * fps);

		if (Time.timeScale > 0)
			frameNumberField.GetComponent<InputField> ().text = "" + leftFrameIndex;

		lerpToTime = linearInterpolation ? t * fps - (float)leftFrameIndex : 0;
	}

	public void ApplyCameraState() {
		trackingCamera.ApplyState (leftFrameIndex, lerpToTime);
	}

	void ApplyTargetsState () {
		foreach(Target t in targets) {
			t.ApplyState(leftFrameIndex, lerpToTime);
		}
	}

	public int TargetCount () {
		return targets.Count;
	}

	public void EnableInterpolation (bool enable) {
		linearInterpolation = enable;
	}

	public void setFPS(string value) {
		fps = Utils.stringToFloat (value);
	}

	public void EnableRecording (bool enable) {
		recording = enable;
	}

	void SaveFrame () {
		if (recording && leftFrameIndex > lastCapturedFrame) {
			lastCapturedFrame = leftFrameIndex;
			Debug.Log ("saving frame " + leftFrameIndex);
			StartCoroutine (screenshot());
		}
	}

	public void setFrameNumber(string value) {
		if (startTime != 0) {
			startTime = Time.time - leftFrameIndex * fps;
		}

		leftFrameIndex = (int) Utils.stringToFloat (value);
			
		ApplyTargetsState ();
		ApplyCameraState();
	}

	IEnumerator screenshot() {
		yield return new WaitForEndOfFrame();
		trackingCamera.GetComponentInChildren<ScreenRecorder> ().CaptureScreenshot (startDate, leftFrameIndex);
	}
}
