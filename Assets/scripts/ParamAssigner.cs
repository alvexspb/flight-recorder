using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParamAssigner : MonoBehaviour {

	public Transform objectForPositionAssign;
	public VectorElement vectorElement;
	public bool negate;
	public AssignParameter assignParameter;
	public bool onChange;

	private InputField field;

	void Start() {
		InputField field = GetComponent<InputField> ();
		if (onChange)
			field.onValueChanged.AddListener (assign);
		else
			field.onEndEdit.AddListener (assign);
		assign (field.text);
	}

	Vector3 Apply (Vector3 v, float value) {
		if (vectorElement == VectorElement.X)
			v.x = negate ? -value : value;
		if (vectorElement == VectorElement.Y)
			v.y = negate ? -value : value;
		if (vectorElement == VectorElement.Z)
			v.z = negate ? -value : value;
		return v;
	}

	void assign (string text) {
		float value = Utils.stringToFloat (text);
		if (assignParameter == AssignParameter.POSITION) {
			Vector3 pos = objectForPositionAssign.localPosition;		
			objectForPositionAssign.localPosition = Apply (pos, value);
		}

		if (assignParameter == AssignParameter.ROTATION) {
			Vector3 rot = objectForPositionAssign.localEulerAngles;
			objectForPositionAssign.localEulerAngles = Apply (rot, value);
		}
	}
		

}

public enum AssignParameter{
	POSITION, ROTATION
}