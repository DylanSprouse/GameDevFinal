using UnityEngine;
using System.Collections;

public class BalanceSliderAutomatic1 : MonoBehaviour { 
	
	// These variables are used for the player's health and health bar
	public float currentHealth=100;
	public float maxHealth=100;
	public float minHealth = 0;
	public float maxBAR=100;
	public float HealthBarLength;
	
	public GameObject cityPrefab;
	GameObject cityPrefabClone;
	
	public float DestroyTime = 5f;
	
	public bool clicked = true;
	
	void OnGUI()
	{
		// This code creates the health bar at the coordinates 10,10
		GUI.color = Color.green;
		GUI.Box(new Rect(10,10,HealthBarLength,25), "City");
		// This code determines the length of the health bar
		HealthBarLength=currentHealth*maxBAR/maxHealth;
	}
	
	void ChangeHPCity(float Change)
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
	
	void Update () {
		
		if (Input.GetMouseButton (1)) {
			Debug.Log ("Pressed left click.");
			cityPrefabClone = Instantiate (cityPrefab, new Vector3 (Random.Range (-10f, 10f), 0f, Random.Range (-10f, 10f)), Quaternion.identity) as GameObject;
			ChangeHPCity (1);
		} 

		else if (Input.GetMouseButton (0)) {
			//GameObject.Destroy(cityPrefabClone);
			Destroy(cityPrefabClone);
			//StartCoroutine("DestroyMeForest");
			ChangeHPCity(-1);
		}
	}
	
	IEnumerator DestroyMeCity()
	{
		yield return new WaitForSeconds(DestroyTime);
		Destroy(cityPrefabClone);
		//Destroy(gameObject);
	}
}