using UnityEngine;
using System.Collections;
using System;

public class runnermovement : MonoBehaviour {
	public float speed = 0.4f;
	Vector3 destination = Vector3.zero;
	// Use this for initialization
	void Start () {
		destination = transform.position;
	}
	
	void OnCollisionEnter(Collision coll) {
		if (coll.gameObject.tag == "Chaser")
			Debug.Log("Ran into Chaser");
		
	} 
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetKey ("i")) {
			transform.position = new Vector3 (transform.position.x, 0, transform.position.z + 0.1f);
		}
		if (Input.GetKey ("j")) {
			transform.position = new Vector3 (transform.position.x - 0.1f, 0, transform.position.z);
		}
		if (Input.GetKey ("k")) {
			transform.position = new Vector3 (transform.position.x, 0, transform.position.z - 0.1f);
		}
		if (Input.GetKey ("l")) {
			transform.position = new Vector3 (transform.position.x + 0.1f, 0, transform.position.z);
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

