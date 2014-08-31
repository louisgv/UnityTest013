using UnityEngine;
using System.Collections;

public class GameJoltAPIManager : MonoBehaviour {

	public int gameID;
	public string privateKey;
	public string userName;
	public string userToken;
	void Awake () {
		DontDestroyOnLoad ( gameObject );
		GJAPI.Init ( gameID, privateKey );

#if UNITY_EDITOR
		OnGetFromWeb ("louisgv", "f8db4c");
#else
		GJAPIHelper.Users.GetFromWeb (OnGetFromWeb);
#endif

		StartCoroutine(checkUser());
	}
	
	IEnumerator checkUser(){
		yield return new WaitForSeconds(3.0f);
		if (GJAPI.User == null){
			GJAPIHelper.Users.ShowLogin();	
		}		
	}
	
	void OnGetFromWeb(string name, string token){
		if (name != null || token != null) {
			GJAPI.Users.Verify(name, token);
		}
	}
	void OnEnable () {
		GJAPI.Users.VerifyCallback += OnVerifyUser;
	}
	
	void OnDisable () {
		GJAPI.Users.VerifyCallback -= OnVerifyUser;
	}
	void OnVerifyUser ( bool success ) {
		if ( success ) {		
			GJAPIHelper.Users.ShowGreetingNotification ();
			Debug.Log ( "Yepee!" );
		}
		else {
			Debug.Log ( "Um... Something went wrong." );
		}
	}
}
