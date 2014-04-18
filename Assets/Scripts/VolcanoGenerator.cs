using UnityEngine;
using System.Collections;

public class VolcanoGenerator : MonoBehaviour {
	
	public bool volcanoEnabled = false;

	void Update() {

		if (volcanoEnabled) {
		
			SpawnVolcano();

		}

	}

	void SpawnVolcano() {
		volcanoEnabled = false;

		GameObject[] mountainTiles;
		mountainTiles = GameObject.FindGameObjectsWithTag("Mountain");

		if (mountainTiles.Length > 0) {
			int randomMountain = Random.Range (0, mountainTiles.Length);
			GameObject currentMountain = mountainTiles[randomMountain];
			Vector3 mountPos = new Vector3(currentMountain.transform.position.x, 
		                               currentMountain.transform.position.y, 
		                               currentMountain.transform.position.z);
			GameObject.Destroy (currentMountain);
			GameObject volcano = Instantiate (Resources.Load ("volcano_1", typeof (GameObject)), mountPos, Quaternion.identity) as GameObject;
			GetComponent<MouseController>()._volcanoes.Add (volcano);
		}

	}
}
