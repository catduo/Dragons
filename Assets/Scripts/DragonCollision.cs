using UnityEngine;
using System.Collections;

public class DragonCollision : MonoBehaviour {
	
	private GameObject foodObject;
	private Transform modifiers;
	public int playerNumber;
	private Jovios jovios;
	
	void Start(){
		jovios = GameManager.jovios;
		foodObject = GameObject.Find ("MainCamera").GetComponent<GameManager>().foodObject;
		modifiers = GameObject.Find ("Modifiers").transform;
	}
	
	void OnTriggerEnter(Collider other){
		if(other.name == "Food(Clone)"){
			GameManager.score[jovios.GetPlayer(playerNumber).GetUserID().GetIDNumber()] += 1;
			if(GameManager.winner[0] > -1){
				if(GameManager.score[jovios.GetPlayer(playerNumber).GetUserID().GetIDNumber()] > GameManager.score[jovios.GetPlayer(playerNumber).GetUserID().GetIDNumber()]){
					for(int i = 0; i < jovios.GetPlayerCount(); i++){
						//Jovios.players[i].statusObject.GetComponent<Status>().crown.renderer.enabled = false;
						//Jovios.players[i].playerObject.GetComponent<Dragon>().crown.renderer.enabled = false;
					}
					GameManager.winner = new int[] {-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1};
					GameManager.winner[0] = playerNumber;
					//Jovios.players[playerNumber].statusObject.GetComponent<Status>().crown.renderer.enabled = true;
					//Jovios.players[playerNumber].playerObject.GetComponent<Dragon>().crown.renderer.enabled = true;
				}
				else if(GameManager.score[jovios.GetPlayer(playerNumber).GetUserID().GetIDNumber()] == GameManager.score[jovios.GetPlayer(GameManager.winner[0]).GetUserID().GetIDNumber()]){
					int j = 0;
					//Jovios.players[playerNumber].statusObject.GetComponent<Status>().crown.renderer.enabled = true;
					//Jovios.players[playerNumber].playerObject.GetComponent<Dragon>().crown.renderer.enabled = true;
					for(int i = 0; i < GameManager.winner.Length; i++){
						if(GameManager.winner[i] == -1){
							j = i;
							break;
						}
					}
					GameManager.winner[j] = playerNumber;
				}
			}
			else{
				GameManager.winner[0] = playerNumber;
			}
			jovios.GetPlayer(playerNumber).GetStatusObject().GetComponent<Status>().status.text = GameManager.score[jovios.GetPlayer(playerNumber).GetUserID().GetIDNumber()].ToString();
			transform.parent.SendMessage("AddSegment");
			GameObject foodItem = (GameObject) GameObject.Instantiate(foodObject, new Vector3((Random.value - 0.5F)*12, (Random.value - 0.5F)*9, 0), Quaternion.identity);
			foodItem.transform.parent = modifiers;
			Destroy(other.gameObject);
		}
		if(other.name == "BigFood(Clone)"){
			GameManager.score[jovios.GetPlayer(playerNumber).GetUserID().GetIDNumber()] += 10;
			jovios.GetPlayer(playerNumber).GetStatusObject().GetComponent<Status>().status.text = GameManager.score[jovios.GetPlayer(playerNumber).GetUserID().GetIDNumber()].ToString();
			transform.parent.SendMessage("AddSegment");
			transform.parent.SendMessage("AddSegment");
			transform.parent.SendMessage("AddSegment");
			transform.parent.SendMessage("AddSegment");
			Destroy(other.gameObject);
		}
		else if(other.name == "Snout"){}
		else if((other.transform.parent != transform.parent && other.name == "Segment(Clone)") && other.transform.parent.name != "Modifiers"){
			GameManager.score[jovios.GetPlayer(playerNumber).GetUserID().GetIDNumber()] += 1;
			jovios.GetPlayer(playerNumber).GetStatusObject().GetComponent<Status>().status.text = GameManager.score[jovios.GetPlayer(playerNumber).GetUserID().GetIDNumber()].ToString();
			other.transform.parent.SendMessage("Hit", SendMessageOptions.DontRequireReceiver);
			transform.parent.SendMessage("AddSegment");
		}
	}
	
	void OnCollisionEnter(Collision collision){
		if(collision.transform.parent.name == "Bounds"){
			transform.parent.SendMessage("Hit", SendMessageOptions.DontRequireReceiver);
			if(collision.transform.name == "Top"){
				transform.Rotate(transform.eulerAngles.x, transform.eulerAngles.y, 180 - transform.eulerAngles.z * 2);
			}
			if(collision.transform.name == "Left"){
				transform.Rotate(transform.eulerAngles.x, transform.eulerAngles.y, 180 - (transform.eulerAngles.z - 90) * 2);
			}
			if(collision.transform.name == "Right"){
				transform.Rotate(transform.eulerAngles.x, transform.eulerAngles.y, 180 - (transform.eulerAngles.z - 270) * 2);
			}
			if(collision.transform.name == "Bottom"){
				transform.Rotate(transform.eulerAngles.x, transform.eulerAngles.y, 180 - (transform.eulerAngles.z - 180) * 2);
			}
			rigidbody.angularVelocity = Vector3.zero;
		}
		if(collision.transform.name == "Head"){
			transform.parent.SendMessage("Hit", SendMessageOptions.DontRequireReceiver);
			transform.Rotate(transform.eulerAngles.x, transform.eulerAngles.y, Vector3.Angle(collision.transform.position, transform.position));
			rigidbody.angularVelocity = Vector3.zero;
		}
	}
	
	void OnCollisionStay(Collision collision){
		if(collision.transform.parent.name == "Bounds"){
			transform.parent.SendMessage("Hit", SendMessageOptions.DontRequireReceiver);
		}
	}
	
	void Hit(){
		transform.parent.SendMessage("Hit", SendMessageOptions.DontRequireReceiver);
	}
	
	void AddSegment(){
		transform.parent.GetComponent<Dragon>().AddSegment();
	}
}