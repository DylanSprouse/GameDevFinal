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
		if (floodEnabled) {

			floodEnabled = false;

			for (int i = 0; i < 1; i++) {

				int randomHex = Random.Range (0, GetComponent<MouseController>().greenTileList.Count);
				GameObject current = GetComponent<MouseController>().greenTileList[randomHex];
				Vector3 hexPos = new Vector3(current.transform.position.x, current.transform.position.y, current.transform.position.z);
				Vector3 hexPos1 = new Vector3(current.transform.position.x, (current.transform.position.y)+waterHeight, current.transform.position.z);
				GetComponent<MouseController>().greenTileList.Remove (current);
				GetComponent<MouseController>().builtTileList.Remove (current);
				GameObject.Destroy(current);
				//Instantiate(blueTile, hexPos, Quaternion.identity);
				rainPrefabClone = Instantiate(Resources.Load ("RainPrefab", typeof (GameObject)), hexPos1, Quaternion.identity) as GameObject;
				Instantiate(blueTile, hexPos, Quaternion.identity);
				Destroy(rainPrefabClone, time);
				rainSound.Play();
			}
		}
	}
}