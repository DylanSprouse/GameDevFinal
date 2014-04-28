using UnityEngine;
using System.Collections;

public class NaturalDisasters : MonoBehaviour {
	
	public int disasterChance;
	public int disasterSeason;
	public int difficultyCounter = 0;
	public bool disasterActive = false;
	
	private float disasterFrequency = 8f;
	
	
	void Update() {
		
		if (!disasterActive) {
			DifficultyRamping ();
			StartCoroutine(SetSeason());
			
		}
		
	}
	
	void DifficultyRamping() {
		
		if (difficultyCounter == 7 && disasterFrequency >= 1f) {
			
			difficultyCounter = 0;
			disasterFrequency -= 0.25f;
			
		}
		
		
	}
	
	private IEnumerator SetSeason() {
		disasterActive = true;
		yield return new WaitForSeconds(disasterFrequency);
		disasterSeason = Random.Range (0, 3);
		//tornado season chances
		if (disasterSeason == 0) {
			DisasterSelection (3, 5, 7, 9);
		} 
		//flood season chances
		else if (disasterSeason == 1) {
			DisasterSelection(1,5,7,9);
		}
		//volcano season chances
		else if (disasterSeason == 2) {
			DisasterSelection(1,3,5,9);
		}
		disasterActive = false;
		
		
	}
	
	void DisasterSelection (int tornadoChance, int floodChance, int quakeChance, int volcanoChance){
		disasterChance = Random.Range (0,10);
		Debug.Log (disasterChance);
		if (disasterChance <= tornadoChance) {
			
			GetComponent<TornadoGenerator>().tornadoModel.renderer.enabled = true;
			GetComponent<TornadoGenerator>().tornadoEnabled = true;
			
		} else if (disasterChance <= floodChance) {
			
			GetComponent<FloodGenerator>().floodEnabled = true;
			
		} else if (disasterChance <= quakeChance) {
			
			
			GetComponent<EarthquakeGenerator>().earthquakeEnabled = true;
			
		} else if (disasterChance <= volcanoChance) {
			
			
			GetComponent<VolcanoGenerator>().volcanoEnabled = true;
		}
	}
	
}