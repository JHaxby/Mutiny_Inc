using UnityEngine;
using System.Collections;

public class ItemBehaviour : MonoBehaviour {
	
	public int ItemID;
	private RaycastHit hit;
	public bool Exist;

	
	void Awake()
	{
		
	}
	
	void Update(){
		if(Input.GetMouseButtonUp(0))
		{
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				if(Physics.Raycast(Camera.main.transform.position, ray.direction, out hit, 1000))
				{
					if(hit.transform.gameObject == this.transform.gameObject)
					{
		   		   		Inventory.ItemToBagRequest = ItemID;
						Destroy(gameObject);
					}
				}
		}

		
	}
	
}
