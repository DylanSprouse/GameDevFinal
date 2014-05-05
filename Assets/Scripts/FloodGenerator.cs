using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class FloodGenerator : MonoBehaviour {
	
	public Transform blueTile;
	public bool floodEnabled = false;
	//public ParticleSystem rain;
	public GameObject rain;
	public List<GameObject> blueTileList;
	GameObject rainPrefabClone;
	public float time = 5f;
	public int waterHeight = 30;
	public Vector3 temp;

	private bool floodActive = false;

	public AudioSource rainSound;

	void Update () {
		if (floodEnabled) {

			floodActive = false;

			floodEnabled = false;

			while (!floodActive) {

			blueTileList = GameObject.FindGameObjectsWithTag("Blue").ToList();

			int randomHex = Random.Range (0, blueTileList.Count);
			GameObject current = blueTileList[randomHex];

				for (int i = 0; i < MouseController.Instance.greenTileList.Count; i++) {

				temp = new Vector3 (MouseController.Instance.greenTileList[i].transform.position.x,
				                    MouseController.Instance.greenTileList[i].transform.position.y,
				                    MouseController.Instance.greenTileList[i].transform.position.z);

				if (Vector3.Distance (MouseController.Instance.greenTileList[i].transform.position, current.transform.position) < 3.75f) {

					floodActive = true;
					GameObject instance = Instantiate (Resources.Load ("blueTile_1", typeof (GameObject)), new Vector3 (MouseController.Instance.greenTileList[i].transform.position.x,
					                                                                                                     MouseController.Instance.greenTileList[i].transform.position.y,
					                                                                                                     MouseController.Instance.greenTileList[i].transform.position.z),
					                                   
					                                   Quaternion.identity) as GameObject;

					Destroy (MouseController.Instance.greenTileList[i]);
					MouseController.Instance.greenTileList.Remove (MouseController.Instance.greenTileList[i]);

					for (int j = 0;j < MouseController.Instance.builtTileList.Count; j++) {

						if(MouseController.Instance.builtTileList[j].transform.position == temp){
							MouseController.Instance.goldenAgeCounter = 0;
							MouseController.Instance.gaT = 0f;
								Instantiate (Resources.Load ("ruins_1", typeof (GameObject)), new Vector3 (MouseController.Instance.builtTileList[j].transform.position.x,
								                                                                           MouseController.Instance.builtTileList[j].transform.position.y + 0.2f,
								                                                                           MouseController.Instance.builtTileList[j].transform.position.z),
								             Quaternion.identity);
								if (!MouseController.Instance.screamingSound.isPlaying) {
									MouseController.Instance.screamingSound.Play ();
								}
							MouseController.Instance.builtTileList.Remove (MouseController.Instance.builtTileList[j]);
						}
					}

					rainPrefabClone = Instantiate(Resources.Load ("RainPrefab", typeof (GameObject)),
					                              new Vector3 (current.transform.position.x,
					            							   current.transform.position.y + 15f,
					             							   current.transform.position.z),
					                              Quaternion.identity) as GameObject;
					Destroy(rainPrefabClone, time);
					rainSound.Play();
				

						}
					}


				}



				/*Vector3 hexPos = new Vector3(current.transform.position.x, current.transform.position.y, current.transform.position.z);
				Vector3 hexPos1 = new Vector3(current.transform.position.x, (current.transform.position.y)+waterHeight, current.transform.position.z);

	

				rainPrefabClone = Instantiate(Resources.Load ("RainPrefab", typeof (GameObject)), hexPos1, Quaternion.identity) as GameObject;
				Instantiate(blueTile, hexPos, Quaternion.identity);
				Destroy(rainPrefabClone, time);
				rainSound.Play(); */

		}
	}
}