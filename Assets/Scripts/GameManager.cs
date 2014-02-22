using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour, IJoviosPlayerListener {
	
	
	public static float foodSpawnTime;
	private float foodSpawnLag = 0;
	public GameObject foodObject;
	public GameObject bigFoodObject;
	private Transform playerObjects;
	private Transform modifiers;
	public static Dictionary<int, int> score = new Dictionary<int, int>();
	public static int[] winner = new int[] {-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1};
	public static Transform bonusSpawners;
	public static float bonusSpawnTimer = 0;
	public static GameObject chosenArena;
	public GameObject arena1;
	public static GameObject[] arenas;
	public GameObject arenaLobby;

	public GameObject statusObject;
	public GameObject playerObject;

	public static Jovios jovios;

	void Awake () {
		jovios = Jovios.Create();
	}

	// Use this for initialization
	void Start () {
		jovios.StartServer("snak");
		foodSpawnTime = Time.time;
		modifiers = GameObject.Find ("Modifiers").transform;
		playerObjects = GameObject.Find ("PlayerObjects").transform;
		jovios.AddPlayerListener(this);
	}
	
	// Update is called once per frame
	void Update () {
		if(MenuManager.gameState == GameState.GameOn){
			if(foodSpawnTime + foodSpawnLag < Time.time){
				foodSpawnTime = Time.time;
				foodSpawnLag = 30 / jovios.GetPlayerCount();
				GameObject foodItem = (GameObject) GameObject.Instantiate(foodObject, new Vector3((Random.value - 0.5F)*24, (Random.value - 0.5F)*18, 0), Quaternion.identity);
				foodItem.transform.parent = modifiers;
				if(Random.value < 0.1F){
					GameObject bigFoodItem = (GameObject) GameObject.Instantiate(bigFoodObject, new Vector3((Random.value - 0.5F)*24, (Random.value - 0.5F)*18, 0), Quaternion.identity);
					bigFoodItem.transform.parent = modifiers;
				}
			}
		}
	}
	
	public static void StartRound(){
		for(int i = 0; i < jovios.GetPlayerCount(); i++){
			score[jovios.GetPlayer(i).GetUserID().GetIDNumber()] = 0;
			if(jovios.GetPlayer(i).GetStatusObject().GetComponent<Status>().is_ready){
				jovios.GetPlayer(i).GetStatusObject().GetComponent<Status>().StartRound();
			}
		}
	}
	
	public static void EndRound(){
		//Destroy(chosenArena);
		MenuManager.gameState = GameState.ChooseArena;
		//chosenArena = (GameObject) GameObject.Instantiate(arenas[0], Vector3.zero, Quaternion.identity);
		for(int i = 0; i < jovios.GetPlayerCount(); i++){
			jovios.GetPlayer(i).GetStatusObject().GetComponent<Status>().status.text = "X";
			jovios.GetPlayer(i).GetStatusObject().GetComponent<Status>().status.color = Color.red;
		}
	}
	
	public static void ChooseArena(int selectedArena){
		if(selectedArena > 0){
			//Destroy (chosenArena);
			//chosenArena = (GameObject) GameObject.Instantiate(arenas[selectedArena], Vector3.zero, Quaternion.identity);
			if(MenuManager.gameState != GameState.GameOn && MenuManager.gameState != GameState.Countdown){
				MenuManager.lastTickTime = Time.time;
				MenuManager.gameState = GameState.Countdown;
				GameManager.StartRound();
				foodSpawnTime = Time.time;
			}
		}
	}
	
	bool IJoviosPlayerListener.PlayerConnected(JoviosPlayer p){
		Debug.Log (p.GetPlayerName());
		GameObject newStatusObject = (GameObject) GameObject.Instantiate(statusObject, new Vector3(-10 + p.GetPlayerNumber() * 2, 9.5F, 0), Quaternion.identity);
		jovios.GetPlayer(p.GetUserID()).SetStatusObject(newStatusObject);
		p.GetStatusObject().GetComponent<Status>().SetMyPlayer(p);
		return false;
	}
	bool IJoviosPlayerListener.PlayerUpdated(JoviosPlayer p){
		Debug.Log(p.GetPlayerName());
		return false;
	}
	bool IJoviosPlayerListener.PlayerDisconnected(JoviosPlayer p){
		Destroy(p.GetStatusObject());
		for(int i = 0; i < jovios.GetPlayerCount(); i++){
			jovios.GetPlayer(i).GetStatusObject().transform.localPosition = new Vector3(-10 + 2 * i, 9.5F, 0);
		}
		return false;
	}
}