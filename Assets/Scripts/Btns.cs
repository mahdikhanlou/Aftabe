using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Btns : MonoBehaviour {
	Button _btn;
	public char Character;
	void Start () {
		
		_btn = GetComponent <Button > ();


		_btn.onClick.AddListener (()=>{
			SendChar ();
		});
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SendChar(){

		FindObjectOfType <GameManager> ().GetChar (Character,gameObject);

	}


	public void SetCharacter(char GetChar){
		Character = GetChar;
		transform.GetChild (0).GetComponent<Text > ().text = Character.ToString ();
	}


 
 
}
