using UnityEngine;
using System.Collections;

public class TileHighlighting : MonoBehaviour {

	public Material shaderMat;

	void OnMouseEnter() {
		
		renderer.material.shader = Shader.Find ("Self-Illumin/Diffuse") ;
		
	}
	
	void OnMouseExit(){
		
		renderer.material.shader = Shader.Find ("Diffuse") ;
		
	}

}