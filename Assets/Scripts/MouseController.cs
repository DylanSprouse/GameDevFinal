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
	public GameObject costBoard, costBoardHighlight;
	public GameObject selectedHexagon;
	public GameObject selectedCity;
	public GameObject hubrisAddGUI;
	public TextMesh hubrisTickGUI, hubrisTickGUIShadow;
	public GameObject gaTickGUI;
	public Light directionalLight;

	public TextMesh hubrisCounter;
	public TextMesh costBoardText;
	public TextMesh gameOver, gameOverHubrisTitle, gameOverHubrisScore;
	public GameObject gameOverBox1, gameOverBox2, gameOverBox3;
	public GameObject GABox1;
	public GameObject ResetBox;
	public TextMesh ResetTextMesh;
	public TextMesh goldenAgeText;

	public bool firstCityBuilt = false;
	public bool goldenAge = false;

	public float cityDecayRate = 0.001f;
	public float cityExpansionRate = 0.05f;
	public float tornadoDecay = 0.5f;
	public float earthquakeDecay = 0.01f;

	public AudioSource buildSound, screamingSound, goldenAgeSound;

	public int goldenAgeCounter = 0;
	public int hubrisAmount = 50;
	public int _day = 1;
	private int _season = 0;
	private GameObject highlitHexagon;

	private float t = 0f;
	private float tickDuration = 7f;
	private float timer = 1f;

	public float gaT = 0f;
	private float gaDelayDuration = 75f;
	private float gaTimer = 1f;

	private bool hubrisDelay = false, dayDelay = false;
	public bool gameIsOver = false;
	private bool lockPlacement = false;
	private bool mouseButtonHeld;

	void Awake() {

		if (Instance != null && Instance != this) {

			Destroy(gameObject);

		}

		Instance = this;

		//DontDestroyOnLoad(this.gameObject);



	}

	void Start () {


		Time.timeScale = 1f;
		gameOver.renderer.enabled = false;
		gameOverHubrisTitle.renderer.enabled = false;
		gameOverHubrisScore.renderer.enabled = false;
		gameOverBox1.renderer.enabled = false;
		gameOverBox2.renderer.enabled = false;
		gameOverBox3.renderer.enabled = false;

		ResetBox.renderer.enabled = false;
		foreach (Transform resetChild in ResetBox.transform) {
			
			resetChild.renderer.enabled = false;
		}

		GABox1.renderer.enabled = false;
		foreach (Transform gaChild in GABox1.transform) {

			gaChild.renderer.enabled = false;
		}
		goldenAgeText.renderer.enabled = false;

		_volcanoes = new List<GameObject>();
		_mountains = new List<GameObject>();

		greenTileList = new List<GameObject>();
		builtTileList = new List<GameObject>();

		StartCoroutine(AddTiles());

		hubrisCounter.text = (hubrisAmount.ToString ());

		costBoardHighlight.renderer.enabled = false;
		costBoard.renderer.enabled = false;
		costBoardText.renderer.enabled = false;

	}

	void Update () {

		mouseButtonHeld = (Input.GetMouseButton (0)) == true? true : false;

		costBoardHighlight.renderer.enabled = false;
		costBoard.renderer.enabled = false;
		costBoardText.renderer.enabled = false;
		
		Ray mouseRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit mouseHit = new RaycastHit();


			hubrisAddGUI.renderer.material.SetFloat ("_Cutoff", Mathf.Lerp (timer* 1f, timer * 0f, t));
			if (t < 1) {
			
			t += Time.deltaTime/tickDuration;
				}
		
		if (firstCityBuilt) {
		gaTickGUI.renderer.material.SetFloat ("_Cutoff", Mathf.Lerp (gaTimer* 1f, gaTimer * 0f, gaT));
			if (gaT < 1  && !goldenAge) {
			
			gaT += Time.deltaTime/gaDelayDuration;
		}
		}
		if (goldenAge) {

			hubrisTickGUI.text = "+" + builtTileList.Count * 2;
			hubrisTickGUIShadow.text = "+" + builtTileList.Count * 2;


		} else {

			hubrisTickGUI.text = "+" + builtTileList.Count;
			hubrisTickGUIShadow.text = "+" + builtTileList.Count;

		}


		if (!dayDelay && !gameIsOver && firstCityBuilt) {
			
			StartCoroutine (AddDay());
			
		}

		if (Physics.Raycast (mouseRay, out mouseHit, 1000f)) {

			highlitHexagon = mouseHit.collider.gameObject;
			costBoard.renderer.enabled = true;
			costBoardHighlight.renderer.enabled = true;
			costBoard.transform.position = new Vector3 (highlitHexagon.transform.position.x,
			                                            highlitHexagon.transform.position.y + 1f,
			                                            highlitHexagon.transform.position.z);
			costBoardText.renderer.enabled = true;

			if (highlitHexagon.gameObject.tag == "Green" && !builtTileList.Contains (highlitHexagon)) {
			
			costBoardText.text = "10";

			} else if (highlitHexagon.gameObject.tag == "Yellow" && !builtTileList.Contains (highlitHexagon)) {

				costBoardText.text = "12";

			} else if (highlitHexagon.gameObject.tag == "White" && !builtTileList.Contains (highlitHexagon)) {
				
				costBoardText.text = "8";

			} else {

				costBoardHighlight.renderer.enabled = false;
				costBoard.renderer.enabled = false;
				costBoardText.renderer.enabled = false;
			}


		
		}
		
		
		if (Physics.Raycast (mouseRay, out mouseHit, 1000f) && mouseButtonHeld && !PauseMenu.paused) {

			// Mouse building placement

			costBoardHighlight.renderer.enabled = false;
			costBoard.renderer.enabled = false;
			costBoardText.renderer.enabled = false;

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
				Instantiate (Resources.Load ("gui_costCounter10", typeof (GameObject)), new Vector3 (instance.transform.position.x,
					                                                                                   instance.transform.position.y + 2f,
					                                                                                   instance.transform.position.z),
					             Quaternion.identity);	
				instance.transform.localScale = new Vector3 (1.5f, 1.5f, 1.5f);
				selectedCity = instance.gameObject;
				selectedCity.transform.parent = selectedHexagon.transform;

				}

				// Instantiate desert monument

				if (!builtTileList.Contains (mouseHit.collider.gameObject) && hubrisAmount >= 12 
				    && !lockPlacement && mouseHit.collider.gameObject.tag == "Yellow") {
					
					lockPlacement = true;
					
					if (!firstCityBuilt) {
						
						firstCityBuilt = true;
						
					}
					
					hubrisAmount -= 12;
					hubrisCounter.text = (hubrisAmount.ToString ());
					buildSound.Play ();
					
					builtTileList.Add (selectedHexagon);
					
					GameObject instance = Instantiate (Resources.Load ("Lighthouse", typeof (GameObject)), new Vector3 (selectedHexagon.transform.position.x,
					                                                                                                 selectedHexagon.transform.position.y + 0.2f,
					                                                                                                 selectedHexagon.transform.position.z),
					                                   
					                                   Quaternion.identity) as GameObject;
					Instantiate (Resources.Load ("gui_costCounter12", typeof (GameObject)), new Vector3 (instance.transform.position.x,
					                                                                                   instance.transform.position.y + 2f,
					                                                                                   instance.transform.position.z),
					             Quaternion.identity);
					instance.transform.localScale = new Vector3 (3.5f, 3.5f, 3.5f);
					selectedCity = instance.gameObject;
					selectedCity.transform.parent = selectedHexagon.transform;
					
				}

				// Instantiate tundra monument

				if (!builtTileList.Contains (mouseHit.collider.gameObject) && hubrisAmount >= 8 
				    && !lockPlacement && mouseHit.collider.gameObject.tag == "White") {
					
					lockPlacement = true;
					
					if (!firstCityBuilt) {
						
						firstCityBuilt = true;
						
					}
					
					hubrisAmount -= 8;
					hubrisCounter.text = (hubrisAmount.ToString ());
					buildSound.Play ();
					
					builtTileList.Add (selectedHexagon);
					
					GameObject instance = Instantiate (Resources.Load ("Castle", typeof (GameObject)), new Vector3 (selectedHexagon.transform.position.x,
					                                                                                                 selectedHexagon.transform.position.y + 0.2f,
					                                                                                                 selectedHexagon.transform.position.z),
					                                   
					                                   Quaternion.identity) as GameObject;

					Instantiate (Resources.Load ("gui_costCounter8", typeof (GameObject)), new Vector3 (instance.transform.position.x,
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

						Instantiate (Resources.Load ("ruins_1", typeof (GameObject)), new Vector3 (builtTileList[i].transform.position.x,
						                                                                                    builtTileList[i].transform.position.y + 0.2f,
						                                                                                    builtTileList[i].transform.position.z),
						             	Quaternion.identity);
							Destroy (child.gameObject);
							builtTileList.Remove (builtTileList [i]);
							if (!screamingSound.isPlaying) {
							screamingSound.Play ();
							}
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
			Time.timeScale = 0f;
			gameOver.renderer.enabled = true;
			gameOverHubrisTitle.renderer.enabled = true;
			gameOverHubrisScore.renderer.enabled = true;
			gameOverHubrisScore.text = (hubrisAmount.ToString());
			gameOverBox1.renderer.enabled = true;
			gameOverBox2.renderer.enabled = true;
			gameOverBox3.renderer.enabled = true;
			ResetBox.renderer.enabled = true;

			foreach (Transform resetChild in ResetBox.transform) {

				resetChild.renderer.enabled = true;
			}




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
		goldenAgeSound.Play ();
		cityExpansionRate = 0.1f;
		goldenAgeText.renderer.enabled = true;
		GABox1.renderer.enabled = true;
		foreach (Transform gaChild in GABox1.transform) {
			
			gaChild.renderer.enabled = true;
		}

		yield return new WaitForSeconds(20f);

		goldenAgeText.renderer.enabled = false;
		goldenAge = false;
		cityExpansionRate = 0.05f;
		GABox1.renderer.enabled = false;
		foreach (Transform gaChild in GABox1.transform) {
			
			gaChild.renderer.enabled = false;
		}


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

		if (goldenAgeCounter == 5 && !goldenAge) {
			goldenAge = true;
			goldenAgeCounter = 0;
			gaT = 0f;
			StartCoroutine (GoldenAges());
		}


	}
}
