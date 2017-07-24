using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperScript : MonoBehaviour {

	public float force;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag ("BalloonInStage")) {
			var rb = other.attachedRigidbody;
			rb.AddForce (transform.up * force);
		}
	}

	void OnTriggerStay(Collider other)
	{
		if (other.CompareTag ("BalloonInStage")) {
			var rb = other.attachedRigidbody;
			rb.AddForce (transform.up * force);
		}
	}
}
