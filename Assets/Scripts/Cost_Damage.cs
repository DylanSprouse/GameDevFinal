using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Cost_Damage : MonoBehaviour {

	Ray ray;
	RaycastHit hit;
	public int number = 0;

	//private GUIText GUIDamage;
	//public GameObject GUIPrefab;
	bool shown = false;
	public float waitTime = 0.01f;
	public List<GameObject> builtTileList;
	Vector3 boxPosition;
	public int hubrisAmount = 50;
	private bool lockPlacement = false;

	
	void Start () {
		// Initialise ray
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		// Print out the current number value to the console window
		Debug.Log("Number is currently: " + number); 
	}
	
	void Update () {
		Selection();
	}
	
	void Selection() {
		// Use Input.GetKeyDown() for single clicks
		if(Input.GetMouseButton (0) || Input.GetMouseButton (1)) 
		{
			// Reset ray with new mouse position
			ray = Camera.main.ScreenPointToRay(Input.mousePosition); 

			if(Physics.Raycast(ray, out hit)) {

				if(hit.transform.tag == "Green") {
					OnGUI();
					///int randomHex = Random.Range (0, GetComponent<MouseController>().greenTileList.Count);
					///GameObject current = GetComponent<MouseController>().greenTileList[randomHex];
					///	Vector3 hexPos = new Vector3(current.transform.position.x, current.transform.position.y + 10, current.transform.position.z);
					///	GUIDamage = ((GameObject)Instantiate(GUIPrefab, hexPos, Quaternion.identity)).GetComponent<GUIText>();
					//GUIDamage = Instantiate(GUIPrefab, hexPos, Quaternion.identity) as GameObject;
					//Destroy(GUIDamage, 2f);

					///GameObject damageHubris = new GameObject("SomeGUIText");
					///Instantiate(damageHubris);
					///GUIText myText = damageHubris.AddComponent<GUIText>();
					//myText.transform.position = new Vector3(0.5f,0.5f,0f);
					///myText.transform.position = new Vector3(current.transform.position.x, current.transform.position.y+5, current.transform.position.z);
					///myText.guiText.text = "-4";

					number = 2;
				}

				if(hit.transform.tag == "Yellow") {
					OnGUI();
					number = 3;
				}

				if(hit.transform.tag == "White") {
					number = 4;
					OnGUI();
				}

				// Reset number to zero if no object selected
				if (hit.transform.tag == "Untagged") {
					number = 0;
				}

				Debug.Log("Number is currently: " + number);
			}
		}
	}

	void OnGUI () {
		if (!builtTileList.Contains (hit.collider.gameObject) && hubrisAmount >= 5 
		    && !lockPlacement && hit.collider.gameObject.tag == "Green") {
			if (hit.transform.tag == "Green" && Input.GetMouseButton (0)) {// && builtTileList.Count > 0)
				shown = true;
			} else if (shown) {
				StartCoroutine (DisapearBoxAfter ()); 
			}
			if (shown) {
				//GUI.Box(new Rect((Screen.width/2)-200,0,40,30) , "+4");
				boxPosition.x = Input.mousePosition.x;
				boxPosition.y = Screen.height - Input.mousePosition.y;
				GUI.Box (new Rect (boxPosition.x, boxPosition.y, 40, 30), "-5");
			}
		}
		if (!builtTileList.Contains (hit.collider.gameObject) && hubrisAmount >= 5 
		    && !lockPlacement && hit.collider.gameObject.tag == "Yellow") {
			if (hit.transform.tag == "Yellow" && Input.GetMouseButton (0)) {// && builtTileList.Count > 0)
				shown = true;
			} else if (shown) {
				StartCoroutine (DisapearBoxAfter ()); 
			}
			if (shown) {
				//GUI.Box(new Rect((Screen.width/2)-200,0,40,30) , "+4");
				boxPosition.x = Input.mousePosition.x;
				boxPosition.y = Screen.height - Input.mousePosition.y;
				GUI.Box (new Rect (boxPosition.x, boxPosition.y, 40, 30), "-7");
			}
		}
		if (!builtTileList.Contains (hit.collider.gameObject) && hubrisAmount >= 5 
		    && !lockPlacement && hit.collider.gameObject.tag == "White") {
			if (hit.transform.tag == "White" && Input.GetMouseButton (0)) {// && builtTileList.Count > 0)
				shown = true;
			} else if (shown) {
				StartCoroutine (DisapearBoxAfter ()); 
			}
			if (shown) {
				//GUI.Box(new Rect((Screen.width/2)-200,0,40,30) , "+4");
				boxPosition.x = Input.mousePosition.x;
				boxPosition.y = Screen.height - Input.mousePosition.y;
				GUI.Box (new Rect (boxPosition.x, boxPosition.y, 40, 30), "-3");
			}
		}
	}

	IEnumerator DisapearBoxAfter() {
		// suspend execution for waitTime seconds
		Debug.Log("Before Waiting 2 seconds");
		yield return new WaitForSeconds(waitTime);
		Debug.Log("After Waiting 2 Seconds");
		shown = false;
	}
}