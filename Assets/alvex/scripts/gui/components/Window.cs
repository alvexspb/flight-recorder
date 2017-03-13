using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour {

	static Window lastOpenedWindow;
	Vector2 hiddenPosition;

	public Window parent;

	void Start () {
		hiddenPosition = GetComponent<RectTransform> ().anchoredPosition;
	}

	void Update () {
		if(null == parent && Input.GetKeyDown(KeyCode.Escape)) { 
			ToggleLastOpenedWindow ();
		}
	}

	public void ShowWindow() {
		GetComponent<RectTransform> ().anchoredPosition = Vector2.zero;
		lastOpenedWindow = this;
	}

	public void CloseWindow() {
		GetComponent<RectTransform> ().anchoredPosition = hiddenPosition;
	}

	public void HideWindow() {
		CloseWindow();
		if (parent) {
			parent.ShowWindow ();
		}
	}

	public void ToggleLastOpenedWindow () {
		if (null == lastOpenedWindow) {
			foreach (Window w in FindObjectsOfType<Window>()) {
				if (w.parent == null) {
					lastOpenedWindow = w;
				}
			}
		}
			
		lastOpenedWindow.Toggle ();
	}

	public void Toggle() {
		if (GetComponent<RectTransform> ().anchoredPosition == hiddenPosition) {
			ShowWindow ();
		} else {
			HideWindow ();
		}
	}
}
