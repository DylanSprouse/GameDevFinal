using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InitializeTiles : MonoBehaviour {
	
	public List<GameObject> tileList = new List<GameObject>();
	public Transform greenTile, blueTile, mountain, snow, desert;
	static int greenTileChance = 6, specialTileChance = 4;
	
	
	// Use this for initialization
	void Start () {
		GameObject[] hexagons;
		hexagons = GameObject.FindGameObjectsWithTag ("Hexagon");
		foreach (GameObject current in hexagons) {
			bool specialSpawn = false;
			Vector3 hexPos = new Vector3(current.transform.position.x, current.transform.position.y, current.transform.position.z);
			if (current.transform.position.x < 5) {
				int randomNumber = Random.Range (0, 10);
				if (randomNumber <= specialTileChance) {
					GameObject.Destroy(current);
					Instantiate (snow, hexPos, Quaternion.identity);
					specialSpawn = true;
				}
			}
			else if (current.transform.position.x > 6) {
				int randomNumber = Random.Range (0, 10);
				if (randomNumber <= specialTileChance) {
					GameObject.Destroy(current);
					Instantiate (desert, hexPos, Quaternion.identity);
					specialSpawn = true;
				}
			}
			if (specialSpawn == false){
				int randomNumber = Random.Range(0,10);
				if (randomNumber <= greenTileChance){
					Instantiate(greenTile, hexPos, Quaternion.identity);
				}
				else if (randomNumber > greenTileChance){
					
					Instantiate(mountain, hexPos, Quaternion.identity);
				}
				GameObject.Destroy(current);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	}
}
