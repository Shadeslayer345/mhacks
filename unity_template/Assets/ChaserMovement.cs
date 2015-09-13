using UnityEngine;
using System.Collections;
using BladeCast;
using System;

public class ChaserMovement : MonoBehaviour {
	public float speed = 0.4f;
	Vector3 destination = Vector3.zero;
	// Use this for initialization


	void Start () {
		System.Random rnd = new System.Random();
		int starting_x = UnityEngine.Random.Range(-12, -9);
		int starting_z = UnityEngine.Random.Range(-5, 6);
		transform.position = new Vector3(starting_x,0,starting_z);
		destination = transform.position;
	}


	/**void  InitControllerListeners() {
		BCMessenger.Instance.RegisterListener("connect", 0, this.gameObject, "HandleControllerRegister");
		BCMessenger.Instance.RegisterListener("movement", 0, this.gameObject, "HandleRotate_ControllerMovement");
	}

	void HandleControllerRegister() {
		print("Connected to controller");
	}

	void HandleMovement(ControllerMessage msg) {
		if (msg.Payload.HasField ("movement") && msg.Payload.HasField ("player") && int.Parse(msg.Payload.GetField("player").ToString()) == 1) {
			string temp = msg.Payload.GetField("movement").ToString();
			Vector3 p = Vector3.MoveTowards(transform.position, dest, speed);
			//GetComponent<Rigidbody>().MovePosition(p);
			switch (temp) {
			case "Up":
					transform.position = (new Vector3 (transform.position.x, 3, transform.position.z + 0.1f)); break;
			case "Down":
				transform.position = new Vector3 (transform.position.x, 3, transform.position.z - 0.1f);break;
			case "Left":
				transform.position = new Vector3 (transform.position.x - 0.1f, 3, transform.position.z); break;
			case "Right":
				transform.position = new Vector3 (transform.position.x + 0.1f, 3, transform.position.z); break;
			}

		} else {
			print ("angle field did not exist");
		}
	}*/

	Vector3 moveUp() {
		return new Vector3 (transform.position.x, 0, transform.position.z + 0.1f);
	}

	Vector3 moveLeft() {
		return new Vector3 (transform.position.x - 0.1f, 0, transform.position.z);
	}

	Vector3 moveDown() {
		return new Vector3 (transform.position.x, 0, transform.position.z - 0.1f);
	}

	Vector3 moveRight() {
		return new Vector3 (transform.position.x + 0.1f, 0, transform.position.z);
	}
	
	void OnCollision(Collider col) {
		Debug.Log ("Chase Hit Something.");
		if (col.gameObject.tag == "Runner")
			Debug.Log("Ran into Runner!");
	}

	// Update is called once per frame
	void FixedUpdate () {
		// Move closer to Destination
		if (Input.GetKey ("w")) {
			//destination = moveUp();
			transform.position = moveUp();
		}
		if (Input.GetKey ("a")) {
			transform.position = moveLeft();
		}
		if (Input.GetKey ("s")) {
			transform.position = moveDown();
		}
		if (Input.GetKey ("d")) {
			transform.position = moveRight();
		}

	// Check for Input if not moving
		/*if ((Vector2)transform.position == dest) {
			Debug.Log("At destination");
			if (Input.GetKey (KeyCode.UpArrow) && valid(Vector2.up))
				dest = (Vector2)transform.position + Vector2.up;
			if (Input.GetKey (KeyCode.RightArrow) && valid(Vector2.right))
				dest = (Vector2)transform.position + Vector2.right;
			if (Input.GetKey (KeyCode.DownArrow) && valid(-Vector2.up))
				dest = (Vector2)transform.position - Vector2.up;
			if (Input.GetKey (KeyCode.LeftArrow) && valid(-Vector2.right))
				dest = (Vector2)transform.position - Vector2.right;
		}*/
	}
	bool valid(Vector3 dir) {
		// Cast Line from 'next to Pac-Man' to 'Pac-Man'
		Vector3 pos = transform.position;
		RaycastHit2D hit = Physics2D.Linecast(pos + dir, pos);
		return (hit.collider == GetComponent<Collider2D>());
	}
}
