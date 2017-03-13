using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Browse : MonoBehaviour {

	public string buttonText;

	protected InputField input;

	public string extension;

	void Start () {
		InitButton ();
		InitInput ();

		input.onValueChanged.AddListener (doLoad);

		if (input.text != null && input.text.Length > 0) {
			doLoad (input.text);
		}
	}

	void doLoad (string path) {
//		Debug.Log ("loading: " + input.text);
		StartCoroutine (load (path));
	}


	protected virtual IEnumerator load (string path) {
		yield return new WaitForEndOfFrame();
	}

	void InitButton () {
		Button button = GetComponentInChildren<Button> ();
		button.gameObject.name = "Button-" + gameObject.name;
		button.gameObject.GetComponentInChildren<Text> ().text = buttonText;
		button.onClick.AddListener (browse);
	}

	void InitInput () 	{
		input = GetComponentInChildren<InputField> ();
		input.gameObject.name = "InputField-" + gameObject.name;
		//input.placeholder.GetComponent<Text>().text = defaultValue;
	}

		
	void browse () {
		string path = Application.dataPath;
		//#if UNITY_EDITOR
		path += "/..";
		//#endif
		path += "/../data/";

		BrowseFile browseFile = FindObjectOfType<BrowseFile> ();
		Window browseWindow = browseFile.GetComponentInParent<Window> ();
		browseWindow.ShowWindow ();
		browseFile.Open (path, input, extension);

		Window parentWindow = GetComponentInParent<Window> ();
		browseWindow.parent = parentWindow;
		parentWindow.CloseWindow ();
	
	}
}
