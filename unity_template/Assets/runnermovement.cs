using UnityEngine;
using System.Collections;
using System;
using BladeCast;

public class runnermovement : MonoBehaviour {
	public float speed = 0.4f;

	void Start () {
		int starting_x = UnityEngine.Random.Range(10, 13);
		int starting_z = UnityEngine.Random.Range(-5, 6);
		transform.position = new Vector3(starting_x,0,starting_z);
		InitControllerListeners();
	}

	void InitControllerListeners() {
		BCMessenger.Instance.RegisterListener("connect", 0, this.gameObject, "HandleControllerRegister");
		BCMessenger.Instance.RegisterListener("movement", 0, this.gameObject, "HandleRotate_ControllerMovement");
	}

	void HandleControllerRegister() {
		print("Controller Connected");
	}

	void HandleRotate_ControllerMovement(ControllerMessage msg) {
		if (msg.Payload.HasField ("movement") && msg.Payload.HasField ("player") && msg.Payload.GetField ("player").ToString () [1] == '1') {
			string direction = msg.Payload.GetField ("movement").ToString ();
			direction = direction.Substring (1, direction.Length - 2);
			switch (direction) {
			case "Up":
				transform.position = new Vector3 (transform.position.x, 0, transform.position.z + 0.1f);
				break;
			case "Down":
				transform.position = new Vector3 (transform.position.x, 0, transform.position.z - 0.1f);
				break;
			case "Left":
				transform.position = new Vector3 (transform.position.x - 0.1f, 0, transform.position.z);
				break;
			case "Right":
				transform.position = new Vector3 (transform.position.x + 0.1f, 0, transform.position.z);
				break;
			}
		} else if (msg.Payload.GetField ("player").ToString () [1] == '2') {
		} else {
			print ("angle field or player not specified");
		}
	}

	void OnCollisionEnter(Collision coll) {
		if (coll.gameObject.tag == "Chaser")
			Debug.Log("Ran into Chaser");
			BCMessenger.Instance.SendToListeners("collide", -1);
	}

	void FixedUpdate () {}
}

