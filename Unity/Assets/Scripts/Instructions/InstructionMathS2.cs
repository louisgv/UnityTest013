using UnityEngine;
using System.Collections;

public class InstructionMathS2 : MonoBehaviour {

	// Generate a private int here?.... nah, just make it public int.
	// Use this for initialization
	public int[] number;
	
	public int answer;
	
	public bool simple = false;
	
	void Awake (){
		if (number.Length == 2){
			answer = 2;
			transform.Find("Question").GetComponent<TextMesh>().text = 
				answer.ToString() + "  " + answer.ToString();
		}
		else{
			answer = Random.Range(1,4);
		}
	}
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
