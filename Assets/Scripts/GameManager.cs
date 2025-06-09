using UnityEngine;
using System.Collections;
using UnityEngine.UI;
 

public enum GameState{
	InMenu, Ready , InGame , WinGame
}
public class GameManager : MonoBehaviour {
	public int Coin;

	public GameState currentState;
	public int Level;
	public DataBase [] DB;

	public Image Img;
	public string [] Answer;
	public char[] BtnsCharacter;

	public GameObject Gm_Game , Gm_Menu;


	 char [] GetAnswer;
	public GameObject [] AllText;
	public GameObject[] EnterBtns;
	public GameObject Pref_Btn;
	 int num = 0;
	 int Move = 0;
	 int NumChar = 0;
	 int Counter = 0;
	 int RowsCounter = 0;
	public Transform [] Rows;
	public Transform RowsManager;
	// Use this for initialization



	 GameObject[] BtnsChar;
	public GameObject WinMenu;
	 
	void Start () {
		//PlayerPrefs.SetInt ("Level",0);
		//Level = PlayerPrefs.GetInt ("Level");
		GetState (GameState.Ready);
	 
	}
 	
	void Update(){
		if (Input.GetKeyDown (KeyCode.B)) {
			BuyLevel ();
		}
	}
	 
	public void GetState (GameState Get){
		currentState = Get; 

		switch (currentState) {
		case GameState.InMenu:
			{
				Gm_Game.SetActive (false);
				Gm_Menu.SetActive (true);
			}
			break;
	
		case GameState.Ready:
			{
				Gm_Game.SetActive (true);
				Gm_Menu.SetActive (false);
				num = 0;
				Move = 0;
				NumChar = 0;
				Counter = 0;
				RowsCounter = 0;

				GetDataFromDataBase ();
				SetDataForBtns ();

				for (int i = 0; i < Answer.Length; i++) {
					NumChar += Answer [i].Length;
				}
				GetAnswer = new char[NumChar];
				AllText = new GameObject[NumChar];
				EnterBtns = new GameObject[NumChar];


				int space = 770;
				for (int i = 0; i < Answer.Length; i++) {
					if (Answer [i].Length * 80 <= space + Move) {
						CreateGameObject (i);
					} else {

						Rows [RowsCounter].localPosition = new Vector3 (Rows [RowsCounter].localPosition.x - (space + Move) / 2, Rows [RowsCounter].localPosition.y, Rows [RowsCounter].localPosition.z);


						RowsCounter += 1;
						Move = 0;
						CreateGameObject (i);
					}

				}

				Rows [RowsCounter].localPosition = new Vector3 (Rows [RowsCounter].localPosition.x - (space + Move) / 2, Rows [RowsCounter].localPosition.y, Rows [RowsCounter].localPosition.z);

				if (RowsCounter == 1) {
					RowsManager.localPosition = new Vector3 (RowsManager.localPosition.x, RowsManager.localPosition.y + 40, RowsManager.localPosition.z);
				}else if (RowsCounter == 2) {
					RowsManager.localPosition = new Vector3 (RowsManager.localPosition.x, RowsManager.localPosition.y + 80, RowsManager.localPosition.z);
				}



				GetState (GameState.InGame);
			}
			break;

		case GameState.InGame:
			{
				print ("InGame");
			}
			break;

		case GameState.WinGame:
			{
				WinMenu.SetActive (true);
			}
			break;
		}
	}
 
	public void CreateGameObject(int Get_i){
		for (int j = 0; j < Answer [Get_i].Length; j++) {
			GameObject Btn = Instantiate (Pref_Btn, Pref_Btn.transform.position, Quaternion.identity) as GameObject;
			Btn.transform.SetParent (Rows [RowsCounter], false);
			AllText [Counter] = Btn;
			Btn.GetComponent <TxtBtn > ().Id = Counter;
			Btn.transform.localPosition = new Vector3 (Btn.transform.localPosition.x + Move, 0, 0);
			Move -= 80;
			Counter += 1;

		}
		Move -= 40;
	}

	 
	public void GetChar(char Get , GameObject gm){
		if (num < GetAnswer.Length) {
			for (int i = 0; i < GetAnswer.Length; i++) {
				if (GetAnswer [i] == '\0') {
					GetAnswer [i] = Get;
					EnterBtns [i] = gm;
					gm.SetActive (false);
					AllText [i].GetComponent <TxtBtn > ().GetChar (Get);
					break;
				}
			}
			num += 1;
		}

		if (num >= GetAnswer.Length) {
			CheckAnswer ();
		}

	}

	public void CleanChar(int GetId){
		GetAnswer [GetId ] = '\0';
		EnterBtns [GetId].SetActive (true);
		EnterBtns [GetId] = null;
		num -= 1;
	}

	public void CheckAnswer(){
		print ("Check");
		bool Faul = false;

		int Counter = 0;
		for (int i = 0; i < Answer.Length; i++) {
			for (int j = 0; j < Answer [i].Length; j++) {
				if (GetAnswer [Counter] != Answer [i] [j]) {
					Faul = true;
				}
				Counter += 1;
			}
		}


		if (Faul == false) {
			GetState (GameState.WinGame);
		}
	}



	public void SetDataForBtns(){
		BtnsChar = GameObject.FindGameObjectsWithTag ("BtnCharacter");

		for (int i = 0; i< BtnsCharacter.Length; i++) {
			BtnsChar [i].GetComponent <Btns > ().SetCharacter (BtnsCharacter [i]);
		}
	}

	public void GetDataFromDataBase(){
		//Img.sprite = DB [Level].ImgLevel;
		Sprite sp = Resources .Load <Sprite>("Images/" + Level);
		Img.sprite = sp;
		Answer = DB [Level].Answer;
		BtnsCharacter = DB [Level].BtnChar;

	}

 
	public void GoToNextLevel(){
		Level += 1;

		if (Level > PlayerPrefs.GetInt ("TopLevel")) {
			PlayerPrefs.SetInt ("TopLevel", Level);
		}
		 
		for (int i = 0; i < AllText.Length; i++) {
			if (AllText [i] != null) 
				Destroy (AllText [i]);

		}

		for (int i = 0; i < EnterBtns.Length; i++) { 
			if (EnterBtns [i] != null)
			EnterBtns [i].SetActive (true);
		}

		WinMenu.SetActive (false);
		 
		for (int i = 0; i < Rows.Length; i++) {
			Rows [i].localPosition = new Vector3 (0, Rows [i].localPosition.y, Rows [i].localPosition.z);
		}

		GetState (GameState.Ready);
		 
	}


	public void BuyLevel(){
		if (Coin >= 100) {
			Coin -= 100;
			GoToNextLevel ();
		} else {
			print ("You Have Not Gold_ Go To Shop");
		}
	}

	public void EnableLevel(int LevelNumber){
		Level = LevelNumber;
		GetState (GameState.Ready);
	}
	 
	 


}
