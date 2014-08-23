using UnityEngine;
using System.Collections;

public class TouchMoveS2 : MonoBehaviour {

	
	public float speed = 5f;
	private Vector3 pos;
	
	// Use this for initialization
	void Start () {
		pos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0)) {
			pos = Input.mousePosition;
			pos.z = transform.position.z - Camera.main.transform.position.z;
			pos = Camera.main.ScreenToWorldPoint(pos);
			
		}
		transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);
		
		//Vector3 dir = pos - transform.position;
	
	}
	
	
}
