using UnityEngine;
using System.Collections;

public class CostObject : MonoBehaviour {

	// Use this for initialization
	void Start () {

		StartCoroutine (DestroySelf ());
	}
	
	// Update is called once per frame
	void Update () {
	
		transform.position += new Vector3 (0f, 0.005f, 0f);
	}

	private IEnumerator DestroySelf() {

		yield return new WaitForSeconds(3f);
		Destroy (this.gameObject);


	}
}
