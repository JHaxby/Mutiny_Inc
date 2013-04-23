using UnityEngine;
using System.Collections;

public class PreRegister : MonoBehaviour {

	public static bool PreReg = false;
	
	void Update () {
		if(PreReg)
		{
			WWWForm form = new WWWForm();
			form.AddField("user", LoginScript.user);
			form.AddField("email", LoginScript.email);
			form.AddField("password", LoginScript.password);
			WWW w = new WWW("http://jonathanhaxby.co.uk/public_html/Pirate_Game/Register.php", form);
			StartCoroutine(register(w));
		}
	}
	
	IEnumerator register(WWW w)
	{
		yield return w;	
		if(w.error == null)
		{
			LoginScript.errorMess += w.text;
			print("DONE!");
		}else{
			LoginScript.errorMess += "ERROR: " + w.error + "\n";
		}
	}
}
