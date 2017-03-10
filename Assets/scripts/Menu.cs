using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	public GameObject menuPanel;

	bool active = true;
		
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)) {
			toggle ();
		}
	}

	public void toggle () {
		setVisible (!active);		
	}
		
	public void setVisible(bool visible) {
		menuPanel.transform.localPosition = new Vector3 (0, visible ? 0 : 10000, 0);
		active = visible;
	}
}
