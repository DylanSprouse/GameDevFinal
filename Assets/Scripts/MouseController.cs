using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MouseController : MonoBehaviour {

	public Transform cityPrefab;

	public List<GameObject> greenTileList;
	//public List<GameObject> blueTileArray;
	//public List<GameObject> blueTileList;
	public List<GameObject> builtTileList;
	public List<GameObject> _volcanoes;

	public List<GameObject> builtPlainsTileList;
	public List<GameObject> builtDesertTileList;
	public List<GameObject> builtSnowTileList;

	public GameObject[] greenTileArray;
	public GameObject _tornado;
	public GameObject selectedHexagon;
	public GameObject selectedCity;
	public Light directionalLight;

	public GUIText hubrisCounter;
	public GUIText gameOver;
	public GUIText dayTimer;
	public GUIText goldenAgeText;

	public bool firstCityBuilt = false;
	public bool goldenAge = false;
	public bool highlit = true;


	public float plainsCityDecayRate = 0.0007f, snowCityDecayRate = 0.001f, desertCityDecayRate = 0.0004f;
	public float cityExpansionRate = 0.05f;

	public Material goldenAgeMaterial;
	public Material nonGoldenAgeMaterial;
	public Material highlight;

	public AudioSource buildSound;

	public int goldenAgeCounter = 0;
	public int hubrisAmount = 50;
	public int _day = 1;
	public int _season = 0;

	public bool hubrisDelay = false, dayDelay = false, gameIsOver = false;
	public bool lockPlacement = false;
	public bool mouseButtonHeld;

	public string nextLevel = "EndScore";

	
	void Start () {

		goldenAgeText.enabled = false;
		gameOver.enabled = false;

		_volcanoes = new List<GameObject>();

		greenTileList = new List<GameObject>();
		//blueTileList = new List<GameObject>();
		builtTileList = new List<GameObject>();

		builtPlainsTileList = new List<GameObject>();
		builtDesertTileList = new List<GameObject>();
		builtSnowTileList = new List<GameObject>();

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
				builtPlainsTileList.Add (selectedHexagon);

				GameObject instance = Instantiate (Resources.Load ("babel_1", typeof (GameObject)), new Vector3 (selectedHexagon.transform.position.x,
				                                                                selectedHexagon.transform.position.y + 0.2f,
				                                                                selectedHexagon.transform.position.z),

				                                   Quaternion.identity) as GameObject;
				instance.transform.localScale = new Vector3 (1.5f, 1.5f, 1.5f);
				selectedCity = instance.gameObject;
				selectedCity.transform.parent = selectedHexagon.transform;

				}

				// Instantiate desert monument

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
					builtDesertTileList.Add (selectedHexagon);
					
					GameObject instance = Instantiate (Resources.Load ("Lighthouse", typeof (GameObject)), new Vector3 (selectedHexagon.transform.position.x,
					                                                                                                 selectedHexagon.transform.position.y + 0.2f,
					                                                                                                 selectedHexagon.transform.position.z),
					                                   
					                                   Quaternion.identity) as GameObject;
					instance.transform.localScale = new Vector3 (3.7f, 3.7f, 3.7f);
					selectedCity = instance.gameObject;
					selectedCity.transform.parent = selectedHexagon.transform;
					
				}

				// Instantiate tundra monument

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
					builtSnowTileList.Add (selectedHexagon);
					
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
			
				for (int x = 0; x < builtPlainsTileList.Count; x++) {

					foreach (Transform plainsChild in builtPlainsTileList[x].transform) {

						if (plainsChild.transform.localScale.y < 0.001f) {

							Destroy (plainsChild.gameObject);
							builtTileList.Remove (builtTileList[i]);
							builtPlainsTileList.Remove (builtPlainsTileList[x]);
							goldenAgeCounter = 0;

						} else if (!goldenAge) {
							
							plainsChild.transform.localScale -= new Vector3 (plainsCityDecayRate, plainsCityDecayRate, plainsCityDecayRate);
							
						}


					}


				}

				for (int y = 0; y < builtSnowTileList.Count; y++) {
					
					foreach (Transform snowChild in builtSnowTileList[y].transform) {
						
						if (snowChild.transform.localScale.y < 0.001f) {
							
							Destroy (snowChild.gameObject);
							builtTileList.Remove (builtTileList[i]);
							builtSnowTileList.Remove (builtSnowTileList[y]);
							goldenAgeCounter = 0;
							
						} else if (!goldenAge) {
							
							snowChild.transform.localScale -= new Vector3 (snowCityDecayRate, snowCityDecayRate, snowCityDecayRate);
							
						}
						
						
					}
					
					
				}

				for (int z = 0; z < builtDesertTileList.Count; z++) {
					
					foreach (Transform desertChild in builtDesertTileList[z].transform) {
						
						if (desertChild.transform.localScale.y < 0.001f) {
							
							Destroy (desertChild.gameObject);
							builtTileList.Remove (builtTileList[i]);
							builtDesertTileList.Remove (builtDesertTileList[z]);
							goldenAgeCounter = 0;
							
						} else if (!goldenAge) {
							
							desertChild.transform.localScale -= new Vector3 (desertCityDecayRate, desertCityDecayRate, desertCityDecayRate);
							
						}
						
						
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

			plainsCityDecayRate = 0.008f;
			snowCityDecayRate = 0.008f;
			desertCityDecayRate = 0.008f;

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
			Application.LoadLevel(nextLevel);



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
		_season++;

		if (_season == 4) {
			_season = 0;
			directionalLight.color = Color.red;

		}

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
