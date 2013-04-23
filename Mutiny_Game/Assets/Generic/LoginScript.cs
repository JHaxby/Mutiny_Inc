using UnityEngine;
using System.Collections;

public class LoginScript : MonoBehaviour {

	public static string user = "";
	public static string email = "";
	public static string password = "", rePass = "", errorMess = "";
	
	public static int SelectedSave = -1;
	
	private bool registration = false;
	private bool InGame = false;
	private bool successfulLogin = false;
	private bool InLevelSelect = false;
	
	public GameObject others;
	
	public static string PlayerInfoString;
	public static string ChosenSave;
	public static string parsedInts;
	public static int[] SelectedDataInventory = new int[15];
	
	public static float playerxpos = 0.0f, playerypos = 0.0f, playerzpos = 0.0f;

	public static int sLevel = 0;
	public static string Savenum1Name = "";
	public static string Savenum2Name = "";
	public static string Savenum3Name = "";
	public static string SaveDetails;
	public string Save1Data;
	public string Save2Data;
	public string Save3Data;
	
	private string[] SaveData1Packets;
	private string[] SaveData2Packets;
	private string[] SaveData3Packets;
	
	public string[] DataSplit;
	public string[] PlayerSaves;
	public string[] ChosenSaveDataSplit;
	public static string[] ChosenSaveNames;
	
	public Texture2D[] itemsForInventory;                   
	
	public static bool NeedToPositionPlayer = true;
	
	private bool ErrorLogin = false;
	
	public GameObject Player;
	
	public bool rememberme;
	private bool rememberbutton;
	
	public GUISkin LoginSkin;
	public GUIStyle Style1;
	
	public void Awake()
	{
		this.gameObject.tag = "";
		others = GameObject.FindGameObjectWithTag("Login");
		Destroy(others);
		this.gameObject.tag = "Login";
		
		DontDestroyOnLoad(transform.gameObject);
		
		if(PlayerPrefs.GetInt("RememberMe") == 1)
		{	
			rememberme = true;	
		}else{
			rememberme = false;	
		}
		
										
		if(rememberme)
				{
					user = PlayerPrefs.GetString("Player Name");
					password = PlayerPrefs.GetString("Player Password");
					PlayerPrefs.SetInt("RememberMe", 1);
				}else{
					user = "";
					password = "";
					PlayerPrefs.SetInt("RememberMe", 0);
				}
				
	}
	
	public void OnLevelWasLoaded()
	{
	}
	
