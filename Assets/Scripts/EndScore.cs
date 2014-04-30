using UnityEngine;
using System.Collections;

public class EndScore : MonoBehaviour {

	public MouseController mouse;
	public GUIText endScore;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		endScore.text = "Score: " + (mouse._day + mouse.hubrisAmount);

	}
}
