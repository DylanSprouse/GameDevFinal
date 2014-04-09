using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TownBuilder : MonoBehaviour {

	public Transform cityPrefab_level1;
	public GameObject[] greenTileArray;
	public List<GameObject> greenTileList = new List<GameObject>();
	public float checkDistance = 1f;
	public List<Vector3> adjacentPosition = new List<Vector3>();

	private GameObject selectedHexagon;
	private GameObject activeCity, spillCity;
	private bool mouseButtonHeld;
	private bool cityGrowing = false;

	void Start() {

		adjacentPosition = new List<Vector3>();
		greenTileList = new List<GameObject>();
		greenTileArray = GameObject.FindGameObjectsWithTag("Green");

		foreach (GameObject greenTile in greenTileArray) {
			greenTileList.Add (greenTile);


		}


	}

	void Update() {

		// set input variables

		mouseButtonHeld = (Input.GetMouseButton (0)) == true? true : false;

		Ray mouseRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit mouseHit = new RaycastHit();

		if (Physics.Raycast (mouseRay, out mouseHit, 1000f) && (Input.GetMouseButton(0))) {
			
			if (mouseHit.collider.gameObject.tag == "Green" && !cityGrowing) {
				selectedHexagon = mouseHit.collider.gameObject;
				greenTileList.Remove (selectedHexagon);

				GameObject activeCity = Instantiate (cityPrefab_level1, new Vector3(selectedHexagon.transform.position.x, 
				                                                                    selectedHexagon.transform.position.y + 0.5f, 
				                                                                    selectedHexagon.transform.position.z),
				                                                                    Quaternion.identity) as GameObject;
				cityGrowing = true;

				if (checkIfAdjacent (selectedHexagon.transform.position.x ,selectedHexagon.transform.position.z)) {

					Debug.Log ("Growing");
					for (int i = 0; i < adjacentPosition.Count; i++) {

						GameObject spillCity = Instantiate (cityPrefab_level1, new Vector3(adjacentPosition[i].x, adjacentPosition[i].y + 0.5f, adjacentPosition[i].z),
					                                     Quaternion.identity) as GameObject;
					}
					
				} else if (!checkIfAdjacent (selectedHexagon.transform.position.x ,selectedHexagon.transform.position.z)) {

					Debug.Log ("Not growing.");

				}


			}


				
				
			}


	}

	public bool checkIfAdjacent (float x, float z) {

		foreach (GameObject currentHexagon in greenTileList) {

			if (Vector3.Distance (currentHexagon.transform.position,
			                      selectedHexagon.transform.position) <= checkDistance) {

				adjacentPosition.Add (currentHexagon.transform.position);
			}


		}
		if (adjacentPosition.Count > 0) {
			
			return true;

		} else {

			return false;
		}
		
		
	}


}