	private void OnGUI()
	{
		
		if(!InGame)
		{
			Screen.fullScreen = true;
			float ResW = Screen.currentResolution.width;
			float ResH = Screen.currentResolution.height;
			
			GUI.matrix = Matrix4x4.TRS( Vector3.zero, Quaternion.identity, new Vector3( Screen.width / ResW, Screen.height / ResH, 1.0f ) );
			
			GUI.skin = LoginSkin;

 			//Screen.SetResolution (800, 480, true);
			if(registration == true && InLevelSelect == false)
			{
				if(errorMess != "")
				{
					GUILayout.Box(errorMess);
				}
				
				//GUILayout.BeginArea (new Rect((Screen.width/2)-(Screen.width/2)/2, (Screen.height/2)-(Screen.height/2)/1.5f ,(Screen.width/2) , (Screen.height/1.5f)));
				GUILayout.BeginArea (new Rect((Screen.width/2)- 200,(Screen.height/100)* 10, 400,Screen.height));
				GUILayout.Label("Username",GUILayout.Height((Screen.height/ 100) * 10));
				user = GUILayout.TextField(user,GUILayout.Height((Screen.height/ 100) * 10));
				GUILayout.Label("Email Address",GUILayout.Height((Screen.height/ 100) * 10));
				email = GUILayout.TextField(email,GUILayout.Height((Screen.height/ 100) * 10));
				GUILayout.Label("Password",GUILayout.Height((Screen.height/ 100) * 10));
				password = GUILayout.PasswordField(password, "*"[0],GUILayout.Height((Screen.height/ 100) * 10));
				GUILayout.Label("Re-password",GUILayout.Height((Screen.height/ 100) * 10));
				rePass = GUILayout.PasswordField(rePass, "*"[0],GUILayout.Height((Screen.height/ 100) * 10));

				GUILayout.BeginHorizontal();
				if(GUILayout.Button("Back",GUILayout.Height((Screen.height/ 100) * 10)))
				{
					registration = false;
				}
				
				if(GUILayout.Button("Register",GUILayout.Height((Screen.height/ 100) * 10)))
				{
					if(user == "" || email == "" || password == ""){
						errorMess += "Please insure all fields are filled in \n";
					}else{
						if(password == rePass)
						{
							WWWForm form = new WWWForm();
							form.AddField("user", user);
							form.AddField("email", email);
							form.AddField("pass", password);
							WWW w = new WWW("http://ajreidgames.co.uk/Pirate_Game/Register%20-%20Email.php", form);
							StartCoroutine(register(w));
						}else{
							errorMess += "Passwords do not match, Please try again \n";	
						}
					}
				}
				GUILayout.EndHorizontal();
				GUILayout.EndArea ();

			}else if(InLevelSelect == false){
				if(errorMess != "")
				{
					GUILayout.Box(errorMess);
				}
				GUILayout.BeginArea (new Rect((Screen.width/2)-(Screen.width/2)/2, (Screen.height/2)-(Screen.height/2)/1.5f ,(Screen.width/2) , (Screen.height/1.5f)));

				GUILayout.Label("Username:", GUILayout.Height((Screen.height/ 100) * 10));
				user = GUILayout.TextField(user,GUILayout.Height((Screen.height/ 100) * 10));
				GUILayout.Label("Password:",GUILayout.Height((Screen.height/ 100) * 10));
				password = GUILayout.PasswordField(password, "*" [0], GUILayout.Height((Screen.height/ 100) * 10));
				GUILayout.BeginHorizontal();
				rememberme = GUILayout.Toggle(rememberme, "Remember me",GUILayout.Height((Screen.height/ 100) * 15));
				GUI.skin = null;
				if(GUILayout.Button("Forgotten Password?", GUILayout.Height(40), GUILayout.Width(150)))
				{
					WWWForm form = new WWWForm();
					form.AddField("user", user);
					form.AddField("email", email);
					form.AddField("pass", password);
					WWW w = new WWW("http://ajreidgames.co.uk/Pirate_Game/ForgottenPassword.php", form);
					errorMess += "reminder sent to your email address";
				}
				

				GUILayout.EndHorizontal();
				GUILayout.BeginHorizontal();
				GUI.skin = LoginSkin;
				if(GUILayout.Button("Login",GUILayout.Height((Screen.height/ 100) * 10)))
				{
					errorMess = "";
					if(user == "" || password == "")
					{
						errorMess += "Please insure all fields are filled in \n";
					}else{
						WWWForm form = new WWWForm();
						form.AddField("user", user);
						form.AddField("pass", password);
						WWW w = new WWW("http://ajreidgames.co.uk/Pirate_Game/Login.php", form);
						StartCoroutine(login(w));
					}
	
				}
				
				if(GUILayout.Button("Register",GUILayout.Height((Screen.height/ 100) * 10)))
				{
					registration = true;
				}
				GUILayout.EndHorizontal();
				GUILayout.EndArea ();

			}
			
			if(InLevelSelect)
			{
				ErrorLogin = false;
				
				GUILayout.BeginArea (new Rect((Screen.width/2)-(Screen.width/2)/2, (Screen.height/2)-(Screen.height/2)/1.8f ,(Screen.width/2) , (Screen.height/1.5f)));

				if(GUILayout.Button(Savenum1Name,GUILayout.Height((Screen.height/ 100) * 20)))
					{
						SelectedSave = 1;
						WWWForm form = new WWWForm();
						form.AddField("user", user);
						form.AddField("pass", password);
						WWW w = new WWW("http://ajreidgames.co.uk/Pirate_Game/Login.php", form);
						SavingScript.SaveToUpdate = 4;
						StartCoroutine(loadSave(w));
					}
				
				 GUILayout.Space (20);
				
				if(GUILayout.Button(Savenum2Name,GUILayout.Height((Screen.height/ 100) * 20)))
					{
						SelectedSave = 2;
						WWWForm form = new WWWForm();
						form.AddField("user", user);
						form.AddField("pass", password);
						WWW w = new WWW("http://ajreidgames.co.uk/Pirate_Game/Login.php", form);
						SavingScript.SaveToUpdate = 5;
						StartCoroutine(loadSave(w));
					}
				
				 GUILayout.Space (20);
				if(GUILayout.Button(Savenum3Name,GUILayout.Height((Screen.height/ 100) * 20)))
					{
						SelectedSave = 3;
						WWWForm form = new WWWForm();
						form.AddField("user", user);
						form.AddField("pass", password);
						WWW w = new WWW("http://ajreidgames.co.uk/Pirate_Game/Login.php", form);
						SavingScript.SaveToUpdate = 6;
						StartCoroutine(loadSave(w));
					}	
					GUILayout.EndArea ();

				}
			
			if(ErrorLogin)
			{
				GUILayout.Box("Username/Password is Incorrect");
			}
			
		}
	}

	
	void OnApplicationQuit()
	{
			if(rememberme)
				{
					PlayerPrefs.SetString("Player Name", user);
					PlayerPrefs.SetString("Player Password", password);
					PlayerPrefs.SetInt("RememberMe", 1);
				}else{
					PlayerPrefs.SetString("Player Name", "");
					PlayerPrefs.SetString("Player Password", "");
					PlayerPrefs.SetInt("RememberMe", 0);
			}
	}
	
