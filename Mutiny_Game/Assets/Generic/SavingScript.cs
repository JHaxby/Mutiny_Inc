using UnityEngine;
using System.Collections;

public class SavingScript : MonoBehaviour {
	
	int SaveItem1;
	int SaveItem2;
	int SaveItem3;
	int SaveItem4;
	int SaveItem5;
	int SaveItem6;
	int SaveItem7;
	int SaveItem8;
	int SaveItem9;
	int SaveItem10;
	int SaveItem11;
	int SaveItem12;
	int SaveItem13;
	int SaveItem14;
	int SaveItem15;
	
	GameObject Player;
	
	float PositionX;
	float PositionY;
	float PositionZ;	
	float RotationX;
	float RotationY;
	float RotationZ;
	
	int Level;
	
	public static int SaveToUpdate;
	
	public bool Save = false;
	private Rect SceneRect = new Rect(0,0,0,0);
	private int selectionState = -1;
	private int ConfirmSave = -1;
	private string[] selectionString = new string[]{"Yes", "No"};
	private string[] ConfirmSaveString = new string[]{"Save", "Cancel"};
	private int SaveWindow_ID = 0;
	bool SavingBox = false;
	bool SavedBox = false;
	
	public static string Savenum1Name = "";
	public static string Savenum2Name = "";
	public static string Savenum3Name = "";
	
	private string DataToSave;
	
	private bool selectedSave = false;


	private RaycastHit hit;


	
	void OnGUI(){

	if (Save)
		{
			SceneRect = GUI.Window(SaveWindow_ID, new Rect((Screen.width/2) - 170, (Screen.height/2) - 60, 340, 120),SaveWindow, "Would you like to save?");
		}
		
	if(SavingBox)
		{
			GUI.Box (new Rect ((Screen.width /2) - 100,(Screen.height /2) - 50,200,100), "Saving...");
		}
	if(SavedBox)
		{
			GUI.Box (new Rect ((Screen.width /2) - 100,(Screen.height /2) - 50,200,100), "Saved!");
			StartCoroutine(DoneSave());
		}
		
	}	
	
	void Update(){
		if(Input.GetMouseButtonUp(0))
		{
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				if(Physics.Raycast(Camera.main.transform.position, ray.direction, out hit, 1000))
				{
					if(hit.transform.gameObject == this.transform.gameObject)
					{
		   		   		Save = true;
						
						Savenum1Name = LoginScript.Savenum1Name;
						Savenum2Name = LoginScript.Savenum2Name;
						Savenum3Name = LoginScript.Savenum3Name;
					}
				}
		}
	}
	
	IEnumerator DoneSave()
	{
		yield return new WaitForSeconds(1);
		SavedBox = false;
		selectedSave = false;
		Movement.inInventory = true;

	}
	
