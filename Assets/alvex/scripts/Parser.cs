using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class Parser  {

	// загрузка списка векторов
	public static List<Vector3> Load(string fileName) {
		List<Vector3> values = new List<Vector3>();
		foreach (List<float> value in loadFloats (fileName)) {
			values.Add(new Vector3(value[0], value[1], value[2]));
		}
		return values;
	}

	// Загрузка пуска
	// 
	// Порядок столбцов (15 шт.)
	// =============================
	// координаты_объекта     углы_объекта       координаты_камеры    углы_камеры           угол_зрения_верт фокус дальность_объекта
	// -----------------------------------------------------------------------------------------------------------------------------
	// Xob    Yob   Zob       курс тангаж крен   Xk  Yk  Zk           азимут УМ перекос     Uvert            F     D
	public static void LoadLaunch(
		string fileName, 
		List<Vector3> targetPositions, 
		List<Vector3> targetRotations,
		List<Vector3> trackerPositions,
		List<Vector3> trackerRotations,
		List<Vector3> trackerProperties ) {

		foreach (List<float> value in loadFloats (fileName)) {
			targetPositions.Add(new Vector3(value[0], value[1], value[2]));
			targetRotations.Add(new Vector3(value[3], value[4], value[5]));
			trackerPositions.Add(new Vector3(value[6], value[7], value[8]));
			trackerRotations.Add(new Vector3(value[9], value[10], value[11]));
			trackerProperties.Add(new Vector3(value[12], value[13], value[14]));
		}
	}

	// загружает строки(значения разделенные пробелами) из файла, 
	// приводит значения к float
	public static List<List<float>> loadFloats(string fileName) {
		List<List<float>> values = new List<List<float>> ();
		try {
			string line;
			StreamReader theReader = new StreamReader(fileName, System.Text.Encoding.Default);
			using (theReader) {						
				do {
					line = theReader.ReadLine(); 
					if (line != null) {
						string[] splitted = line.Split(' ');
						List<float> value = new List<float>();
						foreach (string s in splitted) {
							if (s != "") {
								value.Add(Utils.stringToFloat(s));	
							}
						}
						values.Add(value);
					}
				} while (line != null);
				theReader.Close();
			}
		} catch (Exception e) {
			Debug.Log(e.Message);
		}
		return values;
	}

}
