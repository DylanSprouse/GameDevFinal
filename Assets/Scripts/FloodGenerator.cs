using UnityEngine;
using System.Collections;

public class FloodGenerator : MonoBehaviour {
	
	public Transform blueTile;
	public bool floodEnabled = false;
	//public ParticleSystem rain;
	public GameObject rain;
	GameObject rainPrefabClone;
	public float time = 5f;
	public int waterHeight = 30;

	public AudioSource rainSound;
	
	// Use this for initialization
	void Start () {
		
	}
	// Update is called once per frame
	void Update () {
		if (floodEnabled && MouseController.Instance._day < 25) {

			floodEnabled = false;

			for (int i = 0; i < 1; i++) {

				int randomHex = Random.Range (0, GetComponent<MouseController>().greenTileList.Count);
				GameObject current = GetComponent<MouseController>().greenTileList[randomHex];
				Vector3 hexPos = new Vector3(current.transform.position.x, current.transform.position.y, current.transform.position.z);
				Vector3 hexPos1 = new Vector3(current.transform.position.x, (current.transform.position.y)+waterHeight, current.transform.position.z);
				if (GetComponent<MouseController>().builtTileList.Contains (current)) {
					GetComponent<MouseController>().goldenAgeCounter = 0;
					GetComponent<MouseController>().gaT = 0f;
					MouseController.Instance.screamingSound.Play ();
				}
				GameObject.Destroy(current);
				GetComponent<MouseController>().greenTileList.Remove (current);
				GetComponent<MouseController>().builtTileList.Remove (current);
				GetComponent<MouseController>().builtTileList.Remove (current);

				rainPrefabClone = Instantiate(Resources.Load ("RainPrefab", typeof (GameObject)), hexPos1, Quaternion.identity) as GameObject;
				Instantiate(blueTile, hexPos, Quaternion.identity);
				Destroy(rainPrefabClone, time);
				rainSound.Play();
			}
		}

		if (floodEnabled && MouseController.Instance._day >= 25) {
			
			floodEnabled = false;
			
			for (int i = 0; i < 1; i++) {
				
				int randomHex = Random.Range (0, GetComponent<MouseController>().builtTileList.Count);
				GameObject current = GetComponent<MouseController>().builtTileList[randomHex];
				Vector3 hexPos = new Vector3(current.transform.position.x, current.transform.position.y, current.transform.position.z);
				Vector3 hexPos1 = new Vector3(current.transform.position.x, (current.transform.position.y)+waterHeight, current.transform.position.z);

				if (GetComponent<MouseController>().builtTileList.Contains (current)) {
					GetComponent<MouseController>().goldenAgeCounter = 0;
					GetComponent<MouseController>().gaT = 0f;
				}
				GameObject.Destroy(current);
				GetComponent<MouseController>().greenTileList.Remove (current);
				GetComponent<MouseController>().builtTileList.Remove (current);
				GetComponent<MouseController>().builtTileList.Remove (current);

				rainPrefabClone = Instantiate(Resources.Load ("RainPrefab", typeof (GameObject)), hexPos1, Quaternion.identity) as GameObject;
				Instantiate(blueTile, hexPos, Quaternion.identity);
				Destroy(rainPrefabClone, time);
				rainSound.Play();
			}
		}
	}
}