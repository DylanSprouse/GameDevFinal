using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MouseController : MonoBehaviour {

	public Transform cityPrefab;
	public GameObject[] greenTileArray;
	public List<GameObject> greenTileList;
	public List<GameObject> builtTileList;
	//public float distanceCheck = 4f;
	public List<Vector3> adjacentileTile;
	public GameObject _tornado;
	public GUIText hubrisCounter;

	public int hubrisAmount = 50;
	private bool hubrisDelay = false;

	public GameObject selectedHexagon;
	public GameObject selectedCity;
	private bool mouseButtonHeld;
	
	void Start () {
		
		adjacentileTile = new List<Vector3>();
		greenTileList = new List<GameObject>();
		builtTileList = new List<GameObject>();
		StartCoroutine(AddTiles());

		hubrisCounter.text = "Hubris: " + hubrisAmount;

	}

	void Update () {

		mouseButtonHeld = (Input.GetMouseButton (0)) == true? true : false;

		Ray mouseRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit mouseHit = new RaycastHit();

		if (Physics.Raycast (mouseRay, out mouseHit, 1000f) && mouseButtonHeld) {

			if (mouseHit.collider.gameObject.tag == "Green") {

				selectedHexagon = mouseHit.collider.gameObject;

				if (!builtTileList.Contains (mouseHit.collider.gameObject) && hubrisAmount > 10) {

				hubrisAmount -= 10;
				hubrisCounter.text = "Hubris: " + hubrisAmount;

				builtTileList.Add (selectedHexagon);

				GameObject instance = Instantiate (Resources.Load ("babel_1", typeof (GameObject)), new Vector3 (selectedHexagon.transform.position.x,
				                                                                selectedHexagon.transform.position.y + 0.2f,
				                                                                selectedHexagon.transform.position.z),

				                                   Quaternion.identity) as GameObject;
				instance.transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
				selectedCity = instance.gameObject;
				selectedCity.transform.parent = selectedHexagon.transform;

				}
				
				if (builtTileList.Contains (mouseHit.collider.gameObject)) {

					foreach (Transform child in selectedHexagon.transform) {

					if (child.transform.localScale.y < 3.5f) {

					child.transform.localScale += new Vector3 (0.05f, 0.05f, 0.05f);

						}

					}

				}
			}
		}

		if (builtTileList.Count > 0) {
		for (int i = 0; i < builtTileList.Count; i++) {
			
			foreach (Transform child in builtTileList[i].transform) {
				
					if (child.transform.localScale.y < 0.001f) {

						Destroy (child.gameObject);
						builtTileList.Remove (builtTileList[i]);

					} else {

				child.transform.localScale -= new Vector3 (0.001f, 0.001f, 0.001f);

					}
				}
			}
			
		}

		if (!hubrisDelay) {
			
			StartCoroutine (HubrisAdd());
			
		}

		if (builtTileList.Count > 0) {

			for (int i = 0; i < builtTileList.Count; i++) {

				if (Vector3.Distance (builtTileList[i].transform.position, _tornado.transform.position) < 5f) {

					foreach (Transform child in builtTileList[i].transform) {

						child.transform.localScale -= new Vector3 (0.01f, 0.01f, 0.01f);

					}

				}

			}

		}
	}

	private IEnumerator AddTiles() {
		yield return new WaitForSeconds(0.25f);
		greenTileArray = GameObject.FindGameObjectsWithTag("Green");
		
		foreach (GameObject greenTile in greenTileArray) {
			
			greenTileList.Add (greenTile);
		}
	}

	private IEnumerator HubrisAdd() {
		hubrisDelay = true;
		if (builtTileList.Count > 0) {
		hubrisAmount += builtTileList.Count * 1;
		}
		hubrisCounter.text = "Hubris: " + hubrisAmount;
		yield return new WaitForSeconds(5f);
		hubrisDelay = false;

		}
}
