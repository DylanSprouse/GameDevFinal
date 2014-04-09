using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(CharacterController))]

public class CharacterControl : MonoBehaviour
{
	
	public float moveSpeed = 0.1f;
	public float jump = 500f;
	private bool isJumping;
	//public float rotationSpeed;
	//CharacterController controller;
	
	void Start ()
	{
		//controller = GetComponent<CharacterController> ();
	}
	
	void Update ()
	{
		if (Input.GetKey (KeyCode.RightArrow)) {
			transform.position += transform.right * moveSpeed;
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {
			transform.position += -transform.right * moveSpeed;
		}
		if (Input.GetKey (KeyCode.UpArrow)) {
			transform.position += transform.forward * moveSpeed;
		}
		if (Input.GetKey (KeyCode.DownArrow)) {
			transform.position += -transform.forward * moveSpeed;
		}
		if(!isJumping){
			if (Input.GetKeyDown (KeyCode.Space)) {
				//transform.position += transform.up * jump;
				//transform.rigidbody.AddForce(transform.up * jump);
				//rigidbody.AddForce (transform.up * jump, ForceMode.Impulse);     
				transform.rigidbody.AddForce (0, jump, 0);
				//isJumping = true;
			}
		}
		
	}
	
	private void OnCollisionEnter (Collision c)
	{
		if(c.gameObject.name =="FloorPlane")
			isJumping = true;
	}
	
}
