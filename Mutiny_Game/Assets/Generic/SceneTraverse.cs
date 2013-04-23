using UnityEngine;
using System.Collections;

public class SceneTraverse : MonoBehaviour {
	
	private RaycastHit hit;
	public string LevelName;
	private GameObject Player;
	
	private bool menuOn = false;
	private int SceneChangeWindow_ID = 0;
	private Rect SceneRect = new Rect(0,0,0,0);
	private int selectionState = -1;
	private string[] selectionString = new string[]{"Yes", "No"};
	
	static public bool NeedFloatingUpdate = false;


	
	void Awake(){
	
		Player = GameObject.FindGameObjectWithTag("Player");		
		
	}
	
	void OnGUI(){

		if (menuOn == true){
			SceneRect = GUI.Window(SceneChangeWindow_ID, new Rect((Screen.width/2) - 170, (Screen.height/2) - 60, 340, 120),SceneChangeWindow, "Travel to " + LevelName + " ?");
		}
		
	}

	
	void Update(){
		
		if(Input.GetMouseButtonUp(0)){
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				if(Physics.Raycast(Camera.main.transform.position, ray.direction, out hit, 1000))
				{
					if(hit.transform.gameObject == this.transform.gameObject){
					menuOn = true;	
					}
				}
		}
		if(NeedFloatingUpdate == true){
		
			UpdateFloat();
			
		}
		
	}
	
	private void SceneChangeWindow(int id)
	{
		selectionState = -1;
		selectionState = GUI.SelectionGrid(new Rect(10,30,320,80),selectionState,selectionString,2);
		
		if(selectionState == 0){
			NeedFloatingUpdate = true;
			Application.LoadLevel(LevelName);  
		}
		if(selectionState == 1){
			menuOn = false;	
		}
		
	}
	
	void UpdateFloat(){
		
		FloatingInventory.TimeForFloatUpdate = true;
		NeedFloatingUpdate = false;
		
	}
			
}