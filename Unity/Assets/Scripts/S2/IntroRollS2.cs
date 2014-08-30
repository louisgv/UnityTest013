using UnityEngine;
using System.Collections;

public class IntroRollS2 : MonoBehaviour {

	// Use this for initialization
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
	// Update is called once per frame
	void Update () {
		if (isRedHole){
			ColorMorph(Color.black, Color.red) ;
		}
		else if (isGreenHole){
			ColorMorph(Color.red, Color.green);
		}
		else if (isBlueHole){
			ColorMorph(new Color (255.0f/255.0f, 189.0f / 255.0f, 0.0f/255.0f),
			           new Color (19.0f/255.0f, 25.0f / 255.0f, 79.0f/255.0f));
		}
		else if (isYellowHole){
			ColorMorph(new Color (138.0f/255.0f, 16.0f / 255.0f, 61.0f/255.0f), 
			           new Color (209.0f/255.0f, 255.0f / 255.0f, 25.0f/255.0f));
		}
		else if (isPurpleHole){
			ColorMorph(new Color (56.0f/255.0f, 228.0f / 255.0f, 173.0f/255.0f), 
			           new Color (50.0f/255.0f, 0.0f / 255.0f, 56.0f/255.0f));			
		}
		else if (isOrangeHole){
			ColorMorph(new Color (1.0f/255.0f, 0.0f / 255.0f, 26.0f/255.0f), 
			           new Color (248.0f/255.0f, 132.0f / 255.0f, 81.0f/255.0f));			
		}
		else if (isGrayHole){
			ColorMorph( new Color (251.0f/255.0f, 247.0f / 255.0f, 49.0f/255.0f), 
			           new Color (39.0f/255.0f, 53.0f / 255.0f, 52.0f/255.0f));
			
		}
		else if (isCyanHole){
			ColorMorph( Color.black, Color.cyan);
		}
		else if (isMagentaHole){
			ColorMorph( Color.white, Color.magenta);
		}
		else if (isDarkHole){
			ColorMorph(Color.white,
			           new Color (0,0,0,Random.Range(0.1f, 0.8f)));
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
}
