using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunControlScript : MonoBehaviour {

	public GameObject m_FunBase;
	public GameObject m_Fun;

	public float fun_speed = 1.0f;
	public float fun_angle = 90.0f;

	// Use this for initialization
	void Start () {
		m_Fun.transform.Rotate (new Vector3 (0, 0, 90));
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Z)) {
			float next_angle;
			next_angle = m_Fun.transform.rotation.eulerAngles.z + fun_angle * Time.deltaTime;
			if (next_angle < 180.0f) {
				m_Fun.transform.Rotate (new Vector3 (0, 0, fun_angle * Time.deltaTime));
			} else {
				m_Fun.transform.Rotate (new Vector3 (0, 0, 180.0f - m_Fun.transform.rotation.eulerAngles.z));
			}
		} else if (Input.GetKey (KeyCode.X)) {
			float next_angle;
			next_angle = m_Fun.transform.rotation.eulerAngles.z - fun_angle * Time.deltaTime;
			if (next_angle > 0.0f) {
				m_Fun.transform.Rotate (new Vector3 (0, 0, - fun_angle * Time.deltaTime));
			} else {
				m_Fun.transform.Rotate (new Vector3 (0, 0, -m_Fun.transform.rotation.eulerAngles.z));
			}
		}
		if (Input.GetKey (KeyCode.Slash)) {
			float next_pos;
			next_pos = m_FunBase.transform.position.x - fun_speed * Time.deltaTime;
			if (next_pos > -3.0f) {
				m_FunBase.transform.position += new Vector3 (-fun_speed * Time.deltaTime, 0, 0);
			} else {
				m_FunBase.transform.position += new Vector3 (-3.0f - m_FunBase.transform.position.x, 0, 0);
			}
		} else if (Input.GetKey (KeyCode.Underscore)) {
			float next_pos;
			next_pos = m_FunBase.transform.position.x + fun_speed * Time.deltaTime;
			if (next_pos < 3.0f) {
				m_FunBase.transform.position += new Vector3 (fun_speed * Time.deltaTime, 0, 0);
			} else {
				m_FunBase.transform.position += new Vector3 (3.0f - m_FunBase.transform.position.x, 0, 0);
			}
		}
	}
}
