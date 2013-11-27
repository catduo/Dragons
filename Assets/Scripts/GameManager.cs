using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	private float foodSpawnTime;
	private float foodSpawnLag = 0;
	public GameObject foodObject;
	private Transform modifiers;
	private Transform playerObjects;
	
	// Use this for initialization
	void Start () {
		foodSpawnTime = Time.time;
		modifiers = GameObject.Find ("Modifiers").transform;
		playerObjects = GameObject.Find ("PlayerObjects").transform;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(PlayerControls.is_gameOn){
			if(foodSpawnTime + foodSpawnLag < Time.time){
				foodSpawnTime = Time.time;
				foodSpawnLag = 10 / NetworkManager.playerList.Length;
				GameObject foodItem = (GameObject) GameObject.Instantiate(foodObject, new Vector3((Random.value - 0.5F)*24, (Random.value - 0.5F)*18, 0), Quaternion.identity);
				foodItem.transform.parent = modifiers;
			}
		}
	}
	
	public void EndRound (){
		for(int i = 0; i < modifiers.childCount; i++){
			Destroy(modifiers.GetChild(i).gameObject);
		}
		for(int i = 0; i < playerObjects.childCount; i++){
			Destroy(playerObjects.GetChild(i).gameObject);
		}
	}
}
