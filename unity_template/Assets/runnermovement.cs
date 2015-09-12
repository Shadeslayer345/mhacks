using UnityEngine;
using System.Collections;
using System;

public class runnermovement : MonoBehaviour {
	public float speed = 0.4f;
	Vector2 dest = Vector2.zero;
	// Use this for initialization
	void Start () {
		dest = transform.position;
	}
	
	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Chaser")
			Debug.Log("Welcome to the C# Station Tutorial!");
		
	} 

	
	// Update is called once per frame
	void FixedUpdate () {
		// Move closer to Destination
		Vector2 p = Vector2.MoveTowards(transform.position, dest, speed);
		GetComponent<Rigidbody2D>().MovePosition(p);


		
		if (Input.GetKey ("i")) {
			Vector3 destination = new Vector3 (transform.position.x, 3, transform.position.z + 0.1f);
			transform.position = destination;
		}
		if (Input.GetKey ("j")) {
			Vector3 destination = new Vector3 (transform.position.x - 0.1f, 3, transform.position.z);
			transform.position = destination;
		}
		if (Input.GetKey ("k")) {
			Vector3 destination = new Vector3 (transform.position.x, 3, transform.position.z - 0.1f);
			transform.position = destination;
		}
		if (Input.GetKey ("l")) {
			Vector3 destination = new Vector3 (transform.position.x + 0.1f, 3, transform.position.z);
			transform.position = destination;
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
	bool valid(Vector2 dir) {
		// Cast Line from 'next to Pac-Man' to 'Pac-Man'
		Vector2 pos = transform.position;
		RaycastHit2D hit = Physics2D.Linecast(pos + dir, pos);
		return (hit.collider == GetComponent<Collider2D>());
	}
}

