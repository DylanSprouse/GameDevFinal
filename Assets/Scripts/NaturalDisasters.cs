using UnityEngine;
using System.Collections;

public class NaturalDisasters : MonoBehaviour {

	public int disasterChance;
	public int difficultyCounter = 0;
	public bool disasterActive = false;

	private float disasterFrequency = 8f;


	void Update() {

		if (!disasterActive) {
			DifficultyRamping ();
			StartCoroutine(SetDisaster());
			
		}

	}

	void DifficultyRamping() {

		if (difficultyCounter == 7 && disasterFrequency >= 1f) {

			difficultyCounter = 0;
			disasterFrequency -= 0.25f;

		}


	}

	private IEnumerator SetDisaster() {
		disasterActive = true;
		yield return new WaitForSeconds(disasterFrequency);
		disasterChance = Random.Range (0,4);
		Debug.Log (disasterChance);
		if (disasterChance == 0) {

			GetComponent<TornadoGenerator>().tornadoModel.renderer.enabled = true;
			GetComponent<TornadoGenerator>().tornadoEnabled = true;

		} else if (disasterChance == 1) {

			//GetComponent<FloodGenerator>().floodModel.renderer.enabled = true;
			GetComponent<FloodGenerator>().floodEnabled = true;

		} else if (disasterChance == 2) {


			GetComponent<EarthquakeGenerator>().earthquakeEnabled = true;

		} else if (disasterChance == 3) {
			
			
			GetComponent<VolcanoGenerator>().volcanoEnabled = true;
		}


		disasterActive = false;


	}

}
