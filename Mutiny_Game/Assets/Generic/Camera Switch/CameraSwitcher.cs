using UnityEngine;
using System.Collections;

public class CameraSwitcher : MonoBehaviour {
	
	public int AsignedCamera;
	
	void OnTriggerEnter(Collider other) 
	{
		if(other.tag == "Player"){	
			CameraHolder.WantedCam = AsignedCamera;
		}
	}
}
