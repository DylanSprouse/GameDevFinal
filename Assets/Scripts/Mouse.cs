using UnityEngine;
using System.Collections;

public class Mouse : MonoBehaviour {
	
	public GameObject hexagonPrefab;
	GameObject clone;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown (0)) {
			Debug.Log ("Pressed left click.");
			clone = Instantiate (hexagonPrefab, new Vector3( Random.Range (-10f, 10f), 0f, Random.Range (-10f, 10f) ), Quaternion.identity ) as GameObject;
		}
		
		if (Input.GetMouseButtonDown (1)) {
			Debug.Log ("Pressed right click.");

			Ray ray = Camera.main.ScreenPointToRay ( Input.mousePosition );
			RaycastHit rayHit = new RaycastHit(); // a blank container to hold forensics info
			
			if ( Physics.Raycast (ray, out rayHit, 1000f ) )
			{
				Destroy(clone);
			}
			//Destroy(hexagonPrefab.gameObject);
		}
	}
}