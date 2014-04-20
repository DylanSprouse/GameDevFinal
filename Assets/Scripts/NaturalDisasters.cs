using UnityEngine;
using System.Collections;

public class NaturalDisasters : MonoBehaviour {

	public int disasterChance;
	public bool disasterActive = false;

	void Update() {

		if (!disasterActive) {
			
			StartCoroutine(SetDisaster());
			
		}

	}

	private IEnumerator SetDisaster() {
		disasterActive = true;
		yield return new WaitForSeconds(8f);
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
