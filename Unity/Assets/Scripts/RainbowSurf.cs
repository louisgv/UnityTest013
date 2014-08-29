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
	public bool isGreenHole = false;
	public bool isBlueHole = false;
	public bool isYellowHole = false;
	public bool isPurpleHole = false;
	public bool isOrangeHole = false;
	public bool isMagentaHole = false;
	public bool isCyanHole = false;
	public bool isGrayHole = false;
	public bool isDarkHole = false;
	public bool isDistortHole = false;

	public Color distortCam, distortRain;
	// MIGHT BE BETTER TO ASSESS THIS DIRECTLY!
	// Maybe try some other type of holes?
	
	void OnTriggerStay2D(Collider2D col){
		if (col.gameObject.tag == "Player"){
			//Debug.Log("Stay!");
			if (isRedHole){
				ColorMorph(Color.black, Color.red) ;
				transform.parent.GetComponent<RainbowSystem>().failure = true;
			}
			else if (isGreenHole){
				ColorMorph(Color.red, Color.green);
				transform.parent.GetComponent<RainbowSystem>().failure = true;
			}
			else if (isBlueHole){
				ColorMorph(new Color (255.0f/255.0f, 189.0f / 255.0f, 0.0f/255.0f),
				           new Color (19.0f/255.0f, 25.0f / 255.0f, 79.0f/255.0f));
				transform.parent.GetComponent<RainbowSystem>().failure = true;
			}
			else if (isYellowHole){
				ColorMorph(new Color (138.0f/255.0f, 16.0f / 255.0f, 61.0f/255.0f), 
				           new Color (209.0f/255.0f, 255.0f / 255.0f, 25.0f/255.0f));
				transform.parent.GetComponent<RainbowSystem>().failure = true;
			}
			else if (isPurpleHole){
				ColorMorph(new Color (56.0f/255.0f, 228.0f / 255.0f, 173.0f/255.0f), 
				           new Color (50.0f/255.0f, 0.0f / 255.0f, 56.0f/255.0f));
				transform.parent.GetComponent<RainbowSystem>().failure = true;
				
			}
			else if (isOrangeHole){
				ColorMorph(new Color (1.0f/255.0f, 0.0f / 255.0f, 26.0f/255.0f), 
				           new Color (248.0f/255.0f, 132.0f / 255.0f, 81.0f/255.0f));
				transform.parent.GetComponent<RainbowSystem>().failure = true;
				
			}
			else if (isGrayHole){
				ColorMorph( new Color (251.0f/255.0f, 247.0f / 255.0f, 49.0f/255.0f), 
				           new Color (39.0f/255.0f, 53.0f / 255.0f, 52.0f/255.0f));
				transform.parent.GetComponent<RainbowSystem>().failure = true;
				
			}
			else if (isCyanHole){
				ColorMorph( Color.black, Color.cyan);
				transform.parent.GetComponent<RainbowSystem>().failure = true;
			}
			else if (isMagentaHole){
				ColorMorph( Color.white, Color.magenta);
				transform.parent.GetComponent<RainbowSystem>().failure = true;
			}
			else if (isDarkHole){
				ColorMorph(Color.white,
				           new Color (0,0,0,Random.Range(0.1f, 0.8f)));
				transform.parent.GetComponent<RainbowSystem>().failure = true;
			}
			else if (isDistortHole){
				ColorMorph (distortCam, distortRain);
				/*ColorMorph(Color.gray,
				           new Color (Random.Range(0.1f, 0.9f),
				           Random.Range(0.1f, 0.8f),
				           Random.Range(0.1f, 0.8f),
				           Random.Range(0.1f, 0.8f)));*/
				transform.parent.GetComponent<RainbowSystem>().failure = true;
			}
			else {
				transform.parent.GetComponent<RainbowSystem>().success = true;
			}
			//Level 10: make a new game Object, start a coroutine. If they stay inside, 
			//they won.
			//level 8: number or something liek that.
			//level 9: smile face. Watchmen?
			
			ZoomOut();
			
			
			col.transform.GetComponent<TouchMoveS2>().enabled = false;
			if (Vector2.Distance(col.transform.position, transform.position) != 0)
				col.transform.position = Vector2.MoveTowards(col.transform.position, 
				                                             transform.position, 
				                                             Time.deltaTime * morphSpeed);
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
