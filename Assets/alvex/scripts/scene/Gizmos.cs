using UnityEngine;
using System.Collections;

// Put this script on a Camera
public class Gizmos : MonoBehaviour {

	static Material material;

	static Color[] colors = new Color[] {
		new Color(1f, 0, 0), 
		new Color(0, 1f, 0), 
		new Color(0, 0, 1f)
	};

	public int size = 5;
	public GameObject parent;


	Vector3 rotate(Vector3 point, Quaternion rotate, Vector3 around) {
		return rotate * (point - around) + around;
	}

	void DrawArrow (Vector3 from, Quaternion rot, int size, Color c) {
		Vector3 colorVector = new Vector3 (c.r, c.g, c.b);
		Vector3 modifier = Vector3.one - colorVector;
	
		Vector3 to = from + colorVector * size;
		Vector3 to1 = to - colorVector;

		DrawLine (from, to, c, rot);
		DrawTriangle (to, to1+modifier*0.15f, to1-modifier*0.15f, c, rot, from);

	}


	void singleGizmos (Vector3 from, Quaternion rot, int size) {
		foreach (Color c in colors) {
			DrawArrow (from, rot, size, c);
		}
	}

	void DrawLine (Vector3 from, Vector3 to, Color c, Quaternion rot) {
		to = rotate (to, rot, from);
		GL.Begin (GL.LINES);
		GL.Color (c);
		GL.Vertex3 (from.x, from.y, from.z);
		GL.Vertex3 (to.x, to.y, to.z);
		GL.End ();
	}

	void DrawTriangle (Vector3 a, Vector3 b, Vector3 c, Color color, Quaternion rot, Vector3 around) {
		Vector3 a1 = rotate (a, rot, around);
		Vector3 b1 = rotate (b, rot, around);
		Vector3 c1 = rotate (c, rot, around);
		GL.Begin (GL.TRIANGLES);
		GL.Color (color);
		GL.Vertex3 (a1.x, a1.y, a1.z);
		GL.Vertex3 (b1.x, b1.y, b1.z);
		GL.Vertex3 (c1.x, c1.y, c1.z);
		GL.End ();
	}

	void DrawAllGizmos() {
		ensureMaterial ();

		singleGizmos(Vector3.zero, Quaternion.Euler(Vector3.zero), 10);

		if(parent) {
			foreach (Target target in parent.GetComponentsInChildren<Target> ()) {
				Transform t = target.model.GetChild (0).transform;
				singleGizmos (t.position, t.localRotation, size);
			}
		}
	}

	static void ensureMaterial () {
		if (material == null) {
			material = new Material (Shader.Find ("GUI/Text Shader"));
		}
		material.SetPass (0);
	}

	// To show the lines in the game window whne it is running
	void OnPostRender() {
		DrawAllGizmos();
	}

	// To show the lines in the editor
	void OnDrawGizmos() {
		DrawAllGizmos();
	}
}