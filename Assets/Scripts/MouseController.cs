using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MouseController : MonoBehaviour {

	public Transform cityPrefab;
	public GameObject[] greenTileArray;
	public List<GameObject> greenTileList;
	//public List<GameObject> blueTileArray;
	//public List<GameObject> blueTileList;
	public List<GameObject> builtTileList;
	public List<GameObject> _volcanoes;
	public List<Vector3> adjacentileTile;
	public GameObject _tornado;
	public GUIText hubrisCounter;
	public GUIText gameOver;
	public GUIText dayTimer;
	public bool firstCityBuilt = false;
	public float cityDecayRate = 0.001f;

	public int hubrisAmount = 50;
	public int _day = 1;
	private bool hubrisDelay = false, difficultyDelay = false, dayDelay = false, gameIsOver = false;

	public GameObject selectedHexagon;
	public GameObject selectedCity;
	private bool mouseButtonHeld;
	
	void Start () {

		gameOver.enabled = false;
		adjacentileTile = new List<Vector3>();
		_volcanoes = new List<GameObject>();
		greenTileList = new List<GameObject>();
		//blueTileList = new List<GameObject>();
		builtTileList = new List<GameObject>();
		StartCoroutine(AddTiles());

		hubrisCounter.text = "Hubris: " + hubrisAmount;

	}

	void Update () {

		mouseButtonHeld = (Input.GetMouseButton (0)) == true? true : false;

		Ray mouseRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit mouseHit = new RaycastHit();

		if (!difficultyDelay) {

			StartCoroutine (DifficultyRamp());

		}

		if (!dayDelay && !gameIsOver && firstCityBuilt) {
			
			StartCoroutine (AddDay());
			
		}

		// Mouse city interaction

		if (Physics.Raycast (mouseRay, out mouseHit, 1000f) && mouseButtonHeld) {

			// Mouse building placement

			if (mouseHit.collider.gameObject.tag == "Green") {

				selectedHexagon = mouseHit.collider.gameObject;

				if (!builtTileList.Contains (mouseHit.collider.gameObject) && hubrisAmount >= 10) {

				firstCityBuilt = true;
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

				// Mouse building growth

				if (builtTileList.Contains (mouseHit.collider.gameObject)) {

					foreach (Transform child in selectedHexagon.transform) {

					if (child.transform.localScale.y < 3.5f) {

					child.transform.localScale += new Vector3 (0.05f, 0.05f, 0.05f);

						}

					}

				}
			}
		}

		// General decay rate

		if (builtTileList.Count > 0) {
		for (int i = 0; i < builtTileList.Count; i++) {
			
			foreach (Transform child in builtTileList[i].transform) {
				
					if (child.transform.localScale.y < 0.001f) {

						Destroy (child.gameObject);
						builtTileList.Remove (builtTileList[i]);

					} else {

						child.transform.localScale -= new Vector3 (cityDecayRate, cityDecayRate, cityDecayRate);

					}
				}
			}
			
		}

		// Tornado effect

		if (builtTileList.Count > 0 && GetComponent<TornadoGenerator>().tornadoEnabled) {

			for (int i = 0; i < builtTileList.Count; i++) {

				if (Vector3.Distance (builtTileList[i].transform.position, _tornado.transform.position) < 5f) {

					foreach (Transform child in builtTileList[i].transform) {

						child.transform.localScale -= new Vector3 (0.025f, 0.025f, 0.025f);

					}

				}

			}

		}

		// Volcano effect

		if (builtTileList.Count > 0 && _volcanoes.Count > 0) {

			for (int i = 0; i < _volcanoes.Count; i++) {

				foreach (GameObject builtCity in builtTileList) {

					if (Vector3.Distance (builtCity.transform.position,
					                      _volcanoes[i].transform.position) < 5f) {
						
						foreach (Transform child in builtCity.transform) {
							
							child.transform.localScale -= new Vector3 (0.05f, 0.05f, 0.05f);
							
						}

					}

				}
				             

			}

		}

		// Earthquake effect

		if (GetComponent<EarthquakeGenerator>().earthquakeEnabled) {

			cityDecayRate = 0.008f;

		}

		// Hubris adding over time
		
		if (!hubrisDelay) {
			
			StartCoroutine (HubrisAdd());
			
		}

		// Game over

		if (firstCityBuilt && builtTileList.Count == 0) {

			gameIsOver = true;
			gameOver.enabled = true;
			Time.timeScale = 0f;


		}
	}

	// Add green tiles to list

	private IEnumerator AddTiles() {
		yield return new WaitForSeconds(0.25f);
		greenTileArray = GameObject.FindGameObjectsWithTag("Green");
		//blueTileArray = GameObject.FindGameObjectsWithTag("Blue");
		
		foreach (GameObject greenTile in greenTileArray) {
			
			greenTileList.Add (greenTile);
		}

//		foreach (GameObject blueTile in blueTileArray) {
//			
//			blueTileList.Add (blueTile);
//		}
	}

	// Ramp difficulty over time

	private IEnumerator DifficultyRamp() {
		difficultyDelay = true;
		yield return new WaitForSeconds(10f);
		cityDecayRate += 0.0001f;
		difficultyDelay = false;
		Debug.Log (cityDecayRate);


	}

	// Hubris tick over time

	private IEnumerator HubrisAdd() {
		hubrisDelay = true;
		if (builtTileList.Count > 0) {
		hubrisAmount += builtTileList.Count * 1;
		}
		hubrisCounter.text = "Hubris: " + hubrisAmount;
		yield return new WaitForSeconds(7f);
		hubrisDelay = false;

		}

	// Time counter

	private IEnumerator AddDay() {
		dayDelay = true;
		yield return new WaitForSeconds(15f);
		dayDelay = false;
		_day++;
		dayTimer.text = "Day: " + _day;


	}
}
