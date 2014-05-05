using UnityEngine;
using System.Collections;

public class Restart : MonoBehaviour {


	// Update is called once per frame
	void Update () {
	
		if (Input.GetKey (KeyCode.Space) && MouseController.Instance.gameIsOver) {

			Application.LoadLevel (0);
		}

	}
}
