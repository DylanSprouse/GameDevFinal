using UnityEngine;
using System.Collections;

public class SelectionPlacement : MonoBehaviour {

	public Material grayHex, greenHex, blueHex;
	public GUIText hexagonOrderAlert;
	public GameObject hexagonAlert;
	public float hexagonSpinSpeed = 2f;

	private bool hexagonPlaced = true;
	private bool delayPlacement = false;
	private int randomHexagon;
	private GameObject newHexagon;

	void Update() {

		hexagonAlert.transform.Rotate (Vector3.up * Time.deltaTime * hexagonSpinSpeed);

		if (!hexagonPlaced) {
			hexagonPlaced = true;
			randomHexagon = Random.Range (0, 3);

			if (randomHexagon < 1) {


				hexagonAlert.renderer.material = grayHex;

				
			} else if (randomHexagon < 2) {


				hexagonAlert.renderer.material = greenHex;

			} else if (randomHexagon <= 3) {


				hexagonAlert.renderer.material = blueHex;

			}

		}

		Ray placementRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit placementHit = new RaycastHit();

		if (Physics.Raycast (placementRay, out placementHit, 1000f) && (Input.GetMouseButton(0))) {

			if (placementHit.collider.gameObject.tag == "Unused" && 
			    delayPlacement == false) {
				StartCoroutine(DelayPlacement());
				newHexagon = placementHit.collider.gameObject;
				HexagonRandomizer();
				newHexagon.renderer.enabled = true;
				hexagonPlaced = false;


			}


		}


	}

	void HexagonRandomizer() {

		if (randomHexagon == 0) {
			
			newHexagon.renderer.material = grayHex;

		} else if (randomHexagon == 1) {
			
			newHexagon.renderer.material = greenHex;
			
		} else if (randomHexagon == 2) {
			
			newHexagon.renderer.material = blueHex;
			
		}

	}

	private IEnumerator DelayPlacement() {
		delayPlacement = true;
		yield return new WaitForSeconds(0.25f);
		delayPlacement = false;
	}





}
