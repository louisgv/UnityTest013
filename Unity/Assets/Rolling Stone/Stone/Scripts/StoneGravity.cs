using UnityEngine;
using System.Collections;

public class StoneGravity : MonoBehaviour
{
	Vector3 gravityDir;
	Vector3 force;

	void Start()
	{
		gravityDir = new Vector3(0, -10, 0);
	}

	void Update()
	{
		force = transform.right * 100;
		rigidbody.AddForce(force * Time.deltaTime);
		print("Force");
	}

	void FixedUpdate()
	{
		// Assign gravity to the level that the stone is touching
		Physics.gravity = gravityDir;
	}

	void OnCollisionStay(Collision collision)
	{
		print(collision.transform.name);
		// Get the transform of the mesh and set our gravity to be thre

		gravityDir = collision.transform.position - transform.position;
	}
	
}
