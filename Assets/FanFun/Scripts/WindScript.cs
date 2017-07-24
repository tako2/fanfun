using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag ("BalloonInStage")) {
			if (other.transform.position.x >= -4.0f && other.transform.position.x <= 4.0f) {
				//var rb = other.GetComponent<Rigidbody> ();
				var rb = other.attachedRigidbody;
				rb.AddForce (transform.up * 0.3f);
			}
		}
	}

	void OnTriggerStay(Collider other) {
		if (other.CompareTag ("BalloonInStage")) {
			if (other.transform.position.x >= -4.0f && other.transform.position.x <= 4.0f) {
				//var rb = other.GetComponent<Rigidbody> ();
				var rb = other.attachedRigidbody;
				rb.AddForce (transform.up * 0.2f);
			}
		}
	}
}
