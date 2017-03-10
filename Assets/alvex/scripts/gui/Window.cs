using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour {

	Vector2 hiddenPosition;
	public Window parent;

	void Start () {
		hiddenPosition = GetComponent<RectTransform> ().anchoredPosition;
	}

	public void ShowWindow() {
		GetComponent<RectTransform> ().anchoredPosition = Vector2.zero;
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

	public void Toggle() {
		if (GetComponent<RectTransform> ().anchoredPosition == hiddenPosition) {
			ShowWindow ();
		} else {
			HideWindow ();
		}
	}
}
