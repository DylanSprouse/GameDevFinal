﻿using UnityEngine;
using System.Collections;

public class EarthquakeGenerator : MonoBehaviour {
	
	public bool earthquakeEnabled = false;
	public float bounceHeightMax = 0.05f;
	public float bounceHeightMin = 0.01f;
	public float bounceSpeed = 30f;
	public float shakeStrength = .2f;
	public float shake = 0.5f;

	Vector3 originalPos;


	void Start () {

		originalPos = transform.localPosition;

	}


	void Update () {

		if (earthquakeEnabled) {

			StartCoroutine (EarthquakeLength());
			shake = shakeStrength;

			Camera.main.transform.position += transform.up * (Mathf.Sin (Time.time * bounceSpeed) * Random.Range(bounceHeightMin, bounceHeightMax)); 

			shake = Mathf.MoveTowards (shake, 0, Time.deltaTime * shakeStrength);

		}

		if (shake == 0) {
			
			Camera.main.transform.localPosition = originalPos;
			
		}

	}

	private IEnumerator EarthquakeLength() {

		yield return new WaitForSeconds(5f);
		GetComponent<MouseController>().cityDecayRate = 0.001f;
		earthquakeEnabled = false; 

	}
}