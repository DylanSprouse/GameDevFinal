using UnityEngine;
using System.Collections;

public class RuinObject : MonoBehaviour {

	private bool decayRuin = false;

	void Start() {

		StartCoroutine (RuinRemove ());

	}

	void Update () {
	

		if (decayRuin) {

			transform.position -= new Vector3 (0, 0.0025f, 0);
		}

		for (int i = 0; i < MouseController.Instance.builtTileList.Count; i++) {

			if (Vector3.Distance (transform.position, MouseController.Instance.builtTileList[i].transform.position) < 1f) {

				Destroy (this.gameObject);

			}


		}

	}

	private IEnumerator RuinRemove() {

		yield return new WaitForSeconds(7f);
		decayRuin = true;
		yield return new WaitForSeconds(5f);
		Destroy (this.gameObject);
	}
}
