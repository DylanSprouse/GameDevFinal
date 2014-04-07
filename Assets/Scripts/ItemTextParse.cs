using UnityEngine;
using System.Collections;

public class ItemTextParse : MonoBehaviour {

	public TextAsset textFile;
	public Transform itemPrefab;

	// Use this for initialization
	void Start () {

		StartCoroutine (ParseAndGenerate() );
	}

	IEnumerator ParseAndGenerate() {

		Debug.Log ( textFile.text);
		yield return 0;

		//clean up platform dependant line breaks
		string cleanedTextData = textFile.text.Replace ("\r", "");
		//split up the text data into different lines
		string[] lines = cleanedTextData.Split ( "\n" [0] );

		//for each line, split it along the commas, and parse it
		foreach (string line in lines) {

			var newItem = Instantiate ( itemPrefab, Random.insideUnitSphere * 10f, Quaternion.identity) as Transform;

			string[] data = line.Split ( "," [0] ); //split each line along the commas
			newItem.name = data[0];
			newItem.transform.localScale = new Vector3 ( 1f, float.Parse (data[1]), 1f ); //sets the value of our item

			yield return 0; //we could wait a frame to make sure theres no framerate problems.

		}
	}
	                              



	// Update is called once per frame
	void Update () {
	
	}
}
