using UnityEngine;
using System.Collections;

public class DragonCollision : MonoBehaviour {
	
	private GameObject foodObject;
	private Transform modifiers;
	
	void Start(){
		foodObject = GameObject.Find ("MainCamera").GetComponent<GameManager>().foodObject;
		modifiers = GameObject.Find ("Modifiers").transform;
	}
	
	void OnTriggerEnter(Collider other){
		if(other.tag == "Respawn"){
			transform.parent.SendMessage("AddSegment");
			GameObject foodItem = (GameObject) GameObject.Instantiate(foodObject, new Vector3((Random.value - 0.5F)*12, (Random.value - 0.5F)*9, 0), Quaternion.identity);
			foodItem.transform.parent = modifiers;
			Destroy(other.gameObject);
		}
		else if(other.name == "Snout"){}
		else if((other.transform.parent != transform.parent && other.name == "Segment(Clone)") && other.transform.parent.name != "Modifiers"){
			other.transform.parent.SendMessage("Hit", SendMessageOptions.DontRequireReceiver);
			transform.parent.SendMessage("AddSegment");
		}
	}
	
	void OnCollisionEnter(Collision collision){
		if(collision.transform.parent.name == "Bounds"){
			transform.parent.SendMessage("Hit", SendMessageOptions.DontRequireReceiver);
			transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 180 - transform.eulerAngles.z * 2);
			rigidbody.angularVelocity = Vector3.zero;
		}
		if(collision.transform.name == "Head"){
			transform.parent.SendMessage("Hit", SendMessageOptions.DontRequireReceiver);
			transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 180 - transform.eulerAngles.z * 2);
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
