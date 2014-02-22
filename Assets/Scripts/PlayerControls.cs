using UnityEngine;
using System.Collections;

public enum TouchStyle{
	Joystick,
	DPad,
	DiagonalDPad,
	Button1,
	Button2,
	Button3Up,
	Button3UpHold
}

public class PlayerControls : MonoBehaviour {
	/*
	static public Transform[] playerObjects = new Transform[0];
	static public Transform[] statusObjects = new Transform[0];
	static public bool is_gameOn = false;
	static public int controllingPlayer = 0;
	
	public static GameObject chosenArena;
	public GameObject chooseArena;
	public GameObject arena1;
	public GameObject arena2;
	public GameObject arena3;
	public GameObject arena4;
	
	[RPC] void SentJoystick(int player, float vertical, float horizontal, string side){
		if(is_gameOn){
			playerObjects[player].GetComponent<Dragon>().direction = new Vector2(horizontal, vertical);
		}
	}
	
	[RPC] void SentDPad(int player, float vertical, float horizontal, string side){
	}
	
	[RPC] void SentDiagonalDPad(int player, float vertical, float horizontal, string side){
	}
	
	[RPC] void SentButton1(int player, string buttonPress, string side){
		if(is_gameOn){
			playerObjects[player].GetComponent<Dragon>().Attack();
		}
	}
	
	[RPC] void SentButton2(int player, string buttonPress, string side){
	}
	
	[RPC] void SentButton3Up(int player, string buttonPress, string side){
	}
	
	[RPC] void SentButton3UpHold(int player, string buttonPress, float holdTime, string side){
	}
	
	[RPC] public void InstantiatePlayerObject(int player, float primaryR, float primaryG, float primaryB, float secondaryR, float secondaryG, float secondaryB, string playerName){
		Color primary = new Color(primaryR, primaryG, primaryB, 1);
		Color secondary = new Color(secondaryR, secondaryG, secondaryB, 1);
		if(playerObjects[player] != null){
			playerObjects[player].GetComponent<Dragon>().SetMyPlayer(player, primary, secondary, playerName);
		}
		statusObjects[player].GetComponent<Status>().SetMyPlayer(player, primary, secondary, playerName);
	}
	
	
	[RPC] public void SetPlayerNumber(int player){}
	[RPC] public void SetControls(int lControls, string lControlsDescription, int rControls, string rControlsDescription){}
	[RPC] void PlayerObjectCreated(){}
	[RPC] void EndOfRound(int player){}
	[RPC] void NewGame(){}
	[RPC] void SentBasicButtonTap(int playerNumber, string button){
		switch(button){
		case "Join Game":
			statusObjects[playerNumber].GetComponent<Status>().status.text = "O";
			statusObjects[playerNumber].GetComponent<Status>().status.color = Color.green;
			SentBasicButtons("Start Game", "Hit Start Game to Begin the Round", NetworkManager.playerList[playerNumber]);
			transform.GetComponent<NetworkManager>().PlayerReady(playerNumber);
			transform.GetComponent<NetworkManager>().StartRound();
			break;
			
		case "Start Game":
			statusObjects[playerNumber].GetComponent<Status>().status.text = "O";
			statusObjects[playerNumber].GetComponent<Status>().status.color = Color.green;
			MenuManager.is_countdown = true;
			MenuManager.is_choosingArena = false;
			MenuManager.lastTickTime = Time.time;
			transform.GetComponent<NetworkManager>().StartRound();
			break;
			
		case "Play Again!":
			SentBasicButtonTap(playerNumber, "Join Game");
			break;
			
		case "Play a Different Game":
			SentBasicButtons("Sumo", "Dragons", "What game would you like to play?", NetworkManager.playerList[playerNumber]);
			break;
			
		case "Sumo":
			transform.GetComponent<NetworkManager>().NewUrl("http://jovios.com/sumo/game.unity3d");
			break;
			
		case "Dragons":
			transform.GetComponent<NetworkManager>().NewUrl("http://jovios.com/dragon/game.unity3d");
			break;
			
		default:
			break;
		}
	}
	public void SentBasicButtons (string question, NetworkPlayer player){
		networkView.RPC ("SetButtons", player, "basic", question, "", "", "", "", "", "", "", "", "");
	}
	public void SentBasicButtons (string button1, string question, NetworkPlayer player){
		networkView.RPC ("SetButtons", player, "basic", question,  "", button1, "", "", "", "", "", "", "");
	}
	public void SentBasicButtons (string button1, string button2, string question, NetworkPlayer player){
		networkView.RPC ("SetButtons", player, "basic", question,  "", button1, button2, "", "", "", "", "", "");
	}
	public void SentBasicButtons (string button1, string button2, string button3, string question, NetworkPlayer player){
		networkView.RPC ("SetButtons", player, "basic", question,  "", button1, button2, button3, "", "", "", "", "");
	}
	public void SentBasicButtons (string button1, string button2, string button3, string button4, string question, NetworkPlayer player){
		networkView.RPC ("SetButtons", player, "basic", question,  "", button1, button2, button3, button4, "", "", "", "");
	}
	public void SentBasicButtons (string button1, string button2, string button3, string button4, string button5, string question, NetworkPlayer player){
		networkView.RPC ("SetButtons", player, "basic", question,  "", button1, button2, button3, button4, button5, "", "", "");
	}
	public void SentBasicButtons (string button1, string button2, string button3, string button4, string button5, string button6, string question, NetworkPlayer player){
		networkView.RPC ("SetButtons", player, "basic", question,  "", button1, button2, button3, button4, button5, button6, "", "");
	}
	public void SentBasicButtons (string button1, string button2, string button3, string button4, string button5, string button6, string button7, string question, NetworkPlayer player){
		networkView.RPC ("SetButtons", player, "basic", question,  "", button1, button2, button3, button4, button5, button6, button7, "");
	}
	public void SentBasicButtons (string button1, string button2, string button3, string button4, string button5, string button6, string button7, string button8, string question, NetworkPlayer player){
		networkView.RPC ("SetButtons", player, "basic", question,  "", button1, button2, button3, button4, button5, button6, button7, button8);
	}
	[RPC] void SetButtons (string type, string question, string actionWord, string button1, string button2, string button3, string button4, string button5, string button6, string button7, string button8){}
*/} 