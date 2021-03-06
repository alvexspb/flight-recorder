﻿using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class ScreenRecorder : MonoBehaviour {

	public int captureWidth = 800;
	public int captureHeight = 600;

	// optional game object to hide during screenshots (usually your scene canvas hud)
	public GameObject hideGameObject; 
	public bool optimizeForManyScreenshots = true;
	public enum Format { RAW, JPG, PNG, PPM };
	public Format format = Format.PPM;
	private string folder;
	private Rect rect;
	private RenderTexture renderTexture;
	private Texture2D screenShot;
	private bool captureScreenshot = false;
	private DateTime startTime;
	private string subdirectory;
	private int frameIndex;

	public void CaptureScreenshot(DateTime time, int frameIndex) {
		if (startTime != time) {
			startTime = time;
			subdirectory = null;
		}
		this.frameIndex = frameIndex;
		captureScreenshot = true;
	}

	void Update() {
		if (captureScreenshot) {
			captureScreenshot = false;
			renderCameraToTexture ();
			save ();
			cleanup ();
		}
	}

	string uniqueFilename(int width, int height) {
		
		if (folder == null) {
			folder = Application.dataPath;
			if (Application.isEditor) {
				folder += "/..";
			} 
			folder += "/../video/";
			System.IO.Directory.CreateDirectory(folder);
		}
			
		if (subdirectory == null) {
			subdirectory = folder + String.Format("{0:yyyy-MM-dd_hh-mm-ss}", startTime);
			System.IO.Directory.CreateDirectory(subdirectory);
		}

		var filename = string.Format("{0}/screen_{1}x{2}_{3}.{4}", 
			subdirectory, width, height, 
			frameIndex.ToString("D6"), 
			format.ToString().ToLower());

		return filename;
	}

	void renderCameraToTexture () {
		ensureRenderTexture ();
		// get main camera and manually render scene into rt
		Camera camera = this.GetComponent<Camera> ();
		// NOTE: added because there was no reference to camera in original script; must add this script to Camera
		camera.targetTexture = renderTexture;
		camera.Render ();
		// read pixels will read from the currently active render texture so make our offscreen 
		// render texture active and then read the pixels
		RenderTexture.active = renderTexture;
		screenShot.ReadPixels (rect, 0, 0);
		// reset active camera texture and render texture
		camera.targetTexture = null;
		RenderTexture.active = null;
	}

	void ensureRenderTexture () {
		if (!renderTexture) {
			rect = new Rect (0, 0, captureWidth, captureHeight);
			renderTexture = new RenderTexture (captureWidth, captureHeight, 24);
			screenShot = new Texture2D (captureWidth, captureHeight, TextureFormat.RGB24, false);
		}
	}

	void save () {
		// get our unique filename
		string filename = uniqueFilename ((int)rect.width, (int)rect.height);
		// pull in our file header/data bytes for the specified image format (has to be done from main thread)
		byte[] fileHeader = null;
		byte[] fileData = null;
		if (format == Format.RAW) {
			fileData = screenShot.GetRawTextureData ();
		}
		else
			if (format == Format.PNG) {
				fileData = screenShot.EncodeToPNG ();
			}
			else
				if (format == Format.JPG) {
					fileData = screenShot.EncodeToJPG ();
				}
				else {
					// ppm
					// create a file header for ppm formatted file
					string headerStr = string.Format ("P6\n{0} {1}\n255\n", rect.width, rect.height);
					fileHeader = System.Text.Encoding.ASCII.GetBytes (headerStr);
					fileData = screenShot.GetRawTextureData ();
				}
		// create new thread to save the image to file (only operation that can be done in background)
		new System.Threading.Thread (() =>  {
			// create file and write optional header with image bytes
			var f = System.IO.File.Create (filename);
			if (fileHeader != null)
				f.Write (fileHeader, 0, fileHeader.Length);
			f.Write (fileData, 0, fileData.Length);
			f.Close ();
			Debug.Log (string.Format ("Wrote screenshot {0} of size {1}", filename, fileData.Length));
		}).Start ();
	}

	void cleanup () {
		if (!optimizeForManyScreenshots) {
			Destroy (renderTexture);
			renderTexture = null;
			screenShot = null;
		}
	}
		
	public void setMatrixWidth(string value) {
		captureWidth = (int) Utils.stringToFloat (value);
	}

	public void setMatrixHeight(string value) {
		captureHeight = (int) Utils.stringToFloat (value);
	}
}
