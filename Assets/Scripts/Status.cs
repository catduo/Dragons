using UnityEngine;
using System.Collections;

public class Status : MonoBehaviour, IJoviosControllerListener {

	public int playerNumber;
	public JoviosUserID myPlayer;
	public Color primary;
	public Color secondary;
	public string playerName;
	public string playerCharacter;
	public TextMesh status;
	public int chosenArena = 1;
	

	private Transform fire;
	private Transform rightEye;
	private Transform leftEye;
	private Transform rightGill;
	private Transform leftGill;
	private Transform snout;
	private Transform head;
	private Transform segment1;
	
	public GameObject playerObject;
	
	public bool is_ready = false;
	private Jovios jovios;

	// Use this for initialization
	void Start () {
		jovios = GameManager.jovios;
		segment1 = transform.FindChild("Segment");
		status = transform.FindChild("Status").GetComponent<TextMesh>();
		status.color = Color.red;
		status.text = "X";
		JoviosControllerStyle controllerStyle = new JoviosControllerStyle();
		MenuManager.SetControls(jovios.GetPlayer(myPlayer).GetUserID(), ControllerStyles.PlayAgain);
		if(!GameManager.score.ContainsKey(myPlayer.GetIDNumber())){
			GameManager.score.Add(jovios.GetPlayer(myPlayer).GetUserID().GetIDNumber(), 0);
		}
		jovios.AddControllerListener(this, myPlayer);
	}
	
	public void SetMyPlayer (JoviosPlayer playerInfo){
		myPlayer = playerInfo.GetUserID();
		playerNumber = playerInfo.GetPlayerNumber();
		primary = playerInfo.GetColor("primary");
		secondary = playerInfo.GetColor("secondary");
		playerName = playerInfo.GetPlayerName();
		if(playerName.Length>0){
			playerCharacter = playerName[0].ToString();
		}
		else{
			playerCharacter = "";
		}
		segment1 = transform.FindChild("Segment");
		status = transform.FindChild("Status").GetComponent<TextMesh>();
		segment1.renderer.material.color = primary;
		segment1.FindChild("Character").GetComponent<TextMesh>().color = secondary;
		segment1.FindChild("Character").GetComponent<TextMesh>().text = playerCharacter;
		if(playerInfo.GetPlayerObject(0) != null){
			jovios.GetPlayer(myPlayer).GetPlayerObject().GetComponent<Dragon>().SetMyPlayer(playerInfo);
		}
	}
	
	bool IJoviosControllerListener.ButtonEventReceived(JoviosButtonEvent e){
		if(e.GetAction() == "press"){
			OnButton(e.GetResponse());
		}
		return false;
	}

	void OnButton (string button){
		JoviosControllerStyle controllerStyle = new JoviosControllerStyle();
		switch(button){
			
		case "Play":
			switch(MenuManager.gameState){
			case GameState.Countdown:
				Ready ();
				StartRound();
				break;
				
			case GameState.ChooseArena:
				Ready ();
				MenuManager.SetControls(myPlayer, ControllerStyles.Snake);
				jovios.SetControls(myPlayer, controllerStyle);
				GameManager.ChooseArena(1);
				break;
				
			case GameState.GameOn:
				Ready ();
				StartRound();
				break;
				
			case GameState.GameEnd:
				if(MenuManager.gameState != GameState.ChooseArena && MenuManager.gameState != GameState.Countdown){
					GameManager.EndRound();
				}
				Ready ();
				MenuManager.SetControls(myPlayer, ControllerStyles.Snake);
				GameManager.ChooseArena(1);
				break;
				
			case GameState.Menu:
				break;
			}
			break;
			
		case "Attack":
			jovios.GetPlayer(myPlayer).GetPlayerObject().GetComponent<Dragon>().Attack();
			break;
			
		default:
			Debug.Log (button);
			break;
		}
	}
	
	public void Ready(){
		status.text = "O";
		status.color = Color.green;
		if(jovios.GetPlayer(myPlayer).GetPlayerObject(0) == null){
			GameObject newPlayerObject = (GameObject) GameObject.Instantiate(playerObject, new Vector3(0,-4,0), Quaternion.identity);
			newPlayerObject.transform.RotateAround(Vector3.zero, Vector3.forward, 360 - 360 / (playerNumber + 1) * jovios.GetPlayerCount());
			newPlayerObject.transform.Rotate(new Vector3(0, 0, - 360 + 360 / (playerNumber + 1) * jovios.GetPlayerCount()));
			newPlayerObject.transform.parent = GameObject.Find ("PlayerObjects").transform;
			newPlayerObject.SendMessage("SetMyPlayer", jovios.GetPlayer(myPlayer), SendMessageOptions.DontRequireReceiver);
			jovios.GetPlayer(myPlayer).AddPlayerObject(newPlayerObject);
		}
		is_ready = true;
	}
	
	public void StartRound(){
		status.text = "0";
		status.color = Color.white;
	}
	
	public void Reset(int newPlayerNumber){
		playerNumber = newPlayerNumber;
		if(jovios.GetPlayer(myPlayer).GetPlayerObject(0) != null){
			jovios.GetPlayer(myPlayer).GetPlayerObject(0).GetComponent<Dragon>().playerNumber = newPlayerNumber;
		}
		if(playerNumber < 4){
			transform.localPosition = new Vector3(-4.5F + (playerNumber -1) * 4, -1.75F, 0);
		}
		else{
			transform.localPosition = new Vector3(-4.5F + (playerNumber -5) * 4, -3F, 0);
		}
	}
}