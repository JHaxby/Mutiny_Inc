using UnityEngine;
using System.Collections;

public class RCC : MonoBehaviour {
		
	private RaycastHit hit;
	
	int FinalItemID;
	
	void Update () {
	if (Input.GetButtonDown ("Fire2")) 
	    		{  
	    		    Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
					if(Physics.Raycast(Camera.main.transform.position, ray.direction, out hit, 1000))
					{
	    			    if (hit.transform.tag == "Item")
	    			    {
							
	    				    Inventory.ItemToBagRequest = FinalItemID;
							Destroy(hit.transform.gameObject);
	    			    }
					}
	    	     }
		
	}
}
