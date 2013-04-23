using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
		
	public float walkSpeed = 2.5F;
	private float gravity = 100.0F;
	private Vector3 moveDirection = Vector3.zero;
	private CharacterController charController;
	private bool move  = false;
	private RaycastHit hit;
	public Transform Waypoint;
	
	public static bool inInventory = false;
	public static bool MovementEnabled = true;
	
	private int ParsedSave;
	
		
void Start()
{
    charController = GetComponent<CharacterController>();
	
		if(LoginScript.NeedToPositionPlayer == true)
		{
			
			this.transform.position = new Vector3(LoginScript.playerxpos,LoginScript.playerypos,LoginScript.playerzpos);
			
			
			LoginScript.NeedToPositionPlayer = false;

			
		}
}
	void Update () 
	{
		if(MovementEnabled){
			if(inInventory == false){
				if (Input.GetButtonDown ("Fire1")) 
		    		{  
		    		    Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
						if(Physics.Raycast(Camera.main.transform.position, ray.direction, out hit, 1000))
						{
		    			    if(move = true)
							{
								animation.CrossFade("idle");
								move = false;
							}
						
							if (hit.transform.tag == "Floor")
		    			    {
		    				    move = true;
		    			    }
						}else{
							animation.CrossFade("idle");
							move = false;
						}
		    	    }
		    	     
				if(move) 
		    	{ 
					if (hit.transform.tag == "Floor")
		    		{
						Instantiate(Waypoint, new Vector3(hit.point.x, transform.position.y, hit.point.z), transform.rotation);
					}else{

					}
			        Vector3 wantedPosition= new Vector3(hit.point.x, transform.position.y, hit.point.z);
					GameObject NewWaypoint =  GameObject.FindGameObjectWithTag("Waypoint");
			        if(NewWaypoint != null){
						transform.LookAt(NewWaypoint.transform);
						animation.CrossFade("walk");
				 		moveDirection = new Vector3(0, 0, Mathf.Abs(hit.point.z)).normalized;
				 		moveDirection = transform.TransformDirection(moveDirection);
				      	charController.Move(moveDirection * (Time.deltaTime * walkSpeed));
					}
					if(Vector3.Distance(transform.position, wantedPosition) < 0.3) 
		        {
					move = false;
		           animation.CrossFade("idle");
		        } 
		    } 
			moveDirection.y -= gravity * Time.deltaTime;
		    moveDirection.x = 0;
		    moveDirection.z = 0;
		    charController.Move(moveDirection * (Time.deltaTime * walkSpeed));	
				
			
			}else{
				animation.CrossFade("idle");	
			}
		}
	}
	
	/*void OnControllerColliderHit (ControllerColliderHit other)
	{
		if (other.gameObject.tag != "Floor")
		{
			Debug.Log ("Collided");
			move = false;
			//animation.CrossFade("idle");
		}
	}
	*/
}
