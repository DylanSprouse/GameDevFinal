using UnityEngine;
using System.Collections;

public class VolcanoGenerator : MonoBehaviour {
	
	public Transform blackTile;
	
	public bool volcanoEnabled = false;
	
	public AudioSource volcanoSound;
	
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
			volcanoSound.Play ();
			int randomMountain = Random.Range (0, mountainTiles.Length);
			GameObject currentMountain = mountainTiles[randomMountain];
			Vector3 mountPos = new Vector3(currentMountain.transform.position.x, 
			                               currentMountain.transform.position.y, 
			                               currentMountain.transform.position.z);
			GameObject.Destroy (currentMountain);
			GameObject volcano = Instantiate (Resources.Load ("volcano_1", typeof (GameObject)), mountPos, Quaternion.identity) as GameObject;

			MouseController.Instance._volcanoes.Add (volcano);
		}
		
	}
	void VolcanoDestroy(float xPos, float yPos, float zPos){
		Vector3 hexPos = new Vector3 (xPos, yPos, zPos);
		Debug.Log ("Volcano destroy being run.");
		foreach (GameObject current in MouseController.Instance.greenTileList) {
			if (current.transform.position == hexPos) {
				if (current.tag == "Green") {
					MouseController.Instance.greenTileList.Remove (current);
				} else if (current.tag == "Yellow") {
					MouseController.Instance.greenTileList.Remove (current);
				} else if (current.tag == "White") {
					MouseController.Instance.greenTileList.Remove (current);
				}
				MouseController.Instance.builtTileList.Remove (current);
				Destroy (current.gameObject);
				Instantiate(Resources.Load("blackTile_1", typeof (GameObject)), hexPos, Quaternion.identity);
			}
		}
	}
}
