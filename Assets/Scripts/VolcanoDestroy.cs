using UnityEngine;
using System.Collections;

public class VolcanoDestroy : MonoBehaviour {


	public Vector3 temp;
	// Use this for initialization
	void Start () {
	
		for (int i = 0; i < MouseController.Instance.greenTileList.Count; i++) {

			temp = new Vector3 (MouseController.Instance.greenTileList[i].transform.position.x,
			MouseController.Instance.greenTileList[i].transform.position.y,
			        MouseController.Instance.greenTileList[i].transform.position.z);

			if (Vector3.Distance (MouseController.Instance.greenTileList[i].transform.position, transform.position) < 4f) {

				GameObject instance = Instantiate (Resources.Load ("blackTile_1", typeof (GameObject)), new Vector3 (MouseController.Instance.greenTileList[i].transform.position.x,
				                                                                                                     MouseController.Instance.greenTileList[i].transform.position.y,
				                                                                                                     MouseController.Instance.greenTileList[i].transform.position.z),
				                                   
				                                   Quaternion.identity) as GameObject;

				Destroy (MouseController.Instance.greenTileList[i]);
				MouseController.Instance.greenTileList.Remove (MouseController.Instance.greenTileList[i]);
				for(int j=0;j<MouseController.Instance.builtTileList.Count;j++){
					if(MouseController.Instance.builtTileList[j].transform.position == temp){
						Instantiate (Resources.Load ("ruins_1", typeof (GameObject)), new Vector3 (MouseController.Instance.builtTileList[j].transform.position.x,
						                                                                           MouseController.Instance.builtTileList[j].transform.position.y + 0.2f,
						                                                                           MouseController.Instance.builtTileList[j].transform.position.z),
						             Quaternion.identity);
						MouseController.Instance.goldenAgeCounter = 0;
						MouseController.Instance.gaT = 0f;
						MouseController.Instance.screamingSound.Play ();
						MouseController.Instance.builtTileList.Remove (MouseController.Instance.builtTileList[j]);
					}
				}



			}

		}


	}
}