	private void SaveWindow(int id)
	{
		GUI.FocusWindow(0);
		
		if(selectedSave == false){
			selectionState = -1;
		}
		ConfirmSave = -1;
		if(selectionState < 0){
			selectionState = GUI.SelectionGrid(new Rect(10,30,320,80),selectionState,selectionString,2);
			Movement.inInventory = true;
		}
		
		if(selectionState > -1){
			selectedSave = true;	
		}
		
		if(selectedSave == true){
			Movement.inInventory = true;

			GUILayout.Label("Please enter a name for your save:");
			if(SaveToUpdate == 4){
				Savenum1Name = GUILayout.TextField(Savenum1Name);	
			}else if(SaveToUpdate == 5){
				Savenum2Name = GUILayout.TextField(Savenum2Name);	
			}else{
				Savenum3Name = GUILayout.TextField(Savenum3Name);	
			}
			
			ConfirmSave = GUI.SelectionGrid(new Rect(10,70,320,90),ConfirmSave,ConfirmSaveString,2);

		}
		
		if(ConfirmSave == 0){
			string user = LoginScript.user;
			
			//SaveToUpdate (Done in Login Script)
			//SaveItem1 = FloatingInventory.HeldInv[0];
			//SaveItem2 = FloatingInventory.HeldInv[1];
			//SaveItem3 = FloatingInventory.HeldInv[2];
			//SaveItem4 = FloatingInventory.HeldInv[3];
			//SaveItem5 = FloatingInventory.HeldInv[4];
			//SaveItem6 = FloatingInventory.HeldInv[5];
			//SaveItem7 = FloatingInventory.HeldInv[6];
			//SaveItem8 = FloatingInventory.HeldInv[7];
			//SaveItem9 = FloatingInventory.HeldInv[8];
			//SaveItem10 = FloatingInventory.HeldInv[9];
			//SaveItem11 = FloatingInventory.HeldInv[10];
			//SaveItem12 = FloatingInventory.HeldInv[11];
			//SaveItem13 = FloatingInventory.HeldInv[12];
			//SaveItem14 = FloatingInventory.HeldInv[13];
			//SaveItem15 = FloatingInventory.HeldInv[14];
			
			SaveItem1 = Inventory.inBagList[0];
			SaveItem2 = Inventory.inBagList[1];
			SaveItem3 = Inventory.inBagList[2];
			SaveItem4 = Inventory.inBagList[3];
			SaveItem5 = Inventory.inBagList[4];
			SaveItem6 = Inventory.inBagList[5];
			SaveItem7 = Inventory.inBagList[6];
			SaveItem8 = Inventory.inBagList[7];
			SaveItem9 = Inventory.inBagList[8];
			SaveItem10 = Inventory.inBagList[9];
			SaveItem11 = Inventory.inBagList[10];
			SaveItem12 = Inventory.inBagList[11];
			SaveItem13 = Inventory.inBagList[12];
			SaveItem14 = Inventory.inBagList[13];
			SaveItem15 = Inventory.inBagList[14];
			
			Player = GameObject.FindGameObjectWithTag("Player");

			PositionX = Player.transform.position.x; 
			PositionY = Player.transform.position.y;
			PositionZ = Player.transform.position.z;	

			
			Level = Application.loadedLevel;
			
			if(SaveToUpdate == 4){
				DataToSave = PositionX.ToString() + "^" + PositionY.ToString() + "^" + PositionZ.ToString() + "^" + Level.ToString() + "^"  + SaveItem1.ToString() + "^" + SaveItem2.ToString() + "^" + SaveItem3.ToString() + "^" + SaveItem4.ToString() + "^" + SaveItem5.ToString() + "^" + SaveItem6.ToString() + "^" + SaveItem7.ToString() + "^" + SaveItem8.ToString() + "^" + SaveItem9.ToString() + "^" + SaveItem10.ToString() + "^" + SaveItem11.ToString() + "^" + SaveItem12.ToString() + "^" + SaveItem13.ToString() + "^" + SaveItem14.ToString() + "^" + SaveItem15.ToString() + "^" + Savenum1Name;
			}else if(SaveToUpdate == 5){
				DataToSave = PositionX.ToString() + "^" + PositionY.ToString() + "^" + PositionZ.ToString() + "^" + Level.ToString() + "^"  + SaveItem1.ToString() + "^" + SaveItem2.ToString() + "^" + SaveItem3.ToString() + "^" + SaveItem4.ToString() + "^" + SaveItem5.ToString() + "^" + SaveItem6.ToString() + "^" + SaveItem7.ToString() + "^" + SaveItem8.ToString() + "^" + SaveItem9.ToString() + "^" + SaveItem10.ToString() + "^" + SaveItem11.ToString() + "^" + SaveItem12.ToString() + "^" + SaveItem13.ToString() + "^" + SaveItem14.ToString() + "^" + SaveItem15.ToString() + "^" + Savenum2Name;
			}else{
				DataToSave = PositionX.ToString() + "^" + PositionY.ToString() + "^" + PositionZ.ToString() + "^" + Level.ToString() + "^"  + SaveItem1.ToString() + "^" + SaveItem2.ToString() + "^" + SaveItem3.ToString() + "^" + SaveItem4.ToString() + "^" + SaveItem5.ToString() + "^" + SaveItem6.ToString() + "^" + SaveItem7.ToString() + "^" + SaveItem8.ToString() + "^" + SaveItem9.ToString() + "^" + SaveItem10.ToString() + "^" + SaveItem11.ToString() + "^" + SaveItem12.ToString() + "^" + SaveItem13.ToString() + "^" + SaveItem14.ToString() + "^" + SaveItem15.ToString() + "^" + Savenum3Name;
			}
			
			WWWForm form = new WWWForm();
			form.AddField("user", user);
			form.AddField("SaveToUpdate", SaveToUpdate);
			form.AddField("DataToSave", DataToSave);
			
			WWW w = new WWW("http://ajreidgames.co.uk/Pirate_Game/Save.php", form);
			StartCoroutine(SaveGame(w));
			
			SavingBox = true;
			Save = false;
			Movement.inInventory = false;
			selectedSave = false;
		}
		if(selectionState == 1){
			Save = false;
			Movement.inInventory = false;
			selectedSave = false;

		}
		if(ConfirmSave == 1){
			Save = false;
			Movement.inInventory = false;
			selectedSave = false;

		}
		
	}
	
	
	IEnumerator SaveGame (WWW w)
	{
		yield return w;
		print(w.text);
		if(w.error == null)
		{
			Save = false;
			SavingBox = false;
			SavedBox = true;
			Movement.inInventory = true;

		}
		
	}
}
