using UnityEngine;
using System.Collections;

public class DylanGenerator : MonoBehaviour {

	//references to our blueprints
	public Transform pinetreeprefab, hollyprefab, pinesproutprefab;

	//public Transform epicenterprefab;

	public int pulsetime = 1;

	public int spawnDistance = 10;

	int count = 0;

	//int distance = 0;

	private bool PulseDelay = false;

	// Use this for initialization
	void Start () {

		StartCoroutine (ForestPulse() );
	
	}

	IEnumerator ForestPulse() {

		yield return new WaitForSeconds ( pulsetime ) ;
		PulseDelay = false;

		if (count < 100){
					

			Instantiate ( pinetreeprefab, new Vector3( Random.Range ( - spawnDistance, spawnDistance) , 0f,  Random.Range (- spawnDistance,spawnDistance)  ), Quaternion.identity );
					
						
			Instantiate ( hollyprefab, new Vector3( Random.Range ( - spawnDistance, spawnDistance) , 0f,  Random.Range (- spawnDistance,spawnDistance)  ), Quaternion.identity );
							
			
			Instantiate ( pinesproutprefab, new Vector3( Random.Range ( - spawnDistance, spawnDistance) , 0f,  Random.Range (- spawnDistance,spawnDistance)  ), Quaternion.identity );

									
						
		}
		}



	// Update is called once per frame
	void Update () {

		if (PulseDelay == false){

			StartCoroutine (ForestPulse() );
			PulseDelay = true ;
		}


		if (count < 500){
//		int randomnumber = Random.Range (0, 101);
//
//		if (randomnumber < 35) {
//
//			//spawn a skyscraper
//			Instantiate ( pinetreeprefab, new Vector3( Random.Range ( -100f, 100f ), 0f, Random.Range ( -100f,100f ) ), Quaternion.identity );
//		}
//		else if (randomnumber < 50 && randomnumber > 35){
//
//			//spawn a shopping mall
//			Instantiate ( hollyprefab, new Vector3( Random.Range ( -100f, 100f ), 0f, Random.Range ( -100f, 100f ) ), Quaternion.identity );
//		}
//		else if (randomnumber < 75 && randomnumber > 50) {
//
//				Instantiate ( pinesproutprefab, new Vector3( Random.Range ( -100f, 100f ), 0f, Random.Range ( -100f, 100f ) ), Quaternion.identity );
//
//		}
//		else if (randomnumber < 85 && randomnumber > 75) {
//
//				Instantiate ( pinesproutprefab, new Vector3( Random.Range ( -100f, 100f ), 0f, Random.Range ( -100f, 100f ) ), Quaternion.identity );
//
//		}
//		else if (randomnumber < 95 && randomnumber > 85) {
//				
//				Instantiate ( snowmanprefab, new Vector3( Random.Range ( -100f, 100f ), 0f, Random.Range ( -100f, 100f ) ), Quaternion.identity );
//				
//		}
//		else if (randomnumber < 100 && randomnumber > 95) {
//				
//				Instantiate ( logcabinprefab, new Vector3( Random.Range ( -100f, 100f ), 0f, Random.Range ( -100f, 100f ) ), Quaternion.identity );
//				
//		}
//	}
}
}
}
