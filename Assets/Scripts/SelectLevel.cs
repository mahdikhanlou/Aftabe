using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class SelectLevel : MonoBehaviour {
	public int LevelNumber;
	public bool Lock = true;
	public GameObject Img_Lock ,Img_Level ;
	 
	void Start(){
		PlayerPrefs.SetInt ("Level", 50);

		if (LevelNumber <= PlayerPrefs.GetInt ("Level")) {
			Lock = false;
		}

		if (Lock == false) {
			Img_Lock.SetActive (false);
			Img_Level.gameObject.SetActive (true);


			Sprite sp = Resources.Load <Sprite> ("Images/" + LevelNumber);
			Img_Level.GetComponent <Image > ().sprite = sp;
		}
	}
	public void Select(){
		if (Lock == false) {
			FindObjectOfType <GameManager > ().EnableLevel (LevelNumber);
		} else {
			print ("This Level Locked");
		}

	}
}
