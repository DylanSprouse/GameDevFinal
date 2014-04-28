using UnityEngine;
using System.Collections;

public class TileHighlighting : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void OnMouseEnter() {
		
		renderer.material.shader = Shader.Find ("Self-Illumin/Outlined Diffuse")  ;
		
	}
	
	void OnMouseExit(){
		
		renderer.material.shader = Shader.Find ("Diffuse") ;
		
	}

	// Update is called once per frame
	void Update () {
	
	}
}
