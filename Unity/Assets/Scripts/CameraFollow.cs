using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour 
{
	public float xMargin = 1f;		// Distance in the x axis the player can move before the camera follows.
	public float yMargin = 1f;		// Distance in the y axis the player can move before the camera follows.
	public float xSmooth = 8f;		// How smoothly the camera catches up with it's target movement in the x axis.
	public float ySmooth = 8f;		// How smoothly the camera catches up with it's target movement in the y axis.
	public Vector2 maxXAndY;		// The maximum x and y coordinates the camera can have.
	public Vector2 minXAndY;		// The minimum x and y coordinates the camera can have.

	private Transform player;		// Reference to the player's transform.


	void Awake ()
	{
		// Setting up the reference.
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}


	bool CheckXMargin()
	{
		// Returns true if the distance between the camera and the player in the x axis is greater than the x margin.
		return Mathf.Abs(transform.position.x - player.position.x) > xMargin;
	}


	bool CheckYMargin()
	{
		// Returns true if the distance between the camera and the player in the y axis is greater than the y margin.
		return Mathf.Abs(transform.position.y - player.position.y) > yMargin;
	}
	
	public Vector3 maxPosition = new Vector3 (999.0f,999.0f,0);
	public Vector3 minPosition = new Vector3 (-999.0f,-999.0f,0);
	public float zoomSpeed = 90.0f;
	
	void FixedUpdate ()
	{
		ZoomScreen();
		TrackPlayer();
		if (player.localPosition.y < minPosition.y ||
			player.localPosition.y > maxPosition.y||
			player.localPosition.x < minPosition.x ||
		    player.localPosition.x > maxPosition.x){
			//StartCoroutine("ReloadGame");
			player.position = 
				player.gameObject.GetComponent<PlayerControl>().spawnPosition;
		}
	}
	
	void ZoomScreen(){
		if (Input.GetAxis("Mouse ScrollWheel") < 0){
			float orthographPreSize = Camera.main.orthographicSize;
			if (orthographPreSize < 900){
				if (orthographPreSize > 90)
				{
					Camera.main.orthographicSize += zoomSpeed * Time.deltaTime * 9;
					player.Find("Rainbows").localScale *= 
						Camera.main.orthographicSize/orthographPreSize;
				}
				else{
					Camera.main.orthographicSize += zoomSpeed * Time.deltaTime ;	
				}
			}
			transform.localScale *= 
				Camera.main.orthographicSize/orthographPreSize;
		}
		if (Input.GetAxis("Mouse ScrollWheel") > 0){
			float orthographPreSize = Camera.main.orthographicSize;			
			if (orthographPreSize > 9){
				if (orthographPreSize >90){
					Camera.main.orthographicSize -= zoomSpeed * Time.deltaTime * 9;
					player.Find("Rainbows").localScale *= Camera.main.orthographicSize/orthographPreSize;
				}
				else{
					Camera.main.orthographicSize -= zoomSpeed * Time.deltaTime ;
				}
			}
			transform.localScale *= Camera.main.orthographicSize/orthographPreSize;
		}
	}
	
	void TrackPlayer ()
	{
		// By default the target x and y coordinates of the camera are it's current x and y coordinates.
		float targetX = transform.position.x;
		float targetY = transform.position.y;

		// If the player has moved beyond the x margin...
		if(CheckXMargin())
			// ... the target x coordinate should be a Lerp between the camera's current x position and the player's current x position.
			targetX = Mathf.Lerp(transform.position.x, player.position.x, xSmooth * Time.deltaTime);

		// If the player has moved beyond the y margin...
		if(CheckYMargin())
			// ... the target y coordinate should be a Lerp between the camera's current y position and the player's current y position.
			targetY = Mathf.Lerp(transform.position.y, player.position.y, ySmooth * Time.deltaTime);

		// The target x and y coordinates should not be larger than the maximum or smaller than the minimum.
		targetX = Mathf.Clamp(targetX, minXAndY.x, maxXAndY.x);
		targetY = Mathf.Clamp(targetY, minXAndY.y, maxXAndY.y);

		// Set the camera's position to the target position with the same z component.
		transform.position = new Vector3(targetX, targetY, transform.position.z);
	}
	
	IEnumerator ReloadGame()
	{			
		// ... pause briefly
		yield return new WaitForSeconds(0);
		// ... and then reload the level.
		Application.LoadLevel(Application.loadedLevel);
	}
}
