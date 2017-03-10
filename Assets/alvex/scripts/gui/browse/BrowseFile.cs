using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class BrowseFile : MonoBehaviour {

	public RectTransform filelist;
	public InputField selected;

	public GameObject filePrefab;
	public GameObject folderPrefab;

	public Button selectButton;
	InputField input;

	void Start(){
		selectButton.onClick.AddListener (select);
	}

	void select () {
		input.text = selected.text;
		GetComponentInParent<Window> ().HideWindow ();
	}

	public void Open(string path, InputField input) {
		this.input = input;
		FillDirectory (path);
	}

	void AddFolder (string name, string path){
		GameObject parentFolder = Instantiate (folderPrefab);
		parentFolder.GetComponentInChildren<Text> ().text = name;
		parentFolder.GetComponent<Button> ().onClick.AddListener (delegate {
			FillDirectory (path);
		});
		parentFolder.transform.SetParent (filelist);
	}

	void FillDirectory (string path) {
		
		selected.text = normalizePath(path);
		Utils.DestroyChildren (filelist.gameObject);

		AddFolder ("..", path + "/..");

		DirectoryInfo dir = new DirectoryInfo (path);

		foreach (DirectoryInfo i in dir.GetDirectories ()) {
			AddFolder (i.Name, i.FullName);
		}

		foreach (FileInfo i in dir.GetFiles ()) {
			GameObject file = Instantiate(filePrefab);
			file.GetComponentInChildren<Text> ().text = i.Name;
			file.GetComponent<Button> ().onClick.AddListener (delegate { click(i.FullName); });
			file.transform.SetParent(filelist);
		}
		filelist.anchoredPosition = Vector2.zero;
		filelist.sizeDelta =  new Vector2(filelist.childCount / 7 * 150, filelist.sizeDelta.y);
	}

	void click(string path) {
		selected.text = normalizePath(path);
	}

	string normalizePath(string path) {
		return new DirectoryInfo (path).FullName;
	}
}
