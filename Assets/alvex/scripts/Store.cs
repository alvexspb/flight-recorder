using System;
using UnityEngine;

public class Store {
	public static bool Has (Settings setting) {
		return PlayerPrefs.HasKey(setting.ToString());
	}

	public static void saveString (Settings setting, int index, string value) {
		PlayerPrefs.SetString(setting + "_" + index, value);
	}

	public static void saveFloat (Settings setting, int index, float value) {
		PlayerPrefs.SetFloat(setting + "_" + index, value);
	}

	public static void saveBoolean (Settings setting, int index, bool value) {
		PlayerPrefs.SetInt(setting + "_" + index, value ? 1 : 0);
	}

	public static void saveInt (Settings setting, int value) {
		PlayerPrefs.SetInt(setting.ToString(), value);
	}

	public static int loadInt (Settings setting) {
		return PlayerPrefs.GetInt (setting.ToString());
	}


	public static bool loadBoolean (Settings key, int index) {
		return 1 == PlayerPrefs.GetInt (key + "_" + index);
	}

	public static string loadString(Settings key, int index) {
		return PlayerPrefs.GetString (key + "_" + index);
	}

	public static float loadFloat(Settings key, int index) {
		return PlayerPrefs.GetFloat (key + "_" + index);
	}


	public static void saveVector (Settings setting, int index, Vector3 vector) {
		saveVectorComponent(setting, Settings.X, index, vector.x);
		saveVectorComponent(setting, Settings.Y, index, vector.y);
		saveVectorComponent(setting, Settings.Z, index, vector.z);
	}

	public static Vector3 loadVector (Settings key, int i) {
		return new Vector3 (
			loadVectorCoponent (key, Settings.X, i),
			loadVectorCoponent (key, Settings.Y, i),
			loadVectorCoponent (key, Settings.Z, i)
		);
	}

	static float loadVectorCoponent (Settings setting, Settings component, int index) {
		return PlayerPrefs.GetFloat (setting + "_" + index + "_" + component);
	}

	static void saveVectorComponent (Settings setting, Settings component, int index, float value) {
		PlayerPrefs.SetFloat(setting + "_" + index + "_" + component, value);
	}
}

public enum Settings {
	TARGETS_COUNT,
	TARGET_MODEL,
	TARGET_MARKERS,
	TARGET_TRAJECTORY,
	TARGET_MODEL_CENTER,
	TARGET_MODEL_ROTATION,
	TARGET_MODEL_SCALE,
	TARGET_SHOW_MARKERS,
	X, Y, Z
}
