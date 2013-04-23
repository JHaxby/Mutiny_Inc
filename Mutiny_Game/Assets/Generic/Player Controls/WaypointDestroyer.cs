using UnityEngine;
using System.Collections;

public class WaypointDestroyer : MonoBehaviour {
	
	GameObject Player;
	
	void Start(){
		
		GameObject FoundWaypoint =  GameObject.FindGameObjectWithTag("Waypoint");
		if(FoundWaypoint != null){
			Destroy(FoundWaypoint);
		}
		this.tag = "Waypoint";
		Player = GameObject.FindGameObjectWithTag("Player");

	}
	
	void Update(){
			
		if(Vector3.Distance(transform.position, Player.transform.position) < 1.5){
			Destroy(this.gameObject);	
		}
		
	}
}
