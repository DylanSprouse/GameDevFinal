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
			yield return new WaitForSeconds (1f/(tileList.Count+1));
			float hexPos = Random.Range (0, 5);
				if (hexPos <1){
					position = new Vector3 (position.x, position.y, position.z + 3);
					if (checkIfEmpty (position) == true) {
						tileList.Add (tileMaker (position));
					}
				}
				else if (hexPos <2){
					position = new Vector3 (position.x + 2.5f, position.y, position.z + 1.5f);
					if (checkIfEmpty (position) == true) {
						tileList.Add (tileMaker (position));
					}
				}
				else if (hexPos <3){
					position = new Vector3 (position.x + 2.5f, position.y, position.z - 1.5f);
					if (checkIfEmpty (position) == true) {
						tileList.Add (tileMaker (position));
					}
				}
				else if (hexPos <4){
					position = new Vector3 (position.x, position.y, position.z - 3);
					if (checkIfEmpty (position) == true) {
						tileList.Add (tileMaker (position));
					}
				}
				else if (hexPos <5){
					position = new Vector3 (position.x - 2.5f, position.y, position.z - 1.5f);
					if (checkIfEmpty (position) == true) {
						tileList.Add (tileMaker (position));
					}
				}
				else if (hexPos <=6){
					position = new Vector3 (position.x - 2.5f, position.y, position.z + 1.5f);
					if (checkIfEmpty (position) == true) {
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

