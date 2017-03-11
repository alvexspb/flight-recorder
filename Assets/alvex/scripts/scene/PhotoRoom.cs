using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoRoom : MonoBehaviour {
	
	static Color TRANSPARENT = new Color (1, 1, 1, 0);

	public Camera cam;
	public Transform place;
	public GameObject enviroment;

	Vector3 prevPosition;
	bool prevActiveState;
	
	public Texture2D TakePhoto(GameObject obj, int size) {

		prevPosition = obj.transform.position;
		prevActiveState = obj.activeInHierarchy;

		obj.transform.position = place.position;
		obj.SetActive (true);

		Rect rect = new Rect (0, 0, size, size);
		//RenderTexture rt = new RenderTexture (size, size, 32);
		Texture2D icon = new Texture2D (size, size, TextureFormat.RGBA32, false);

		cam.targetTexture = new RenderTexture (size, size, 32);
		cam.Render ();

		RenderTexture.active = cam.targetTexture;
		icon.ReadPixels (rect, 0, 0);

		WhiteToTransparent (icon);
			
		icon.Apply ();

		cam.targetTexture = null;
		RenderTexture.active = null;

		obj.transform.position = prevPosition;
		obj.SetActive (prevActiveState);

		return icon;
	}

	static void WhiteToTransparent (Texture2D icon) {
		for (int x = 0; x < icon.width; x++) {
			for (int y = 0; y < icon.height; y++) {
				if (icon.GetPixel (x, y) == Color.white) {
					icon.SetPixel (x, y, TRANSPARENT);
				}
			}
		}
	}
}