	IEnumerator login (WWW w)
	{
		yield return w;
		if(w.text.Contains("Username") || w.text.Contains("Password"))
		{
			ErrorLogin = true;
		}else{
			InLevelSelect = true;
			PlayerInfoString = w.text;
			DisectData();
		}	
	}
	
	void DisectData()
	{
		DataSplit = PlayerInfoString.Split("|"[0]);
		Save1Data= DataSplit[0];
		SaveData1Packets = Save1Data.Split("^"[0]);
		Save2Data= DataSplit[1];
		SaveData2Packets = Save2Data.Split("^"[0]);
		Save3Data= DataSplit[2];
		SaveData3Packets = Save3Data.Split("^"[0]);
		
		Savenum1Name = SaveData1Packets[19];
		Savenum2Name = SaveData2Packets[19];
		Savenum3Name = SaveData3Packets[19];
		
	}
	
	IEnumerator loadSave (WWW w)
	{
		yield return w;
		if(w.error == null)
		{
				SelectedSave = SelectedSave - 1;
			
				if(SelectedSave == 0){
					sLevel = int.Parse(SaveData1Packets[3]);
					for(int i = 0; i < 15; i++) 
					{
						SelectedDataInventory[i] = int.Parse(SaveData1Packets[i+4]);
						FloatingInventory.HeldInv[i] = SelectedDataInventory[i];
						int looper = SelectedDataInventory[i];
						FloatingInventory.HeldInvTex[i] = itemsForInventory[looper];
						FloatingInventory.NeedBagUpdate = true;
						successfulLogin = true;
					}
				
						playerxpos = float.Parse(SaveData1Packets[0]);
						playerypos = float.Parse(SaveData1Packets[1]);
						playerzpos = float.Parse(SaveData1Packets[2]);
				
						if(playerxpos == 0 && playerypos == 0 && playerzpos == 0)
						{
							playerxpos = 1.17f;
							playerypos = -0.23f;
							playerzpos = 0.2f;
						}
				
				}else if(SelectedSave == 1){
					sLevel = int.Parse(SaveData2Packets[3]);
				
					for(int i = 0; i < 15; i++) 
					{
						SelectedDataInventory[i] = int.Parse(SaveData2Packets[i+4]);
						FloatingInventory.HeldInv[i] = SelectedDataInventory[i];
						int looper = SelectedDataInventory[i];
						FloatingInventory.HeldInvTex[i] = itemsForInventory[looper];
						FloatingInventory.NeedBagUpdate = true;
						successfulLogin = true;
					}
				
					playerxpos = float.Parse(SaveData2Packets[0]);
					playerypos = float.Parse(SaveData2Packets[1]);
					playerzpos = float.Parse(SaveData2Packets[2]);
				
					if(playerxpos == 0 && playerypos == 0 && playerzpos == 0)
						{
							playerxpos = 1.17f;
							playerypos = -0.23f;
							playerzpos = 0.2f;
						}
				}else{
					sLevel = int.Parse(SaveData3Packets[3]);
				
					for(int i = 0; i < 15; i++) 
					{
						SelectedDataInventory[i] = int.Parse(SaveData3Packets[i+4]);
						FloatingInventory.HeldInv[i] = SelectedDataInventory[i];
						int looper = SelectedDataInventory[i];
						FloatingInventory.HeldInvTex[i] = itemsForInventory[looper];
						FloatingInventory.NeedBagUpdate = true;
						successfulLogin = true;
					}
				
					playerxpos = float.Parse(SaveData3Packets[0]);
					playerypos = float.Parse(SaveData3Packets[1]);
					playerzpos = float.Parse(SaveData3Packets[2]);
				
					if(playerxpos == 0 && playerypos == 0 && playerzpos == 0)
						{
							playerxpos = 1.17f;
							playerypos = -0.23f;
							playerzpos = 0.2f;
						}
				}


			
			if(successfulLogin == true)
			{
				FloatingInventory.OnLog = true;
				Application.LoadLevel(sLevel);
				InGame = true;
				successfulLogin = false;
				
			}
			errorMess += w.text;
		}else{
			errorMess += "ERROR: " + w.error + "\n";
		}
				SelectedSave = SelectedSave + 1;

	}

	IEnumerator register(WWW w)
	{
		yield return w;	
		if(w.error == null)
		{
			errorMess += w.text;
		}else{
			errorMess += "ERROR: " + w.error + "\n";
		}
	}
	
	IEnumerator save (WWW w)
	{
		yield return w;
		//if(w.error == null)
	}
	
	IEnumerator load (WWW w)
	{
		yield return w;
		//if(w.error == null)
	}
}
