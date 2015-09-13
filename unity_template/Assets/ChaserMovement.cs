using UnityEngine;
using System.Collections;
using BladeCast;
using System;

public class ChaserMovement : MonoBehaviour {
	public float speed = 0.4f;

	void Start () {
		int starting_x = UnityEngine.Random.Range(-12, -9);
		int starting_z = UnityEngine.Random.Range(-5, 6);
		transform.position = new Vector3(starting_x,0,starting_z);
		InitControllerListeners();
	}

	void  InitControllerListeners() {
		BCMessenger.Instance.RegisterListener("connect", 0, this.gameObject, "HandleControllerRegister");
		BCMessenger.Instance.RegisterListener("movement", 0, this.gameObject, "HandleRotate_ControllerMovement");
	}

	void HandleControllerRegister() {
		print("Connected to controller");
	}

	void HandleRotate_ControllerMovement(ControllerMessage msg) {
		if (msg.Payload.HasField ("movement") && msg.Payload.HasField ("player") && msg.Payload.GetField ("player").ToString () [1] == '2') {
			string direction = msg.Payload.GetField("movement").ToString();
			direction = direction.Substring(1,direction.Length-2);
			print(msg.Payload.GetField("player").ToString()[1]);
			switch (direction) {
			case "Up":
					transform.position = new Vector3 (transform.position.x, 0, transform.position.z + 0.1f); break;
			case "Down":
				transform.position = new Vector3 (transform.position.x, 0, transform.position.z - 0.1f);break;
			case "Left":
				transform.position = new Vector3 (transform.position.x - 0.1f, 0, transform.position.z); break;
			case "Right":
				transform.position = new Vector3 (transform.position.x + 0.1f, 0, transform.position.z); break;
			}

		} else if (msg.Payload.GetField ("player").ToString () [1] == '1') {
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
