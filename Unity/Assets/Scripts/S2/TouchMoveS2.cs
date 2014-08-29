using UnityEngine;
using System.Collections;

public class TouchMoveS2 : MonoBehaviour {

	
	public float speed = 5f;
	private Vector3 pos;
	
	// Use this for initialization
	void Start () {
		pos = transform.position;
	}
	public float maxPosition;
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0)) {
			pos = Input.mousePosition;
			pos.z = transform.position.z - Camera.main.transform.position.z;
			pos = Camera.main.ScreenToWorldPoint(pos);
			transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);
			
		}
		//Need to implement maxPosition somehow. If the distance toward the center, we need to negate this function from happen. How?
		//Using the position of the mouse click? if click out-of-range, negate from moving?..
		// If that's the case... might use collider?..
		
		//Vector3 dir = pos - transform.position;
	
	}
	
	
}
