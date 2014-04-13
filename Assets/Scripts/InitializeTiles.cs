using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InitializeTiles : MonoBehaviour {
	
	public List<GameObject> tileList = new List<GameObject>();
	public Transform greenTile, blueTile, mountain;
	public int greenTileChance = 5, mountainChance = 7;
	
	
	// Use this for initialization
	void Start () {
		GameObject[] hexagons;
		hexagons = GameObject.FindGameObjectsWithTag ("Hexagon");
		foreach (GameObject current in hexagons) {
			Vector3 hexPos = new Vector3(current.transform.position.x, current.transform.position.y, current.transform.position.z);
			int randomNumber = Random.Range(0,10);
			if (randomNumber <= greenTileChance){
				Instantiate(greenTile, hexPos, Quaternion.identity);
			}
			else if (randomNumber > greenTileChance && randomNumber <= mountainChance){
				Instantiate(blueTile, hexPos, Quaternion.identity);
			}
			else if (randomNumber > mountainChance){
				Instantiate(mountain, hexPos, Quaternion.identity);
			}
			GameObject.Destroy(current);
		}
	}
	// Update is called once per frame
	void Update () {
	}
}