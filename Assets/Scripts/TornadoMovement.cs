using UnityEngine;
using System.Collections;

public class TornadoMovement : MonoBehaviour {

	public float spinSpeed = 150f;
	public float movementSpeed = 10f;
	public Vector3 tornadoEnd;
	public Vector3 originalPos;
	public bool timerEnabled = false;
	public bool tornadoEnabled = false;
	public bool tornadoActivate = false;

	void Start() {

		StartCoroutine (TornadoDestination ());
		StartCoroutine (TornadoEnd ());

	}

	void Update () {

		if (!timerEnabled) {
			
			StartCoroutine(TornadoCounter());
			
		}
	
		transform.Rotate (Vector3.up * Time.deltaTime * spinSpeed);
		
		transform.position = Vector3.Lerp (transform.position, 
		                                            tornadoEnd, 
		                                            Time.deltaTime * movementSpeed);



		// Tornado effect
		
		if (MouseController.Instance.builtTileList.Count > 0) {

			for (int i = 0; i < MouseController.Instance.builtTileList.Count; i++) {

				if (MouseController.Instance.builtTileList[i] != null && Vector3.Distance (MouseController.Instance.builtTileList[i].transform.position, transform.position) < 5f) {

					foreach (Transform child in MouseController.Instance.builtTileList[i].transform) {

						child.transform.localScale -= new Vector3 (MouseController.Instance.tornadoDecay, MouseController.Instance.tornadoDecay, MouseController.Instance.tornadoDecay);

					}

				}

			}

		}

		GameObject[] mountainTiles;
		mountainTiles = GameObject.FindGameObjectsWithTag("Mountain");

		for (int x = 0; x < mountainTiles.Length; x++) {


			if (Vector3.Distance (mountainTiles[x].transform.position, transform.position) < 3f) {


				if (transform.localScale.y < 0.001f) {
					
					Destroy (this.gameObject);

					
				} else {
					
					transform.localScale -= new Vector3 (0.01f, 0.01f, 0.01f);
					
				}

			}



		}




	}

	private IEnumerator TornadoCounter () {
		timerEnabled = true;
		StartCoroutine(TornadoDestination ());
		yield return new WaitForSeconds (8f);
		timerEnabled = false;
		
	}
	
	private IEnumerator TornadoDestination() {
		yield return new WaitForSeconds(5f);
		int i = Random.Range (0, MouseController.Instance.greenTileList.Count);
		tornadoEnd = MouseController.Instance.greenTileList[i].transform.position;
		
		
	}
	
	public IEnumerator TornadoEnd() {

		yield return new WaitForSeconds (30f);

		
	}

}
