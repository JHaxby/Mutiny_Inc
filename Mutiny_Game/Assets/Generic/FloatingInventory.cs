using UnityEngine;
using System.Collections;

public class FloatingInventory : MonoBehaviour {
	
	public GameObject others;
	static public int[] HeldInv = new int[] {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};
	static public Texture2D[] HeldInvTex = new Texture2D[15];
	static public bool NeedBagUpdate = false;
	static public bool TimeForFloatUpdate = false;
	
	public static bool OnLog = false;


		
	void Awake(){
		this.gameObject.tag = "";
		others = GameObject.FindGameObjectWithTag("Inventory");
		Destroy(others);
		this.gameObject.tag = "Inventory";
				
		DontDestroyOnLoad(transform.gameObject);
		
	}
	
	void Update(){
		
		if(OnLog == true){
			StartBagUpdate();	
		}
		if(NeedBagUpdate == true){
			UpdateBag();	
		}
		if(TimeForFloatUpdate == true){
			FloatUpdate();	
		}
		
	}
	
	
	void StartBagUpdate(){
		
		for(int i = 0; i< 15; i++)
		{ 
				//Inventory.inventoryItemsPictures[i] = HeldInvTex[1];
		}
		
	}
	
	void UpdateBag(){
	
		for(int i = 0; i < 15; i++){
			Inventory.inBagList[i] = HeldInv[i];
			Inventory.inventoryItemsPictures[i] = HeldInvTex[i];
		}
		
		NeedBagUpdate = false;
	}
	void FloatUpdate(){
		for(int i = 0; i < 15; i++){
			HeldInv[i] = Inventory.inBagList[i];
			HeldInvTex[i] = Inventory.inventoryItemsPictures[i];
		}
		
		TimeForFloatUpdate = false;
	}
}
