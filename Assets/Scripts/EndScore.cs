using UnityEngine;
using System.Collections;

public class EndScore : MonoBehaviour {

	//private MouseController mouse;
	public GUIText endScore;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		endScore.text = "Score: " + (MouseController._day * (MouseController.hubrisAmount)/10);
		//endScore.text = "Score: " + (mouse.dayTimer.text + mouse.hubrisCounter.text);

	}
}
