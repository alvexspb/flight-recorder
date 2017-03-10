using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoRoom : MonoBehaviour {
	static Color TRANSPARENT = new Color (1, 1, 1, 0);

	public Camera cam;
	public Transform place;
	public GameObject enviroment;
	
	public Texture2D TakePhoto(GameObject obj, int size) {
		
		obj.transform.position = place.position;

		Rect rect = new Rect (0, 0, size, size);
		RenderTexture rt = new RenderTexture (size, size, 32);
		Texture2D icon = new Texture2D (size, size, TextureFormat.RGBA32, false);

		cam.targetTexture = rt;
		cam.Render ();

		RenderTexture.active = rt;
		icon.ReadPixels (rect, 0, 0);


		for(int x = 0; x < size; x++) {
			for(int y = 0; y < size; y++) {
				if (icon.GetPixel (x, y) == Color.white) {
					icon.SetPixel (x, y, TRANSPARENT);
				}
			}
		}
			
		icon.Apply ();

		var f = System.IO.File.Create ("./icon.png");
		var data = icon.EncodeToPNG ();
		f.Write (data, 0, data.Length);
		f.Close ();

		cam.targetTexture = null;
		RenderTexture.active = null;

		obj.transform.position = Vector3.zero;

		return icon;
	}
}
