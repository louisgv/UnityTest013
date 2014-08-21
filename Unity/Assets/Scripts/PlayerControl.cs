using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
	[HideInInspector]
	public bool facingRight = true;			// For determining which way the player is currently facing.
	/*
	[HideInInspector]
	public bool jump = false;				// Condition for whether the player should jump.
	[HideInInspector]
	public bool dJump = false;            //Double Jump
    */
	public float moveForce = 360f;			// Amount of force added to move the player left and right.
	public float maxSpeed = 9f;				// The fastest the player can travel in the x axis.
	//public AudioClip[] jumpClips;			// Array of clips for when the player jumps.
	public float jumpForce = 900f;			// Amount of force added when the player jumps.
	//public AudioClip[] taunts;				// Array of clips for when the player taunts.
	//public float tauntProbability = 50f;	// Chance of a taunt happening.
	//public float tauntDelay = 1f;			// Delay for when the taunt should happen.


	//private int tauntIndex;					// The index of the taunts array indicating the most recent taunt.
	//private Transform groundCheck;			// A position marking where to check if the player is grounded.
	//private bool grounded = false;			// Whether or not the player is grounded.
	//private Animator anim;					// Reference to the player's animator component.
	
	//public bool escapeJump = true;
	
	public Vector3 spawnPosition = Vector3.zero;

	/*void Awake()
	{
		// Setting up references.
		groundCheck = transform.Find("groundCheck");
		//anim = GetComponent<Animator>();
	}*/


	void Update()
	 {
		// The player is grounded if a linecast to the groundcheck position hits anything on the ground layer.
		//grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));  

		// If the jump button is pressed and the player is grounded then the player should jump.
		if(Physics2D.gravity.normalized != (Vector2)Vector3.down){
			if(Input.GetButtonDown("Jump"))	{ 
				rigidbody2D.AddForce(new Vector2(-Physics2D.gravity.normalized.x*jumpForce, 
				                                 -Physics2D.gravity.normalized.y*jumpForce));
			}
		}
	}


	void FixedUpdate ()
	{
		// Cache the horizontal input.
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");
		Vector2 rightDir = Quaternion.AngleAxis(90.0f, Vector3.forward) * Physics2D.gravity.normalized;
		Vector2 upDir = -Physics2D.gravity.normalized;
		// The Speed animator parameter is set to the absolute value of the horizontal input.
		//anim.SetFloat("Speed", Mathf.Abs(h));

		// If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
		if(h * rigidbody2D.velocity.x < maxSpeed){
			// ... add a force to the player.
			rigidbody2D.AddForce(rightDir * h * moveForce);
		}
		if (Mathf.Abs(rigidbody2D.velocity.x) > maxSpeed){
			rigidbody2D.velocity = 
				new Vector2(Mathf.Sign(rigidbody2D.velocity.x) * maxSpeed, 
				            rigidbody2D.velocity.y);      
		}
		if (v * rigidbody2D.velocity.y < maxSpeed){
			rigidbody2D.AddForce(upDir * v * moveForce);
		}
		// If the player's horizontal velocity is greater than the maxSpeed...
		if(Mathf.Abs(rigidbody2D.velocity.y) > maxSpeed){
			rigidbody2D.velocity = 
				new Vector2(rigidbody2D.velocity.x, 
				            Mathf.Sign(rigidbody2D.velocity.y) * maxSpeed);
		}
		
		
			// ... set the player's velocity to the maxSpeed in the x axis.
			//rigidbody2D.velocity = new Vector2(Mathf.Sign(rigidbody2D.velocity.x) * maxSpeed, rigidbody2D.velocity.y);

		// If the input is moving the player right and the player is facing left...
		
		
		if(h > 0 && !facingRight)
			facingRight = !facingRight;
		
		else if(h < 0 && facingRight)
			facingRight = !facingRight;
		
			// ... flip the player.
		//	Flip();
		// Otherwise if the input is moving the player left and the player is facing right...
		//else if(h < 0 && facingRight)
			// ... flip the player.
		//	Flip();

		// If the player should jump...
		
	}
	
	
	/*void Flip ()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}*/

}
