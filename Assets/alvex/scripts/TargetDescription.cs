using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDescription  {

	public bool showMarkers { get; set; }

	public int index {get; set;}

	public bool isNew {get; set;}

	public TargetWidget targetWidget {get; set;}
	public Target target {get; set;}

	public string trajectory {get; set;}
	public string markers {get; set;}
	public string model {get; set;}

	public float modelScale {get;set;}

	public Vector3 modelRotation {get; set;}
	public Vector3 modelCenter {get; set;}
}
