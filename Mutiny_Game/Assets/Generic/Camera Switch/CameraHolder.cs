using UnityEngine;
using System.Collections;

public class CameraHolder : MonoBehaviour {

	public GameObject[] Cameras;
	public static int CurrentCam;
	public static int WantedCam;
	
	void Update(){
		if(CurrentCam != WantedCam)
		{
			SelectCamera(WantedCam);
		}

	}
	
	void SelectCamera (int Index) 
	{
		CurrentCam = 0;
		for (CurrentCam = 0; CurrentCam < Cameras.Length; CurrentCam++) 
			{
				if (CurrentCam == Index){
		       		Cameras[CurrentCam].active = true;
			    }else{
		        	Cameras[CurrentCam].active = false;
		    	}
			}
	}
}
