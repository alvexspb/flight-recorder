using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrowseTexture : Browse {

	public enum SkyboxSide {
		FRONT, BACK, LEFT, RIGHT, UP, DOWN
	}

	public Camera targetCamera;
	public SkyboxSide side;

	protected override IEnumerator load (string path) {

		yield return new WaitForSecondsRealtime (0.1f);

		WWW www = new WWW ("file:///" + path);

		yield return www;

		targetCamera.GetComponent<Skybox> ().material.SetTexture ("_" + CamelCase(side) + "Tex", www.texture);
	}

	string CamelCase(SkyboxSide side) {
		string str = side.ToString ();
		return str [0].ToString ().ToUpper () + str.Substring (1).ToLower ();
	}
}
