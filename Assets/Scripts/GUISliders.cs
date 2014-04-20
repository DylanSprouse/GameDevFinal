using UnityEngine;
using System.Collections;

public class GUISliders : MonoBehaviour { 
	
	// These variables are used for the player's health and health bar
	public float currentHealth=100;
	public float maxHealth=100;
	public float minHealth = 0;
	public float maxBAR=100;
	public float HealthBarLength;
	public bool sliderUpdate = false;
	
	public bool clicked = true;
	
	void Start() {

		StartCoroutine (SetSliderLength());


	}
	
	void OnGUI()
	{
		// This code creates the health bar at the coordinates 10,10
		GUI.color = Color.yellow;
		GUI.Box(new Rect(10, 10,HealthBarLength,25), "Monuments");
		// This code determines the length of the health bar
		HealthBarLength=currentHealth*maxBAR/maxHealth;
	}
	
	void ChangeHPForest(float Change)
	{
		// This line will take whatever value is passed to this function and add it to curHP.
		currentHealth+=Change;
		
		// This if statement ensures that we don't go over the max health
		if(currentHealth>maxHealth)
		{
			currentHealth=100;
		}
		
		// This if statement ensures that we don't go over the min health
		if(currentHealth<minHealth)
		{
			currentHealth=0;
		}
		
		// This if statement is to check if the player has died
		if(currentHealth<=0)//less than 25%
		{
			// Die
			Debug.Log("Player has died!");
		}
	}

	void Update() {

		if (!sliderUpdate) {

		StartCoroutine(SetSliderLength());

		}
	}

	private IEnumerator SetSliderLength() {
		sliderUpdate = true;
		yield return new WaitForSeconds(1f);
		HealthBarLength = ((GetComponent<MouseController>().builtTileList.Count) * 100) / GetComponent<MouseController>().greenTileList.Count;
		sliderUpdate = false;
	}
}