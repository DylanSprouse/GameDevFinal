using UnityEngine;
using System.Collections;

public class TornadoGenerator : MonoBehaviour {
	public GameObject tornadoModel;

	public float spinSpeed = 150f;
	public float movementSpeed = 10f;
	public Vector3 tornadoEnd;
	public Vector3 originalPos;
	public bool timerEnabled = false;
	public bool tornadoEnabled = false;
	
	void Start() {

		tornadoModel.renderer.enabled = false;
		StartCoroutine (TornadoDestination ());

	}
	// Update is called once per frame
	void Update () {
	
			if (tornadoEnabled) {
			tornadoModel.transform.Rotate (Vector3.up * Time.deltaTime * spinSpeed);

			tornadoModel.transform.position = Vector3.Lerp (tornadoModel.transform.position, 
		                                                tornadoEnd, 
		                                                Time.deltaTime * movementSpeed); 

			if (!timerEnabled) {

				StartCoroutine (TornadoCounter());

			}
		}

	}
	

	private IEnumerator TornadoCounter () {
		timerEnabled = true;
		yield return new WaitForSeconds (8f);
		StartCoroutine(TornadoDestination ());
		timerEnabled = false;

	}

	private IEnumerator TornadoDestination() {
		yield return new WaitForSeconds(5f);
		int i = Random.Range (0, GetComponent<MouseController>().greenTileList.Count);
		tornadoEnd = GetComponent<MouseController>().greenTileList[i].transform.position;


	}

}
