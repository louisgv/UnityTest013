using UnityEngine;
using System.Collections;

public class RainbowSurf : MonoBehaviour {

	//we will design the level. Still, need a... math or something for gameplay.
	// Randomly assign the target. Then what??
	
	// Use this for initialization
	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "Player"){
			//Debug.Log("In!");
			Camera.main.GetComponent<BackGroundGradient>().randomColor();
			
		}
	}
	
	public bool isRedHole = false;
	public bool isDarkHole = false;
	public bool isDistortHole = false;
	public bool finish = false; // MIGHT BE BETTER TO ASSESS THIS DIRECTLY!
	// Maybe try some other type of holes?
	
	void OnTriggerStay2D(Collider2D col){
		if (col.gameObject.tag == "Player"){
			//Debug.Log("Stay!");
			if (isRedHole){
				ColorMorph(Color.black, 
				           Color.red) ;
			}
			else if (isDarkHole){
				ColorMorph(Color.white,
				           new Color (0,0,0,Random.Range(0.1f, 0.8f)));
			}
			else if (isDistortHole){
				ColorMorph(Color.gray,
				           new Color (Random.Range(0.1f, 0.9f),
				           Random.Range(0.1f, 0.8f),
				           Random.Range(0.1f, 0.8f),
				           Random.Range(0.1f, 0.8f)));
			}
			else {
				
			}
			
			ZoomOut();
			
			col.transform.GetComponent<TouchMoveS2>().enabled = false;
			if (Vector2.Distance(col.transform.position, transform.position) != 0)
				col.transform.position = Vector2.MoveTowards(col.transform.position, 
				                                             transform.position, 
				                                             Time.deltaTime * morphSpeed);
			if (Vector2.Distance(col.transform.position, transform.position) < 1){
				finish = true;
			}
		}
	}
	
	public float morphSpeed = 0.9f;
	
	void ColorMorph(Color targetCam, Color targetRain){
		Camera.main.GetComponent<BackGroundGradient>().enabled = false;
		Camera.main.backgroundColor = Color.Lerp(Camera.main.backgroundColor,
		                                         targetCam,
		                                         Time.deltaTime * morphSpeed);
		foreach (SpriteRenderer s in transform.Find("RL").GetComponentsInChildren<SpriteRenderer>())
			s.material.color = Color.Lerp(s.material.color,
			                              targetRain,
			                              Time.deltaTime*morphSpeed);
	}
	
	
	public float zoomSpeed = 0.9f;
	void ZoomOut(){
		float orthographPreSize = Camera.main.orthographicSize;
		if (orthographPreSize < 18.0f)
			Camera.main.orthographicSize += morphSpeed * Time.deltaTime;
		if (orthographPreSize > 18.0f)
			Camera.main.orthographicSize -= morphSpeed * Time.deltaTime;
		Camera.main.transform.localScale *= 
			Camera.main.orthographicSize/orthographPreSize;
	}
}
