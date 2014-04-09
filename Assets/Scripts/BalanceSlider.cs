using UnityEngine;
using System.Collections;

public class BalanceSlider : MonoBehaviour { 

	// These variables are used for the player's health and health bar
	public float currentHealth=100;
	public float maxHealth=100;
	public float maxBAR=100;
	public float HealthBarLength;

	void OnGUI()
	{
		// This code creates the health bar at the coordinates 10,10
		GUI.color = Color.green;
		GUI.Box(new Rect(10,10,HealthBarLength,25), "City");
		// This code determines the length of the health bar
		HealthBarLength=currentHealth*maxBAR/maxHealth;
	}
	
	void ChangeHP(float Change)
	{
		// This line will take whatever value is passed to this function and add it to curHP.
		currentHealth+=Change;
		
		// This if statement ensures that we don't go over the max health
		if(currentHealth>maxHealth)
		{
			currentHealth=100;
		}
		
		// This if statement is to check if the player has died
		if(currentHealth<=0)
		{
			// Die
			Debug.Log("Player has died!");
		}
	}
	
	// This function checks if the player has entered a trigger
	void OnTriggerEnter(Collider other)
	{

		if (other.tag == "Player") {
			ChangeHP(25);
		}
		else if (other.tag == "GameController") {
			ChangeHP(-25);
		}

		// Finally, this line destroys the gameObject the player collided with.
		Destroy(other.gameObject);
	}
}