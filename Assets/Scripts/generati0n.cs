using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class generati0n : MonoBehaviour {

	public Transform tileObject;
	public List<Transform> tileList = new List<Transform>();
	public int xMax = 10;
	public int xMin = -10;
	public int zMax = 10;
	public int zMin = -10;
	Vector3 position = new Vector3(1, 0, 1);

	// Use this for initialization
	void Start () {
		StartCoroutine (tileSpawning ());
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log ("Position = " + position);


	
	}

	Transform tileMaker(Vector3 pos){
		return Instantiate (tileObject, pos, Quaternion.identity) as Transform;
		}
	
	public bool checkIfEmpty(Vector3 pos){
		foreach (Transform current in tileList) {
			if (current.position == pos){
				return false;
			}
				}
		return true;
	}

	IEnumerator tileSpawning(){
		while (true) {
						yield return new WaitForSeconds (.5f);
						float randomNumber = Random.Range (0f, 100f);
						if (0f <= randomNumber && randomNumber <= 24f) {
								position = new Vector3 (position.x + 1, position.y, position.z);
								if (checkIfEmpty (position) == true) {
										Debug.Log ("space empty");
										tileList.Add (tileMaker (position));
								}
						} else if (25f <= randomNumber && randomNumber <= 49f) {
								position = new Vector3 (position.x - 1, position.y, position.z);
								if (checkIfEmpty (position) == true) {
										Debug.Log ("space empty");
										tileList.Add (tileMaker (position));
								}
						} else if (50f <= randomNumber && randomNumber <= 74f) {
								position = new Vector3 (position.x, position.y, position.z + 1);
								if (checkIfEmpty (position) == true) {
										Debug.Log ("space empty");
										tileList.Add (tileMaker (position));
								}
						} else if (75f <= randomNumber && randomNumber <= 100f) {
								position = new Vector3 (position.x, position.y, position.z - 1);
								if (checkIfEmpty (position) == true) {
										Debug.Log ("space empty");
										tileList.Add (tileMaker (position));
								}
						}

				}
	}
//	public bool checkIfAdjacent(int x, int z){
//		foreach (Transform current in tileList) {
//						if (current.position == new Vector3 (x + 1, 0f, z)) {
//								return true;
//						} else if (current.position == new Vector3 (x - 1, 0f, z)) {
//								return true;
//						} else if (current.position == new Vector3 (x, 0f, z + 1)) {
//								return true;
//						} else if (current.position == new Vector3 (x, 0f, z - 1)) {
//								return true;
//						}
//				}
//				return false;
//
//	}
}

