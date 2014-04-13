using UnityEngine;
using System.Collections;

public class ScreenShaker : MonoBehaviour {
	
	public float shakeStrength = 2f;
	public float shake = 0.5f;

	Vector3 originalPosition;
	
	void Start()
	{
		originalPosition = transform.localPosition;
	}
	
	void Update()
	{
		if(Input.GetMouseButton (0) || (Input.GetMouseButton (1)))
		{
			shake = shakeStrength;
		}
		
		Camera.main.transform.localPosition = originalPosition + (Random.insideUnitSphere * shake);
		
		shake = Mathf.MoveTowards(shake, 0, Time.deltaTime * shakeStrength);
		
		if(shake == 0)
		{
			Camera.main.transform.localPosition = originalPosition;
		}
	}
	
}