using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour {

	//Window Settings//
	public float invOffset = 25;
	private float buttonBorder;
	private float buttonOffset;
	
	private int selectionState = -1;
	public int SelectedItem = 0;
	
	private int inventory_ID = 0;
	private Rect inventoryRect;
	public static bool menuOn = false;
	
	//Inventory Items//
	static public int ItemToBagRequest = 0;
	public GameObject[] itemHolder;
	static public int[] inBagList = new int[] {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};
	static public string[] ItemDescriptions;
	public Texture2D EmptyTex;
	public Texture2D[] itemsForInventory;                   
	static public Texture2D[] inventoryItemsPictures;
	
	public bool DetailPane;
	public int DetailToShow;
	
	public float Variable;
	
	public GUISkin InventorySkin;
	public bool Combine = false;
	
	public int ChosenCombine;
	public string[] CombineableItems;
	public string thiscombineitem;
	private string ChosenItemName;
	private string InventoryTitle = "Inventory";
	private string CombineString;

	/*
	Item 0 = Rope (id:1)
	Item 1 = Coconut (id:2)
	Item 2 = Cococnut-Rope (id:3)
	Item 3 = Planks (id:4)
	Item 4 = Nails (id:5)
	Item 5 = Message in a bottle (id:6)
	Item 6 = Map (id:7)
	Item 7 = Oil Container (id:8)   
	Item 8 = Oil container + Oil (id:9)
	Item 9 = Chef's Hat (id:10)
	Item 10 = Tea cup (id:11)
 	Item 11 = Bag o' sugar (id:12)
	Item 12 = Empty bag o' sugar (id:13)
	Item 13 = Moded Bag o' sugar (id:14)
	Item 14 = Broom (id:15)
	Item 15 = Top Hat (id:16)
	Item 16 = Note 1 (id:17)
	Item 17 = Note 2 (id:18)
	Item 18 = Note 3 (id:19)
	Item 19 = Combined Note (id:20)
	Item 20 = Big Firework (id:21)
	Item 21 = Torch (id:22)

	*/
	
	void Awake(){
		
		inventoryItemsPictures = new Texture2D[15];
		ItemDescriptions = new string[21];

		ItemDescriptions[0] = "EMPTY!";
		ItemDescriptions[1] = "A ROPE!";
		ItemDescriptions[2] = "A COCONUT!";
		ItemDescriptions[3] = "COCONT ROPE.... with an anime placeholder";
		ItemDescriptions[4] = "EMPTY!";
		ItemDescriptions[5] = "EMPTY!";
		ItemDescriptions[6] = "EMPTY!";
		ItemDescriptions[7] = "EMPTY!";
		ItemDescriptions[8] = "EMPTY!";
		ItemDescriptions[9] = "EMPTY!";
		ItemDescriptions[10] = "EMPTY!";
		ItemDescriptions[11] = "EMPTY!";
		ItemDescriptions[12] = "EMPTY!";
		ItemDescriptions[13] = "EMPTY!";
		ItemDescriptions[14] = "EMPTY!";
		ItemDescriptions[15] = "EMPTY!";
		ItemDescriptions[16] = "EMPTY!";
		ItemDescriptions[17] = "EMPTY!";
		ItemDescriptions[18] = "EMPTY!";
		ItemDescriptions[19] = "EMPTY!";
		ItemDescriptions[20] = "EMPTY!";
			
		//reset inventory to 0//
		for(int i = 0; i < 15; i++){
			inventoryItemsPictures[i] = EmptyTex;
			inBagList[i] = 0;
		}	
	}
	
	void Start(){
		for(int i = 0; i < 15; i++){
			inBagList[i] = FloatingInventory.HeldInv[i];
			if(FloatingInventory.HeldInvTex[i] != null){
				inventoryItemsPictures[i] = FloatingInventory.HeldInvTex[i];
			}else{
				inventoryItemsPictures[i] = EmptyTex;
			}
			
		}	
	}
	
	void Update(){
		
		if(ItemToBagRequest !=0){
			for(int i = 0; i < 15; i++){				
				if(inBagList[i] == 0){
					inBagList[i] = ItemToBagRequest;
					inventoryItemsPictures[i] = itemsForInventory[ItemToBagRequest];
					ItemToBagRequest = 0;

					FloatingInventory.HeldInv[i] = inBagList[i];
					FloatingInventory.HeldInvTex[i] = inventoryItemsPictures[i];

					break;
				}
			}
		}
		
	}
	
	void OnGUI(){
		
		float ResW = Screen.currentResolution.width;
		float ResH = Screen.currentResolution.height;
			
		GUI.matrix = Matrix4x4.TRS( Vector3.zero, Quaternion.identity, new Vector3( Screen.width / ResW, Screen.height / ResH, 1.0f ) );
		GUI.skin = InventorySkin;

		if(menuOn == false){
			if(GUI.Button(new Rect(30,30,150,80),"Inventory")){
				menuOn = true;
			}
		}
		
		if(menuOn == true){
			Movement.inInventory = true;
			inventoryRect = GUI.Window(inventory_ID, new Rect(10,10, ResW - 20, ResH - 20), InventoryWindow, InventoryTitle);
		}else{
			Movement.inInventory = false;
		}
		   
	}

	private void InventoryWindow(int id)
	{
		float ResW = Screen.currentResolution.width;
		float ResH = Screen.currentResolution.height;
			
		GUI.matrix = Matrix4x4.TRS( Vector3.zero, Quaternion.identity, new Vector3( Screen.width / ResW, Screen.height / ResH, 1.0f ) );
		
		GUI.skin = InventorySkin;
		
		//GUI.FocusWindow(0);
		
		if(GUI.Button(new Rect(ResW - 100, 15 ,70 ,70 ),"X")){
			menuOn = false;
			DetailPane = false;
			Combine = false;
		}
		selectionState = -1;
		ChosenCombine = -1;
		if(!DetailPane){
			if(!Combine){
				InventoryTitle = "Inventory";
				selectionState = GUI.SelectionGrid(new Rect(10,110,ResW - 40,ResH - 140 ),selectionState,inventoryItemsPictures,5);
				if(selectionState != -1){
					if(inBagList[selectionState] != 0)
					{
						SelectedItem = inBagList[selectionState] ;
						DetailPane = true;
											
						thiscombineitem = itemHolder[SelectedItem-1].name;
					}
				}
			}else{
				InventoryTitle = "Combine Items";
				ChosenCombine = GUI.SelectionGrid(new Rect(10,110,ResW - 40,ResH - 140 ),ChosenCombine,inventoryItemsPictures,5);
				if(ChosenCombine != -1){
					if(inBagList[ChosenCombine] != 0)
					{
						ChosenCombine = inBagList[ChosenCombine] ;
						ChosenItemName = itemHolder[ChosenCombine-1].name;
						

						CombineString = ChosenItemName+thiscombineitem;
						print(CombineString);
						CheckCombo();
					}
				}
				
				if(GUI.Button(new Rect(20, 15 ,180 ,70 ),"<---")){
					DetailPane = true;
					Combine = false;
				}
				inventoryRect = GUI.Window(inventory_ID, new Rect(10,10, ResW - 20, ResH - 20), InventoryWindow, InventoryTitle);
			}
		}else{
			InventoryTitle = "Item Details";

			if(GUI.Button(new Rect(20, 15 ,180 ,70 ),"<---")){
				DetailPane = false;
				Combine = false;
			}
			GUI.skin = InventorySkin;

			GUI.DrawTexture(new Rect(20,130 ,ResW - (ResW/2) ,ResH - (ResH/3)),itemsForInventory[SelectedItem], ScaleMode.StretchToFill, true, 10.0F);
			GUI.Box(new Rect(20, 600 ,ResW - 65 ,ResH - 648 ),"Description: \n" + ItemDescriptions[SelectedItem]);
			if(GUI.Button(new Rect(ResW/2 + 20, 130 ,ResW/2 - 60,100),"Use"))
			{
				//this will take you back to the main screen. a raycast will then return the .name of the next thing clickd and if that name is a stored variable within the item it will do something (depending on the item)
				
			}
			if(GUI.Button(new Rect(ResW/2 + 20, 280 ,ResW/2 - 60 ,100),"Combine"))
			{
				DetailPane = false;
				//mouse pointer = item.
				Combine = true;
			}

			
		}
		
		
	}
	
	
	void CheckCombo()
	{
	
		// just have a loop for every item that can be combined (there arn't that many)
		
		if(CombineString == "CoconutRope" || CombineString == "RopeCoconut"  )
		{
			bool onecoconut = false;
			bool onerope = false;
			
			for(int i = 0; i<15; i++)
			{
				if(inBagList[i] == 1 && onecoconut == false)
				{
					inBagList[i] = 0;
					inventoryItemsPictures[i] = EmptyTex;
					onecoconut = true;
				}
				if(inBagList[i] == 2 && onerope == false)
				{
					inBagList[i] = 0;
					inventoryItemsPictures[i] = EmptyTex;
					onerope = true;
				}
			}
			ItemToBagRequest =3;
			FloatingInventory.TimeForFloatUpdate = true;
			DetailPane = false;
			Combine = false;
		}
		
	}
}
