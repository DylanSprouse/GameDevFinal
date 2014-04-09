using UnityEngine;
using System.Collections;

public class Mouse : MonoBehaviour {
	
	public GameObject hexagonPrefab;
	GameObject clone;

	private Ray ray; // The ray
	private RaycastHit rayHit; // What we hit
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown (0)) {
			Debug.Log ("Pressed left click.");
			clone = Instantiate (hexagonPrefab, new Vector3( Random.Range (-10f, 10f), 0f, Random.Range (-10f, 10f) ), Quaternion.identity ) as GameObject;
		}

		ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Ray will be sent out from where your mouse is located 
		if(Physics.Raycast(ray,out rayHit, 1000.0f) && Input.GetMouseButtonDown (1)) // On left click we send down a ray
		{
			//Destroy (clone.gameObject);
			Destroy (rayHit.collider.gameObject); // Destroy what we hit
		}

		//if (Input.GetMouseButtonDown (1)) {
//			Debug.Log ("Pressed right click.");
//
//			Ray ray = Camera.main.ScreenPointToRay ( Input.mousePosition );
//			RaycastHit rayHit = new RaycastHit(); // a blank container to hold forensics info
//			
//			if ( Physics.Raycast (ray, out rayHit, 1000f ) && Input.GetMouseButtonDown (1) )
//			{
//				Destroy(rayHit.collider.clone);
//			}
			//Destroy(hexagonPrefab.gameObject);
		//}
	}
}