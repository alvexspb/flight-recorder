using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ConfirmDelete : MonoBehaviour {

	public delegate void Callback();

	public Button ok;
	public Button cancel;
	public Callback callback;

	void Start () {
		ok.onClick.AddListener (onOkPressed);
		cancel.onClick.AddListener (GetComponentInParent<Window> ().HideWindow);
	}

	public void Show (Window parent, Callback callback) {
		this.callback = callback;
		parent.CloseWindow ();
		GetComponentInParent<Window> ().parent = parent;
		GetComponentInParent<Window> ().ShowWindow ();
	}

	void onOkPressed () {
		callback ();
		GetComponentInParent<Window> ().HideWindow ();
	}
}

