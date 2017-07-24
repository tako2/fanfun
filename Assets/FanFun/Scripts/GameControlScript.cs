using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControlScript : MonoBehaviour {

	public GameObject balloon;
	public GameObject left_wall;
	public GameObject right_wall;

	public GameObject msg_ready;
	public GameObject msg_clear;

	private List<GameObject> balloons_in_stage;

	private GameObject valve_balloon;

	private enum GameState
	{
		START,
		VALVE_BALLOON,
		WAIT_BALLOON,
		CLEAR
	};

	private GameState m_State = GameState.START;
	private float m_CurStateTime;
	private float m_NextStateTime;

	private int m_NumBalloonsLeft;
	private int m_NumBalloonsRight;

	private GameObject newValveBallon()
	{	
		var new_balloon = Instantiate (balloon, transform.position, transform.rotation);
		new_balloon.GetComponent<Rigidbody> ().isKinematic = true;
		new_balloon.transform.localScale = new Vector3 (0.0f, 0.0f, 0.0f);
		return (new_balloon);
	}

	// Use this for initialization
	void Start () {
		balloons_in_stage = new List<GameObject> ();
		m_State = GameState.START;
		m_CurStateTime = 0;
		m_NextStateTime = 5;
		m_NumBalloonsLeft = 0;
		m_NumBalloonsRight = 0;
		msg_ready.SetActive (true);
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Escape)) {
			Application.Quit ();
		}

		m_CurStateTime += Time.deltaTime;
		if (m_CurStateTime >= m_NextStateTime) {
			switch (m_State) {
			case GameState.START:
				msg_ready.SetActive (false);
				m_State = GameState.VALVE_BALLOON;
				m_CurStateTime = 0;
				m_NextStateTime = 3;
				valve_balloon = newValveBallon ();
				break;
			case GameState.VALVE_BALLOON:
				if (valve_balloon) {
					valve_balloon.transform.localScale = new Vector3 (1, 1, 1);
					valve_balloon.GetComponent<Rigidbody> ().isKinematic = false;
					balloons_in_stage.Add (valve_balloon);
					valve_balloon.GetComponent<Rigidbody> ().AddForce (0, -3, 0);
					valve_balloon = null;
				}
				m_State = GameState.WAIT_BALLOON;
				m_CurStateTime = 0;
				m_NextStateTime = 30;
				break;
			case GameState.WAIT_BALLOON:
				m_State = GameState.VALVE_BALLOON;
				m_CurStateTime = 0;
				m_NextStateTime = 3;
				valve_balloon = newValveBallon ();
				break;
			case GameState.CLEAR:
				// Go to Next Stage
				break;
			}
		} else {
			if (m_State == GameState.VALVE_BALLOON) {
				float scale = m_CurStateTime / m_NextStateTime;
				valve_balloon.transform.localScale = new Vector3 (scale, scale, scale);
				valve_balloon.transform.position = transform.position - new Vector3(0, scale / 2, 0);
			}
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag ("BalloonInStage")) {
		//if (balloons_in_stage.Contains(other.gameObject)) {
			if (other.transform.position.x > 0) {
				m_NumBalloonsRight++;
				if (m_NumBalloonsRight >= 4) {
					right_wall.SetActive (true);
				}
			} else {
				m_NumBalloonsLeft++;
				if (m_NumBalloonsLeft >= 4) {
					left_wall.SetActive (true);
				}
			}
			if ((m_NumBalloonsLeft + m_NumBalloonsRight) >= 8) {
				m_State = GameState.CLEAR;
				m_CurStateTime = 0;
				m_NextStateTime = 5;
				msg_clear.SetActive (true);
			} else if (m_State == GameState.WAIT_BALLOON) {
				m_CurStateTime = 0;
				m_NextStateTime = 2;
			}
		}
	}

	void OnTriggerExit(Collider other) {
		// for Fail Safe
		if (other.CompareTag ("BalloonInStage")) {
			if (other.transform.position.x > 0) {
				m_NumBalloonsRight--;
			} else {
				m_NumBalloonsLeft--;
			}
		}
	}
}
