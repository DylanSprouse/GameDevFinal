using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MouseController : MonoBehaviour {

	public static MouseController Instance { get; private set; }

	public Transform cityPrefab;

	public List<GameObject> greenTileList;
	public List<GameObject> builtTileList;
	public List<GameObject> _volcanoes;
	public List<GameObject> _mountains;

	public GameObject[] greenTileArray, yellowTileArray, whiteTileArray;
	public GameObject selectedHexagon;
	public GameObject selectedCity;
	public GameObject hubrisAddGUI;
	public TextMesh hubrisTickGUI;
	public GameObject gaTickGUI;
	public Light directionalLight;

	public GUIText hubrisCounter;
	public GUIText gameOver;
	public GUIText dayTimer;
	public GUIText goldenAgeText;

	public bool firstCityBuilt = false;
	public bool goldenAge = false;

	public float cityDecayRate = 0.001f;
	public float cityExpansionRate = 0.05f;
	public float tornadoDecay = 0.5f;
	public float earthquakeDecay = 0.01f;

	public Material goldenAgeMaterial;
	public Material nonGoldenAgeMaterial;

	public AudioSource buildSound, screamingSound;

	public int goldenAgeCounter = 0;
	public int hubrisAmount = 50;
	public int _day = 1;
	private int _season = 0;

	private float t = 0f;
	private float tickDuration = 7f;
	private float timer = 1f;

	public float gaT = 0f;
	private float gaDelayDuration = 105f;
	private float gaTimer = 1f;

	private bool hubrisDelay = false, dayDelay = false, gameIsOver = false;
	private bool lockPlacement = false;
	private bool mouseButtonHeld;

	void Awake() {

		if (Instance != null && Instance != this) {

			Destroy(gameObject);

		}

		Instance = this;

		DontDestroyOnLoad(gameObject);



	}

	void Start () {

		goldenAgeText.enabled = false;
		gameOver.enabled = false;

		_volcanoes = new List<GameObject>();
		_mountains = new List<GameObject>();

		greenTileList = new List<GameObject>();
		builtTileList = new List<GameObject>();

		StartCoroutine(AddTiles());

		hubrisCounter.text = (hubrisAmount.ToString ());
		hubrisTickGUI.renderer.material.color = Color.blue;
		gaT = 0f;

	}

	void Update () {

		mouseButtonHeld = (Input.GetMouseButton (0)) == true? true : false;

		Ray mouseRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit mouseHit = new RaycastHit();


			hubrisAddGUI.renderer.material.SetFloat ("_Cutoff", Mathf.Lerp (timer* 1f, timer * 0f, t));
			if (t < 1) {
			
			t += Time.deltaTime/tickDuration;
				}
		
		if (firstCityBuilt) {
		gaTickGUI.renderer.material.SetFloat ("_Cutoff", Mathf.Lerp (gaTimer* 1f, gaTimer * 0f, gaT));
		if (gaT < 1) {
			
			gaT += Time.deltaTime/gaDelayDuration;
		}
		}
		if (goldenAge) {

			hubrisTickGUI.text = "+" + builtTileList.Count * 2;
			hubrisTickGUI.renderer.material.color = Color.white;

		} else {

			hubrisTickGUI.text = "+" + builtTileList.Count;
			hubrisTickGUI.renderer.material.color = Color.blue;
		}


		if (!dayDelay && !gameIsOver && firstCityBuilt) {
			
			StartCoroutine (AddDay());
			
		}

		if (hubrisAmount < 10) {

			hubrisCounter.material.color = Color.red;

		} else {

			hubrisCounter.material.color = Color.white;

		}

		// Mouse city interaction

		if (Physics.Raycast (mouseRay, out mouseHit, 1000f) && mouseButtonHeld) {

			// Mouse building placement

			if (mouseHit.collider.gameObject.tag == "Green" || mouseHit.collider.gameObject.tag == "Yellow"
			 || mouseHit.collider.gameObject.tag == "White") {



				selectedHexagon = mouseHit.collider.gameObject;

				// Instantiate plains monument

				if (!builtTileList.Contains (mouseHit.collider.gameObject) && hubrisAmount >= 10 
				    && !lockPlacement && mouseHit.collider.gameObject.tag == "Green") {

				lockPlacement = true;

				if (!firstCityBuilt) {

					firstCityBuilt = true;

					}

				hubrisAmount -= 10;
				hubrisCounter.text = (hubrisAmount.ToString ());
				buildSound.Play ();

				builtTileList.Add (selectedHexagon);

				GameObject instance = Instantiate (Resources.Load ("babel_1", typeof (GameObject)), new Vector3 (selectedHexagon.transform.position.x,
				                                                                selectedHexagon.transform.position.y + 0.2f,
				                                                                selectedHexagon.transform.position.z),

				                                   Quaternion.identity) as GameObject;
				Instantiate (Resources.Load ("gui_costCounter", typeof (GameObject)), new Vector3 (instance.transform.position.x,
					                                                                                   instance.transform.position.y + 2f,
					                                                                                   instance.transform.position.z),
					             Quaternion.identity);	
				instance.transform.localScale = new Vector3 (0.05f, 0.05f, 0.05f);
				selectedCity = instance.gameObject;
				selectedCity.transform.parent = selectedHexagon.transform;

				}

				// Instantiate desert monument

				if (!builtTileList.Contains (mouseHit.collider.gameObject) && hubrisAmount >= 10 
				    && !lockPlacement && mouseHit.collider.gameObject.tag == "Yellow") {
					
					lockPlacement = true;
					
					if (!firstCityBuilt) {
						
						firstCityBuilt = true;
						
					}
					
					hubrisAmount -= 10;
					hubrisCounter.text = (hubrisAmount.ToString ());
					buildSound.Play ();
					
					builtTileList.Add (selectedHexagon);
					
					GameObject instance = Instantiate (Resources.Load ("Lighthouse", typeof (GameObject)), new Vector3 (selectedHexagon.transform.position.x,
					                                                                                                 selectedHexagon.transform.position.y + 0.2f,
					                                                                                                 selectedHexagon.transform.position.z),
					                                   
					                                   Quaternion.identity) as GameObject;
					Instantiate (Resources.Load ("gui_costCounter", typeof (GameObject)), new Vector3 (instance.transform.position.x,
					                                                                                   instance.transform.position.y + 2f,
					                                                                                   instance.transform.position.z),
					             Quaternion.identity);
					instance.transform.localScale = new Vector3 (0.05f, 0.05f, 0.05f);
					selectedCity = instance.gameObject;
					selectedCity.transform.parent = selectedHexagon.transform;
					
				}

				// Instantiate tundra monument

				if (!builtTileList.Contains (mouseHit.collider.gameObject) && hubrisAmount >= 10 
				    && !lockPlacement && mouseHit.collider.gameObject.tag == "White") {
					
					lockPlacement = true;
					
					if (!firstCityBuilt) {
						
						firstCityBuilt = true;
						
					}
					
					hubrisAmount -= 10;
					hubrisCounter.text = (hubrisAmount.ToString ());
					buildSound.Play ();
					
					builtTileList.Add (selectedHexagon);
					
					GameObject instance = Instantiate (Resources.Load ("Castle", typeof (GameObject)), new Vector3 (selectedHexagon.transform.position.x,
					                                                                                                 selectedHexagon.transform.position.y + 0.2f,
					                                                                                                 selectedHexagon.transform.position.z),
					                                   
					                                   Quaternion.identity) as GameObject;

					Instantiate (Resources.Load ("gui_costCounter", typeof (GameObject)), new Vector3 (instance.transform.position.x,
					                                                                                   instance.transform.position.y + 2f,
					                                                                                   instance.transform.position.z),
					             Quaternion.identity);				

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
							builtTileList.Remove (builtTileList [i]);
							screamingSound.Play ();
							goldenAgeCounter = 0;
							gaT = 0f;

						} else if (!goldenAge) {
							
							child.transform.localScale -= new Vector3 (cityDecayRate, cityDecayRate, cityDecayRate);
							
						}

				}
			
			}

		}

		// Volcano effect

		if (builtTileList.Count > 0 && _volcanoes.Count > 0) {

			for (int i = 0; i < _volcanoes.Count; i++) {

				foreach (GameObject builtCity in builtTileList) {

					if (Vector3.Distance (builtCity.transform.position,
					                      _volcanoes[i].transform.position) < 4.5f) {
						
						foreach (Transform child in builtCity.transform) {
							
							child.transform.localScale -= new Vector3 (0.05f, 0.05f, 0.05f);
							
						}

					}

				}
				             

			}

		}

		// Earthquake effect

		if (GetComponent<EarthquakeGenerator>().earthquakeEnabled) {

			cityDecayRate = earthquakeDecay;

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

		yellowTileArray = GameObject.FindGameObjectsWithTag ("Yellow");

		foreach (GameObject yellowTile in yellowTileArray) {

			greenTileList.Add (yellowTile);

		}

		whiteTileArray = GameObject.FindGameObjectsWithTag ("White");

		foreach (GameObject whiteTile in whiteTileArray) {

			greenTileList.Add (whiteTile);

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
		t = 0f;
		hubrisDelay = true;
		if (builtTileList.Count > 0) {

		if (goldenAge) {

				hubrisAmount += builtTileList.Count * 2;

			} else {

				hubrisAmount += builtTileList.Count * 1;
			}
		}
		hubrisCounter.text = (hubrisAmount.ToString ());
		yield return new WaitForSeconds(7f);
		hubrisDelay = false;

		}

	// Time counter

	private IEnumerator AddDay() {
		dayDelay = true;
		yield return new WaitForSeconds(15f);
		dayDelay = false;
		_season++;

		_day++;
		GetComponent<NaturalDisasters>().difficultyCounter++;

		if (!goldenAge) {
		goldenAgeCounter++;
		}

		if (goldenAgeCounter == 7 && !goldenAge) {
			goldenAge = true;
			goldenAgeCounter = 0;
			gaT = 0f;
			StartCoroutine (GoldenAges());
		}


		dayTimer.text = "Year: " + _day;


	}
}
