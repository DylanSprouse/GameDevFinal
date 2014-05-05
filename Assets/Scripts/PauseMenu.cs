using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

	public static bool paused = false;

	public GameObject PauseBox;
	public TextMesh PauseText;

	void Start() {

		PauseBox.renderer.enabled = false;

		foreach (Transform pauseChild in PauseBox.transform) {

			pauseChild.renderer.enabled = false;

		}

	}

	void Update () {

		// Turning pause on

		if (Input.GetKeyDown (KeyCode.Escape)) {

			PauseGame();
	
		}
	}

	public void PauseGame() {

		paused = !paused;

		if (paused) {

			PauseBox.renderer.enabled = true;
			
			foreach (Transform pauseChild in PauseBox.transform) {
				
				pauseChild.renderer.enabled = true;
				
			}

		} else {

				PauseBox.renderer.enabled = false;
				
				foreach (Transform pauseChild in PauseBox.transform) {
					
					pauseChild.renderer.enabled = false;
					
				}


			}



		Time.timeScale = 1.0f - Time.timeScale;

	}



	}
