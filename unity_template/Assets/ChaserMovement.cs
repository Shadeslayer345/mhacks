using UnityEngine;
using System.Collections;
using BladeCast;
using System;

public class ChaserMovement : MonoBehaviour {
	public float speed = 0.4f;
	Vector3 destination = Vector3.zero;

	void Start () {
		System.Random rnd = new System.Random();
		int starting_x = UnityEngine.Random.Range(-12, -9);
		int starting_z = UnityEngine.Random.Range(-5, 6);
		transform.position = new Vector3(starting_x,0,starting_z);
		destination = transform.position;
		InitControllerListeners();
	}

	void  InitControllerListeners() {
		BCMessenger.Instance.RegisterListener("connect", 0, this.gameObject, "HandleControllerRegister");
		BCMessenger.Instance.RegisterListener("movement", 0, this.gameObject, "HandleRotate_ControllerMovement");
	}

	void HandleControllerRegister() {
		print("Connected to controller");
	}

	void HandleMovement(ControllerMessage msg) {
		if (msg.Payload.HasField ("movement") && msg.Payload.HasField ("player") && int.Parse(msg.Payload.GetField("player").ToString()) == 1) {
			string temp = msg.Payload.GetField("movement").ToString();
			switch (temp) {
			case "Up":
					transform.position = new Vector3 (transform.position.x, 3, transform.position.z + 0.1f); break;
			case "Down":
				transform.position = new Vector3 (transform.position.x, 3, transform.position.z - 0.1f);break;
			case "Left":
				transform.position = new Vector3 (transform.position.x - 0.1f, 3, transform.position.z); break;
			case "Right":
				transform.position = new Vector3 (transform.position.x + 0.1f, 3, transform.position.z); break;
			}

		} else {
			print ("angle or movement did not exist");
		}
	}

	void OnCollision(Collider col) {
		Debug.Log ("Chase Hit Something.");
		if (col.gameObject.tag == "Runner")
			Debug.Log("Ran into Runner!");
	}

	void FixedUpdate () {}
}
