using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HasVectorList : MonoBehaviour {
	
	private List<Vector3> data;
	public int count = 0;

	public VectorField vectorField;

	public virtual void SetList(List<Vector3> data) {
		this.data = data;
		count = data.Count;
		if (vectorField && data.Count > 0)
			vectorField.AssignValues (data [0]);
	}
		
	public List<Vector3> GetData {
		get {
			return data;
		}
		set {
			data = value;
		}
	}
		
}

