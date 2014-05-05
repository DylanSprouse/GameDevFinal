using UnityEngine;
using System.Collections;

public class TornadoGenerator : MonoBehaviour {

	public float spinSpeed = 150f;
	public float movementSpeed = 10f;
	public Vector3 tornadoEnd;
	public Vector3 originalPos;
	public bool timerEnabled = false;
	public bool tornadoEnabled = false;
	public bool tornadoActivate = false;
	

	// Update is called once per frame
	void Update () {
	
			if (tornadoEnabled) {



			GameObject instance = Instantiate (Resources.Load ("tornado_1", typeof(GameObject)), new Vector3 (Random.Range (-30, 30), 0f, Random.Range (-30, 30)),
			                                   Quaternion.identity) as GameObject;

			tornadoEnabled = false;
		
		}

	}
}
