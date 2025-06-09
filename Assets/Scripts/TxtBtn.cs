using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class TxtBtn : MonoBehaviour {
	public int Id;
	public Text Txt1;
	public GameObject Img1;


	public void ClearChar(){
		if (Txt1.text != "") {
			Txt1.text = "";
			FindObjectOfType <GameManager > ().CleanChar (Id);
			Img1.SetActive (false);
		}
	}

	public void GetChar(char Get){
		Txt1.text = Get.ToString ();
		Img1.SetActive (true);

	}
 
}
