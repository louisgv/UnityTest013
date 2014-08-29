using UnityEngine;
using System.Collections;

public class CircleQuizS2 : MonoBehaviour {

	//public bool r, g, b, y, p, o, g, d;
	
	public Color colorQ;
	
	public bool distort = true;
	// Update is called once per frame
	
	// take a new color, write the function for that.
	
	void Awake(){
		if (distort){
			colorQ = new Color (Random.Range (0.0f,1.0f), 
			                    Random.Range(0.0f,1.0f), 
	                    Random.Range(0.0f,1.0f),
	                    Random.Range(0.0f,1.0f));
			//How to get inverse color???
	
			transform.GetComponent<SpriteRenderer>().color = colorQ;
			transform.parent.GetComponent<RainbowSurf>().distortCam = colorQ;
			transform.parent.GetComponent<RainbowSurf>().distortRain = 
				new Color (1.0f - colorQ.r, 1.0f - colorQ.g, 1.0f - colorQ.b, 1.0f - colorQ.a);
			transform.parent.GetComponent<RainbowSurf>().isDistortHole = true;
		}
	}
}
