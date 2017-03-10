using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Put this script on a Camera
public class Gizmos : MonoBehaviour {

	static Material material;

	static Color[] colors = new Color[] {
		new Color(1f, 0, 0), 
		new Color(0, 1f, 0), 
		new Color(0, 0, 1f)
	};

	public float zeroGizmosLength = 10;
	public float objectGizmosLength = 5;

	public GameObject parent;

	Vector3 rotate(Vector3 point, Quaternion rotate, Vector3 around) {
		return rotate * (point - around) + around;
	}

	void gizmo (Vector3 startPoint, Quaternion rotation, float arrowLength, Color color) {
		
		Vector3 colorVector = new Vector3 (color.r, color.g, color.b);
		Vector3 endPoint = startPoint + colorVector * arrowLength;
		DrawLine (startPoint, endPoint, color, rotation);

		Vector3 arrowConeBasePoint = endPoint - colorVector;
		List<Vector3> circle = circlePoints (arrowConeBasePoint, colorVector);
		for(int i = 0; i < circle.Count; i++) {
			DrawTriangle (endPoint, circle[i], circle[i == circle.Count - 1 ? 0 : i + 1], color, rotation, startPoint);	
		}

	}

	List<Vector3> circlePoints(Vector3 center, Vector3 axis) {
		List<Vector3> points = new List<Vector3> ();
		for (int i = 0; i < 360; i += 30) {
			float rad = i * Mathf.Deg2Rad;
			float sin = Mathf.Sin (rad) * 0.2f;
			float cos = Mathf.Cos (rad) * 0.2f;
			points.Add (center + new Vector3 (
				1 == axis.x ? 0 : sin,
				1 == axis.y ? 0 : 1 == axis.x ? sin : cos,
				1 == axis.z ? 0 : cos
			));
		}
		return points;
	}



	void gizmos (Vector3 from, Quaternion rot, float size) {
		foreach (Color c in colors) {
			gizmo (from, rot, size, c);
		}
	}

	void vertex (Vector3 vertex){
		GL.Vertex3 (vertex.x, vertex.y, vertex.z);
	}

	void DrawLine (Vector3 fromPoint, Vector3 toPoint, Color color, Quaternion rotation) {
		toPoint = rotate (toPoint, rotation, fromPoint);
		GL.Begin (GL.LINES);
		GL.Color (color);
		vertex (fromPoint);
		vertex (toPoint);
		GL.End ();
	}

	void DrawTriangle (Vector3 a, Vector3 b, Vector3 c, Color color, Quaternion rot, Vector3 around) {
		GL.Begin (GL.TRIANGLES);
		GL.Color (color);
		vertex(rotate (a, rot, around));
		vertex(rotate (b, rot, around));
		vertex(rotate (c, rot, around));
		GL.End ();
	}

	void draw() {
		ensureMaterial ();

		if (zeroGizmosLength > 0) {
			gizmos(Vector3.zero, Quaternion.Euler(Vector3.zero), zeroGizmosLength);
		}

		if(parent == null || objectGizmosLength == 0) {
			return;
		}

		foreach (Target target in parent.GetComponentsInChildren<Target> ()) {
			if (target.model.transform.childCount == 0) {
				continue;
			}

			Transform t = target.model.GetChild (0).transform;
			gizmos (t.position, t.localRotation, objectGizmosLength);
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
		draw();
	}

	// To show the lines in the editor
	void OnDrawGizmos() {
		draw();
	}
}