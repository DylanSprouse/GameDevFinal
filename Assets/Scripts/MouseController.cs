using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MouseController : MonoBehaviour {

	public Transform cityPrefab;

	public List<GameObject> greenTileList;
	public List<GameObject> builtTileList;
	public List<GameObject> _volcanoes;
	public List<Vector3> adjacentileTile;

	public GameObject[] greenTileArray;
	public GameObject _tornado;
	public GameObject selectedHexagon;
	public GameObject selectedCity;

	public GUIText hubrisCounter;
	public GUIText gameOver;
	public GUIText dayTimer;
	public GUIText goldenAgeText;

	public bool firstCityBuilt = false;
	public bool goldenAge = false;

	public float cityDecayRate = 0.0007f;
	public float cityExpansionRate = 0.05f;

	public Material goldenAgeMaterial;
	public Material nonGoldenAgeMaterial;

	public AudioSource buildSound;

	public int goldenAgeCounter = 0;
	public int hubrisAmount = 50;
	public int _day = 1;

	private bool hubrisDelay = false, dayDelay = false, gameIsOver = false;
	private bool lockPlacement = false;
	private bool mouseButtonHeld;
	
	void Start () {

		goldenAgeText.enabled = false;
		gameOver.enabled = false;
		adjacentileTile = new List<Vector3>();
		_volcanoes = new List<GameObject>();
		greenTileList = new List<GameObject>();
		builtTileList = new List<GameObject>();
		StartCoroutine(AddTiles());

		hubrisCounter.text = "Hubris: " + hubrisAmount;

	}

	void Update () {

		mouseButtonHeld = (Input.GetMouseButton (0)) == true? true : false;

		Ray mouseRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit mouseHit = new RaycastHit();


		if (!dayDelay && !gameIsOver && firstCityBuilt) {
			
			StartCoroutine (AddDay());
			
		}

		// Mouse city interaction

		if (Physics.Raycast (mouseRay, out mouseHit, 1000f) && mouseButtonHeld) {

			// Mouse building placement

			if (mouseHit.collider.gameObject.tag == "Green" || mouseHit.collider.gameObject.tag == "Yellow"
			 || mouseHit.collider.gameObject.tag == "White") {



				selectedHexagon = mouseHit.collider.gameObject;

				// Instantiate plains monument

				if (!builtTileList.Contains (mouseHit.collider.gameObject) && hubrisAmount >= 5 
				    && !lockPlacement && mouseHit.collider.gameObject.tag == "Green") {

				lockPlacement = true;

				if (!firstCityBuilt) {

					firstCityBuilt = true;

					}

				hubrisAmount -= 5;
				hubrisCounter.text = "Hubris: " + hubrisAmount;
				buildSound.Play ();

				builtTileList.Add (selectedHexagon);

				GameObject instance = Instantiate (Resources.Load ("babel_1", typeof (GameObject)), new Vector3 (selectedHexagon.transform.position.x,
				                                                                selectedHexagon.transform.position.y + 0.2f,
				                                                                selectedHexagon.transform.position.z),

				                                   Quaternion.identity) as GameObject;
				instance.transform.localScale = new Vector3 (1.5f, 1.5f, 1.5f);
				selectedCity = instance.gameObject;
				selectedCity.transform.parent = selectedHexagon.transform;

				}

				// Instantiate tundra monument

				if (!builtTileList.Contains (mouseHit.collider.gameObject) && hubrisAmount >= 7 
				    && !lockPlacement && mouseHit.collider.gameObject.tag == "Yellow") {
					
					lockPlacement = true;
					
					if (!firstCityBuilt) {
						
						firstCityBuilt = true;
						
					}
					
					hubrisAmount -= 7;
					hubrisCounter.text = "Hubris: " + hubrisAmount;
					buildSound.Play ();
					
					builtTileList.Add (selectedHexagon);
					
					GameObject instance = Instantiate (Resources.Load ("Lighthouse", typeof (GameObject)), new Vector3 (selectedHexagon.transform.position.x,
					                                                                                                 selectedHexagon.transform.position.y + 0.2f,
					                                                                                                 selectedHexagon.transform.position.z),
					                                   
					                                   Quaternion.identity) as GameObject;
					instance.transform.localScale = new Vector3 (3.7f, 3.7f, 3.7f);
					selectedCity = instance.gameObject;
					selectedCity.transform.parent = selectedHexagon.transform;
					
				}

				// Instantiate desert monument

				if (!builtTileList.Contains (mouseHit.collider.gameObject) && hubrisAmount >= 3 
				    && !lockPlacement && mouseHit.collider.gameObject.tag == "White") {
					
					lockPlacement = true;
					
					if (!firstCityBuilt) {
						
						firstCityBuilt = true;
						
					}
					
					hubrisAmount -= 3;
					hubrisCounter.text = "Hubris: " + hubrisAmount;
					buildSound.Play ();
					
					builtTileList.Add (selectedHexagon);
					
					GameObject instance = Instantiate (Resources.Load ("Castle", typeof (GameObject)), new Vector3 (selectedHexagon.transform.position.x,
					                                                                                                 selectedHexagon.transform.position.y + 0.2f,
					                                                                                                 selectedHexagon.transform.position.z),
					                                   
					                                   Quaternion.identity) as GameObject;
					instance.transform.localScale = new Vector3 (0.05f, 0.05f, 0.05f);
					selectedCity = instance.gameObject;
					selectedCity.transform.parent = selectedHexagon.transform;
					
				}

				if (builtTileList.Contains (mouseHit.collider.gameObject)) {

					lockPlacement = true;
					
					foreach (Transform child in selectedHexagon.transform) {
						
						if (child.transform.localScale.y < 3.5f) {
							
							child.transform.localScale += new Vector3 (cityExpansionRate, cityExpansionRate, cityExpansionRate);
							
						}
						
					}
					
				}

			}

		}

		if (Input.GetMouseButtonUp(0)) {

			lockPlacement = false;

		}


		// General decay rate

		if (builtTileList.Count > 0) {
		for (int i = 0; i < builtTileList.Count; i++) {
			
			foreach (Transform child in builtTileList[i].transform) {
				
					if (child.transform.localScale.y < 0.001f) {

						Destroy (child.gameObject);
						builtTileList.Remove (builtTileList[i]);
						goldenAgeCounter = 0;

					} else if (!goldenAge) {

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
			//Time.timeScale = 0f;


		}
	}

	// Add green tiles to list

	private IEnumerator AddTiles() {
		yield return new WaitForSeconds(0.25f);
		greenTileArray = GameObject.FindGameObjectsWithTag("Green");
		
		foreach (GameObject greenTile in greenTileArray) {
			
			greenTileList.Add (greenTile);
		}
	}

	// Golden Age effect

	private IEnumerator GoldenAges() {
		cityExpansionRate = 0.1f;
		goldenAgeText.enabled = true;

		yield return new WaitForSeconds(15f);

		goldenAgeText.enabled = false;
		goldenAge = false;
		cityExpansionRate = 0.05f;


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
		GetComponent<NaturalDisasters>().difficultyCounter++;

		if (!goldenAge) {
		goldenAgeCounter++;
		}

		if (goldenAgeCounter == 7 && !goldenAge) {
			goldenAge = true;
			goldenAgeCounter = 0;
			StartCoroutine (GoldenAges());
		}


		dayTimer.text = "Year: " + _day;


	}
}
