using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	public float camSpeed = 1f;

	private bool inputUp, inputDown, inputLeft, inputRight;

	void Start () {
	
	}

	void Update () {

		inputUp = Input.GetKey (KeyCode.W) == true? true : false;
		inputDown = Input.GetKey (KeyCode.S) == true? true : false;
		inputLeft = Input.GetKey (KeyCode.A) == true? true : false;
		inputRight = Input.GetKey (KeyCode.D) == true? true : false;

		if (inputUp && Camera.main.transform.position.z < 20f) {

			Camera.main.transform.position += new Vector3 (0f, 0f, camSpeed);

		}
		if (inputDown && Camera.main.transform.position.z > -25f) {
			
			Camera.main.transform.position += new Vector3 (0f, 0f, -camSpeed);
			
		}
		if (inputLeft && Camera.main.transform.position.x > -25f) {
			
			Camera.main.transform.position += new Vector3 (-camSpeed, 0f, 0f);
			
		}
		if (inputRight && Camera.main.transform.position.x < 25f) {
			
			Camera.main.transform.position += new Vector3 (camSpeed, 0f, 0f);
			
		}




		if (Input.mousePosition.x > Screen.width && Camera.main.transform.position.x < 25f) {

			Camera.main.transform.position += new Vector3 (camSpeed, 0f, 0f);

		}

		if (Input.mousePosition.x < 0 && Camera.main.transform.position.x > -25f) {
			
			Camera.main.transform.position += new Vector3 (-camSpeed, 0f, 0f);
			
		}

		if (Input.mousePosition.y < 0 && Camera.main.transform.position.z > -25f) {
			
			Camera.main.transform.position += new Vector3 (0f, 0f, -camSpeed);

			
		}

		if (Input.mousePosition.y > Screen.height && Camera.main.transform.position.z < 20f) {
			
			Camera.main.transform.position += new Vector3 (0f, 0f, camSpeed);
			
			
		}



	
	}
}
